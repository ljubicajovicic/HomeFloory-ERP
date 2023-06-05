﻿using AutoMapper;
using HomeFloory.Data;
using HomeFloory.Models;
using HomeFloory.Models.KorisnikDto;
using HomeFloory.Repositories.KorisnikRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HomeFloory.Controllers
{
    [ApiController]
    [Route("api/")]
    public class PrijavaController : Controller
    {
        private readonly IConfiguration _config;
        private readonly HomeFlooryDbContext homeFlooryDbContext;

        public PrijavaController(IConfiguration config , HomeFlooryDbContext homeFlooryDbContext)
        {
            _config = config;
            this.homeFlooryDbContext = homeFlooryDbContext;
        }

        [HttpPost("Prijava")]
        public IActionResult Login([FromBody] LoginDto loginDto )
        {
            var korisnik = Authenticate(loginDto);

            if(korisnik != null)
            {

                var token = Generate(korisnik);
                return Ok(new { token });
            }
            return NotFound("Uneli ste pogresan email ili lozinku!");
        }

        [HttpPost("Registracija")]
        public async Task<IActionResult> AddKorisnik(AddKorisnikDto addKorisnikDto)
        {
            var korisnik = new Korisnik()   
            {
                    Ime = addKorisnikDto.Ime,
                    Prezime = addKorisnikDto.Prezime,
                    DatumRodjenja = addKorisnikDto.DatumRodjenja,
                    Kontakt = addKorisnikDto.Kontakt,
                    Email = addKorisnikDto.Email,
                    Lozinka = addKorisnikDto.Lozinka,
                    IdAdresaIsporuke = addKorisnikDto.IdAdresaIsporuke,
                    IdUloga = 1
            };

            homeFlooryDbContext.Korisnici.Add(korisnik);
            homeFlooryDbContext.SaveChanges();

            // Generate the token
            var token = Generate(korisnik);

            // Return the token in the response
            return Ok(new { token });
        }

        private string Generate(Korisnik korisnik)
        {
            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);
            string role;

            if(korisnik.IdUloga == 1)
            {
                 role = "Registrovan";
            }
            else if (korisnik.IdUloga == 2)
            {
                 role = "Admin";
            }
            else
            {
                 role = "Neregistrovan";
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, korisnik.Email),
                new Claim(ClaimTypes.GivenName, korisnik.Ime),
                new Claim(ClaimTypes.Surname, korisnik.Prezime),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
             _config["Jwt:Audience"],
             claims,
             expires: DateTime.Now.AddMinutes(60),
             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Korisnik Authenticate(LoginDto loginDto)
        {
            var currentKorisnik = homeFlooryDbContext.Korisnici.FirstOrDefault(o => o.Email.ToLower() == 
            loginDto.Email.ToLower() && o.Lozinka == loginDto.Lozinka);


            if(currentKorisnik != null)
            {
                return currentKorisnik;
            }

            return null;
        }

        [HttpPost]
        [Route("/admin")]
        [Authorize("Admin")]
        public async Task<IActionResult> AddAdmin([FromBody] AddAdminDto addAdminDto)
        {
            var existingKorisnik = homeFlooryDbContext.Korisnici.Where(a => a.Email == addAdminDto.Email).FirstOrDefault();

            if(existingKorisnik == null)
            {
                return BadRequest("Uneli ste nepostojeci email!");
            }

            existingKorisnik.IdUloga = 2;
            homeFlooryDbContext.Korisnici.Update(existingKorisnik);
            homeFlooryDbContext.SaveChanges();
            return Ok("Korisnik je admin!");
        }
    }
}

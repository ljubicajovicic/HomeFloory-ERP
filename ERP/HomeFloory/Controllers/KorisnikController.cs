using AutoMapper;
using HomeFloory.Models.KorpaDto;
using HomeFloory.Models;
using HomeFloory.Repositories.KorisnikRepo;
using HomeFloory.Repositories.KorpaRepo;
using Microsoft.AspNetCore.Mvc;
using HomeFloory.Models.KorisnikDto;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HomeFloory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KorisnikController : Controller
    {
        private readonly IKorisnikRepo korisnikRepo;
        private readonly IMapper mapper;

        public KorisnikController(IKorisnikRepo korisnikRepo, IMapper mapper)
        {
            this.korisnikRepo = korisnikRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllKorisnik()
        {
            var korisnik = await korisnikRepo.GetAllKorisnik();
            if (korisnik == null)
            {
                return NoContent();
            }
            var korisnikDto = mapper.Map<List<Korisnik>>(korisnik);
            return Ok(korisnikDto);
        }

        [HttpGet]
        [Route("{IdKorisnik}")]
        //[Authorize(Roles = "Registrovan, Admin")]
        public async Task<IActionResult> GetKorisnik(decimal IdKorisnik)
        {
            var korisnik = await korisnikRepo.GetKorisnik(IdKorisnik);
            if (korisnik == null)
            {
                return NotFound();
            }
            var korisnikDto = mapper.Map<Korisnik>(korisnik);
            return Ok(korisnikDto);
        }

        [HttpPost("Registracija")]
        public async Task<IActionResult> AddKorisnik(AddKorisnikDto addKorisnikDto)
        {
            try
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
                korisnik = await korisnikRepo.AddKorisnik(korisnik);
                var korisnikDto = mapper.Map<Korisnik>(korisnik);
                return CreatedAtAction(nameof(GetKorisnik), new { IdKorisnik = korisnikDto.IdKorisnik }, korisnikDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at create method");
            }
        }

        [HttpPut]
        [Route("{IdKorisnik}")]
        [Authorize(Roles = "Registrovan, Admin")]
        public async Task<IActionResult> UpdateKorisnik(decimal IdKorisnik, UpdateKorisnikDto updateKorisnikDto)
        {
            try
            {
                var korisnik = new Korisnik()
                {
                    Ime = updateKorisnikDto.Ime,
                    Prezime = updateKorisnikDto.Prezime,
                    DatumRodjenja = updateKorisnikDto.DatumRodjenja,
                    Kontakt = updateKorisnikDto.Kontakt,
                    Email = updateKorisnikDto.Email,
                    Lozinka = updateKorisnikDto.Lozinka,
                    IdAdresaIsporuke = updateKorisnikDto.IdAdresaIsporuke,
                    IdUloga = updateKorisnikDto.IdUloga
                };
                korisnik = await korisnikRepo.UpdateKorisnik(IdKorisnik,korisnik);
                if(korisnik == null)
                {
                    return NotFound();
                }
                var korisnikDto = mapper.Map<Korisnik>(korisnik);
                return Ok(korisnikDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at update method");
            }
        }

        [HttpDelete]
        [Route("{IdKorisnik}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteKorisnik(decimal IdKorisnik)
        {
            try
            {
                var korisnik = await korisnikRepo.DeleteKorisnik(IdKorisnik);
                if(korisnik == null)
                {
                    return NotFound();
                }
                var korisnikDto = mapper.Map<Korisnik>(korisnik);
                return Ok(korisnikDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at delete method");
            }
        }

    }
}

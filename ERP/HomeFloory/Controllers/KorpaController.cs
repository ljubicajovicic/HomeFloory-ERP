﻿using AutoMapper;
using HomeFloory.Models;
using HomeFloory.Models.DodatiProizvodiDto;
using HomeFloory.Models.KorpaDto;
using HomeFloory.Repositories.KorpaRepo;
using HomeFloory.Repositories.ProizvodRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeFloory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KorpaController : Controller
    {
        private readonly IKorpaRepo korpaRepo;
        private readonly IMapper mapper;

        public KorpaController(IKorpaRepo korpaRepo, IMapper mapper)
        {
            this.korpaRepo = korpaRepo;
            this.mapper = mapper;
        }

        [HttpGet("Korisnik")]
        public async Task<ActionResult<IReadOnlyList<Korpa>>> GetKorpaKorisnik([FromQuery] string? filterOn, 
            [FromQuery] decimal? filterQuery)
        {
            var korpa = await korpaRepo.GetKorpaKorisnik(filterOn, filterQuery);

            if (korpa == null)
            {
                return NoContent();
            }
            //var proizvodDto = mapper.Map<List<Proizvod>>(proizvod);
            return Ok(korpa);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKorpa()
        {
            
            var korpa = await korpaRepo.GetAllKorpa();
            if(korpa == null)
            {
                return NoContent();
            }
            var korpaDto = mapper.Map<List<Korpa>>(korpa);
            return Ok(korpaDto);
        }

        //provera trigera za racunanje ukupne cene u okviru korpe
        [HttpGet]
        [Route("{IdKorpa}")]
        public async Task<IActionResult> GetKorpa(decimal IdKorpa)
        {
            var korpa = await korpaRepo.GetKorpa(IdKorpa);
            if(korpa == null)
            {
                return NotFound();
            }
            var korpaDto = mapper.Map<Korpa>(korpa);
            return Ok(korpaDto);
        }

        //metoda koja pokrece prvi triger, ukoliko je ukupna cena veca od 15000 poruzbina je besplatna
        [HttpPost]
        //[Authorize(Roles = "Registrovan")]
        public async Task<IActionResult> AddKorpa (AddKorpaDto addKorpaDto)
        {
            try
            {
                var korpa = new Korpa()
                {
                    CenaDostave = addKorpaDto.CenaDostave,
                    UkupnaCena = addKorpaDto.UkupnaCena,
                    DodatiProizvodi = new List<DodatiProizvodi>(),
                    IdKorisnik = 1,
                    IdDostava = 1
                };
                korpa = await korpaRepo.AddKorpa(korpa);
                var korpaDto = mapper.Map<Korpa>(korpa);
                return CreatedAtAction(nameof(GetKorpa), new { IdKorpa = korpaDto.IdKorpa }, korpaDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at create method");
            }
        }

        //update obelezja ukupnaCena pokrece triger za besplatnu dostavu 
        [HttpPut]
        [Route("{IdKorpa}")]
        //[Authorize(Roles = "Registrovan")]
        public async Task<IActionResult> UpdateKorpa(decimal IdKorpa, UpdateKorpaDto updateKorpaDto)
        {
            try
            {
                //var korpa = new Korpa()
                var korpa = await korpaRepo.GetKorpa(IdKorpa);
                if(korpa == null)
                {
                    return NotFound();
                };

                korpa.CenaDostave = updateKorpaDto.CenaDostave;
                korpa.UkupnaCena = updateKorpaDto.UkupnaCena;
                korpa.IdKorisnik = updateKorpaDto.IdKorisnik;
                korpa.IdDostava = updateKorpaDto.IdDostava;
                korpa.Datum = updateKorpaDto.Datum;
                korpa.Status = updateKorpaDto.Status;
                //korpa.DodatiProizvodi.Clear();
                /*foreach (var dodatiProizvodDto in updateKorpaDto.DodatiProizvodi)
                {
                    var dodatiProizvod = new DodatiProizvodi
                    {
                        IdDodatiProizvodi = dodatiProizvodDto.IdDodatiProizvodi,
                        Cena = dodatiProizvodDto.Cena,
                        Kolicina = dodatiProizvodDto.Kolicina,
                        IdKorpa = dodatiProizvodDto.IdKorpa,
                        IdProizvod = dodatiProizvodDto.IdProizvod,
                        IdKorpaNavigation = dodatiProizvodDto.IdKorpaNavigation,
                        IdProizvodNavigation = dodatiProizvodDto.IdProizvodNavigation,
                    };
                    korpa.DodatiProizvodi.Add(dodatiProizvod);
                }*/

                korpa = await korpaRepo.UpdateKorpa(IdKorpa, korpa);
                if (korpa == null)
                {
                    return NotFound();
                }
                var korpaDto = mapper.Map<Korpa>(korpa);
                return Ok(korpaDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at update method");
            }
        }

        [HttpDelete]
        [Route("{IdKorpa}")]
        [Authorize(Roles = "Registrovan")]
        public async Task<IActionResult> DeleteKorpa(decimal IdKorpa)
        {
            try
            {
                var korpa = await korpaRepo.DeleteKorpa(IdKorpa);
                if (korpa == null)
                {
                    return NotFound();
                }
                var korpaDto = mapper.Map<Korpa>(korpa);
                return Ok(korpaDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at delete method");
            }
        }

    }
}

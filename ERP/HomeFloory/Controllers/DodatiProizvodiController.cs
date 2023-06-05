using AutoMapper;
using HomeFloory.Models;
using HomeFloory.Models.DodatiProizvodiDto;
using HomeFloory.Repositories.DodatiProizvodiRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HomeFloory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DodatiProizvodiController : Controller
    {
        private readonly IDodatiProizvodiRepo dodatiProizvodiRepo;
        private readonly IMapper mapper;

        public DodatiProizvodiController(IDodatiProizvodiRepo dodatiProizvodiRepo, IMapper mapper)
        {
            this.dodatiProizvodiRepo = dodatiProizvodiRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Registrovan, Admin")]
        public async Task<IActionResult> GetAllDodatiProizvodi()
        {
            var dodatiProizvodi = await dodatiProizvodiRepo.GetAllDodatiProizvodi();
            if(dodatiProizvodi == null)
            {
                return NoContent();
            }
            var dodatiProizvodiDto = mapper.Map<List<DodatiProizvodiDto>>(dodatiProizvodi);
            return Ok(dodatiProizvodiDto);
        }

        [HttpGet]
        [Route("{IdProizvod}/{IdKorpa}")]
        [Authorize(Roles = "Registrovan, Admin")]
        public async Task<IActionResult> GetDodatiProizvodi(decimal IdProizvod, decimal IdKorpa)
        {
            var dodatiProizvodi = await dodatiProizvodiRepo.GetDodatiProizvodi(IdProizvod,IdKorpa);
            if(dodatiProizvodi == null)
            {
                return NotFound();
            }
            var dodatiProizvodDto = mapper.Map<DodatiProizvodi>(dodatiProizvodi);
            return Ok(dodatiProizvodDto);
        }

        //metoda aktivira triger za racunanje ukupne cene proizvoda u korpi, na osnovu unesene cene i kolicine
        //kako azurira obelezje ukupna cena u okviru tabele korpa, takodje se pokrece i drugi triger
        [HttpPost]
        //[Authorize(Roles = "Registrovan")]
        public async Task<IActionResult> AddDodatiProizvodi(AddDodatiProizvodiDto addDodatiProizvodiDto)
        {
            try
            {
                var dodatiProizvodi = new DodatiProizvodi()
                {
                    IdProizvod = addDodatiProizvodiDto.IdProizvod,
                    IdKorpa = addDodatiProizvodiDto.IdKorpa,
                    Cena = addDodatiProizvodiDto.Cena,
                    Kolicina = addDodatiProizvodiDto.Kolicina,
                    KolicinaPoM2 = addDodatiProizvodiDto.KolicinaPoM2
                };
                dodatiProizvodi = await dodatiProizvodiRepo.AddDodatiProizvodi(dodatiProizvodi);
                var dodatiProizvodiDto = mapper.Map<DodatiProizvodi>(dodatiProizvodi);
                var key = $"{dodatiProizvodiDto.IdProizvod}-{dodatiProizvodi.IdKorpa}";
                return Created($"/api/DodatiProizvodi/{key}", dodatiProizvodiDto);
            }
            catch (DbUpdateException ex)
            {
                
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Naruseno ogranicenje stranog kljuca");
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at create method");

            }
        }

        [HttpPut]
        [Route("{IdProizvod}/{IdKorpa}")]
        //[Authorize(Roles = "Registrovan")]
        public async Task<IActionResult> UpdateDodatiProizvodi(decimal IdProizvod,decimal IdKorpa, UpdateDodatiProizvodiDto updateDodatiProizvodiDto)
        {
            try
            {
                var dodatiProizvodi = new DodatiProizvodi()
                {
                    Cena = updateDodatiProizvodiDto.Cena,
                    Kolicina = updateDodatiProizvodiDto.Kolicina,
                    KolicinaPoM2 = updateDodatiProizvodiDto.KolicinaPoM2
                };
                dodatiProizvodi = await dodatiProizvodiRepo.UpdateDodatiProizvodi(IdProizvod, IdKorpa, dodatiProizvodi);
                if(dodatiProizvodi == null)
                {
                    return NotFound();
                }
                var dodatiProizvodiDto = mapper.Map<DodatiProizvodi>(dodatiProizvodi);
                return Ok(dodatiProizvodiDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at update method");
            }
        }

        /*metoda aktivira triger, u slucaju da se ukloni odredjeni proizvod iz tabele dodatiProizvodi,
        umanjuje se obelezje ukupna cena u korpi, za vrednost uklonjenog proizvoda*/
        [HttpDelete]
        [Route("{IdProizvod}/{IdKorpa}")]
        [Authorize(Roles = "Registrovan")]
        public async Task<IActionResult> DeleteDodatiProizvodi(decimal IdProizvod, decimal IdKorpa)
        {
            try
            {
                var dodatiProizvodi = await dodatiProizvodiRepo.DeleteDodatiProizvodi(IdProizvod, IdKorpa);
                if(dodatiProizvodi == null)
                {
                    return NotFound();
                }
                var dodatiProizvodiDto = mapper.Map<DodatiProizvodi>(dodatiProizvodi);
                return Ok(dodatiProizvodiDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at delete method");
            }
        }

    }
}

using AutoMapper;
using HomeFloory.Models;
using HomeFloory.Repositories.PlacanjeRepo;
using Microsoft.AspNetCore.Mvc;
using HomeFloory.Models.PlacanjeDto;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HomeFloory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacanjeController : Controller
    {
        private readonly IPlacanjeRepo placanjeRepo;
        private readonly IMapper mapper;

        public PlacanjeController(IPlacanjeRepo placanjeRepo, IMapper mapper)
        {
            this.placanjeRepo = placanjeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Registrovan, Admin")]
        public async Task<IActionResult> GetAllPlacanje()
        {
            var placanje = await placanjeRepo.GetAllPlacanje();
            if(placanje == null)
            {
                return NoContent();
            }
            var placanjeDto = mapper.Map<List<Placanje>>(placanje);
            return Ok(placanjeDto);
        }

        [HttpGet]
        [Route("{IdPlacanje}")]
        [Authorize(Roles = "Registrovan")]
        public async Task<IActionResult> GetPlacanje(decimal IdPlacanje)
        {
            var placanje = await placanjeRepo.GetPlacanje(IdPlacanje);
            if(placanje == null)
            {
                return NotFound();
            }
            var placanjeDto = mapper.Map<Placanje>(placanje);
            return Ok(placanjeDto);
        }

        [HttpPost]
        [Authorize(Roles = "Registrovan")]
        public async Task<IActionResult> AddPlacanje(AddPlacanjeDto addPlacanjeDto)
        {
            try
            {
                var placanje = new Placanje()
                {
                    Status = addPlacanjeDto.Status,
                    Datum = addPlacanjeDto.Datum,
                    IdKorisnik = addPlacanjeDto.IdKorisnik
                };
                placanje = await placanjeRepo.AddPlacanje(placanje);
                var placanjeDto = mapper.Map<Placanje> (placanje);
                return CreatedAtAction(nameof(GetPlacanje), new { IdPlacanje = placanjeDto.IdPlacanje }, placanjeDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at create method");
            }
        }

        [HttpPut]
        [Route("{IdPlacanje}")]
        [Authorize(Roles = "Registrovan")]
        public async Task<IActionResult> UpdatePlacanje(decimal IdPlacanje, UpdatePlacanjeDto updatePlacanjeDto)
        {
            try
            {
                var placanje = new Placanje()
                {
                    Status = updatePlacanjeDto.Status,
                    Datum = updatePlacanjeDto.Datum,
                    IdKorisnik = updatePlacanjeDto.IdKorisnik
                };
                placanje = await placanjeRepo.UpdatePlacanje(IdPlacanje, placanje);
                if(placanje == null)
                {
                    return NotFound();
                }
                var placanjeDto = mapper.Map<Placanje>(placanje);
                return Ok(placanjeDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at update method");
            }
        }

        [HttpDelete]
        [Route("{IdPlacanje}")]
        [Authorize(Roles = "Registrovan, Admin")]
        public async Task<IActionResult> DeletePlacanje(decimal IdPlacanje)
        {
            try
            {
                var placanje = await placanjeRepo.DeletePlacanje(IdPlacanje);
                if(placanje == null)
                {
                    return NotFound();
                }
                var placanjeDto = mapper.Map<Placanje>(placanje);
                return Ok(placanjeDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at delete method");
            }
        }

    }
}

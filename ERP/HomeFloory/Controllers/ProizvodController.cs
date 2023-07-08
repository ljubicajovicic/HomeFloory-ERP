using AutoMapper;
using HomeFloory.Models;
using HomeFloory.Models.ProizvodDto;
using HomeFloory.Repositories.ProizvodjacRepo;
using HomeFloory.Repositories.ProizvodRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace HomeFloory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProizvodController : Controller
    {
        private readonly IProizvodRepo proizvodRepo;
        private readonly IMapper mapper;

        public ProizvodController(IProizvodRepo proizvodRepo, IMapper mapper)
        {
            this.proizvodRepo = proizvodRepo;
            this.mapper = mapper;
        }

        [HttpGet("NoParam")]
        public async Task<IActionResult> GetProizvodNoParam()
        {
            var proizvodjac = await proizvodRepo.GetProizvodNoParam();
            if (proizvodjac == null)
            {
                return NoContent();
            }
            var proizvodDto = mapper.Map<List<Proizvod>>(proizvodjac);
            return Ok(proizvodDto);
        }

        //api/ProizvodController?filterOn=Naziv&filterQuery=Track&sortBy=CenaPoM2&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Proizvod>>> GetAllProizvod([FromQuery] string? search, [FromQuery] string? filterOn, [FromQuery] decimal? filterQuery, [FromQuery] string? filterOn2, 
            [FromQuery] decimal? filterQuery2,
            [FromQuery] string? sortBy,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var proizvod = await proizvodRepo.GetAllProizvod(search, filterOn, filterQuery, filterOn2, filterQuery2,
                sortBy, pageNumber, pageSize);

            if (proizvod == null)
            {
                return NoContent();
            }
            //var proizvodDto = mapper.Map<List<Proizvod>>(proizvod);
            return Ok(proizvod);
        }

        [HttpGet]
        [Route("{IdProizvod}")]
        public async Task<IActionResult> GetProizvod(decimal IdProizvod)
        {
            var proizvod = await proizvodRepo.GetProizvod(IdProizvod);
            if (proizvod == null)
            {
                return NotFound();
            }
            var proizvodDto = mapper.Map<Proizvod>(proizvod);
            return Ok(proizvodDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProizvod(AddProizvodDto addProizvodDto)
        {
            try
            {
                var proizvod = new Proizvod()
                {
                    Naziv = addProizvodDto.Naziv,
                    Opis = addProizvodDto.Opis,
                    KolicinaNaStanju = addProizvodDto.KolicinaNaStanju,
                    PaketPoM2 = addProizvodDto.PaketPoM2,
                    CenaPoM2 = addProizvodDto.CenaPoM2,
                    Dimenzija = addProizvodDto.Dimenzija,
                    Nijansa = addProizvodDto.Nijansa,
                    UrlSlike = addProizvodDto.UrlSlike,
                    IdKategorija = addProizvodDto.IdKategorija,
                    IdProizvodjac = addProizvodDto.IdProizvodjac  
                };
                proizvod = await proizvodRepo.AddProizvod(proizvod);
                var proizvodDto = mapper.Map<Proizvod>(proizvod);
                return CreatedAtAction(nameof(GetProizvod), new { IdProizvod = proizvodDto.IdProizvod }, proizvodDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at create method");
            }
        }

        [HttpPut]
        [Route("{IdProizvod}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProizvod(decimal IdProizvod, UpdatePoizvodDto updatePoizvodDto)
        {
            try
            {
                var proizvod = new Proizvod()
                {
                    Naziv = updatePoizvodDto.Naziv,
                    Opis = updatePoizvodDto.Opis,
                    KolicinaNaStanju = updatePoizvodDto.KolicinaNaStanju,
                    PaketPoM2 = updatePoizvodDto.PaketPoM2,
                    CenaPoM2 = updatePoizvodDto.CenaPoM2,
                    Dimenzija = updatePoizvodDto.Dimenzija,
                    Nijansa = updatePoizvodDto.Nijansa,
                    UrlSlike= updatePoizvodDto.UrlSlike,
                    IdKategorija = updatePoizvodDto.IdKategorija,
                    IdProizvodjac = updatePoizvodDto.IdProizvodjac
                };
                proizvod = await proizvodRepo.UpdateProizvod(IdProizvod, proizvod);
                if (proizvod == null)
                {
                    return NotFound();
                }
                var proizvodDto = mapper.Map<ProizvodDto>(proizvod);
                return Ok(proizvodDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at update method");
            }
        }

        [HttpDelete]
        [Route("{IdProizvod}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProizvod(decimal IdProizvod)
        {
            try
            {
                var proizvod = await proizvodRepo.DeleteProizvod(IdProizvod);
                if (proizvod == null)
                {
                    return NotFound();
                }
                var proizvodDto = mapper.Map<Proizvod>(proizvod);
                return Ok(proizvodDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at delete method");
            }
        }
    }
}

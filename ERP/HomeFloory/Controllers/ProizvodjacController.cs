using AutoMapper;
using HomeFloory.Models;
using HomeFloory.Models.ProizvodjacDto;
using HomeFloory.Repositories.ProizvodjacRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HomeFloory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProizvodjacController : Controller
    {
        private readonly IProizvodjacRepo proizvodjacRepo;
        private readonly IMapper mapper;

        public ProizvodjacController(IProizvodjacRepo proizvodjacRepo, IMapper mapper)
        {
            this.proizvodjacRepo = proizvodjacRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProizvodjac()
        {
            var proizvodjac = await proizvodjacRepo.GetAllProizvodjac();
            if (proizvodjac == null)
            {
                return NoContent();
            }
            var proizvodjacDto = mapper.Map<List<Proizvodjac>>(proizvodjac);
            return Ok(proizvodjacDto);
        }

        [HttpGet]
        [Route("{IdProizvodjac}")]
        public async Task<IActionResult> GetProizvodjac(decimal IdProizvodjac)
        {
            var proizvodjac = await proizvodjacRepo.GetProizvodjac(IdProizvodjac);
            if(proizvodjac == null)
            {
                return NotFound();
            }
            var proizvodjacDto = mapper.Map<Proizvodjac>(proizvodjac);
            return Ok(proizvodjacDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProizvodjac(AddProizvodjacDto addProizvodjacDto)
        {
            try
            {
                var proizvodjac = new Proizvodjac()
                {
                    NazivProizvodjaca = addProizvodjacDto.NazivProizvodjaca
                };
                proizvodjac = await proizvodjacRepo.AddProizvodjac(proizvodjac);
                var proizvodjacDto = mapper.Map<Proizvodjac>(proizvodjac);
                return CreatedAtAction(nameof(GetProizvodjac), new { IdProizvodjac = proizvodjacDto.IdProizvodjac }, proizvodjacDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at create method");
            }
        }

        [HttpPut]
        [Route("{IdProizvodjac}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProizvodjac(decimal IdProizvodjac, UpdateProizvodjacDto updateProizvodjacDto)
        {
            try
            {
                var proizvodjac = new Proizvodjac()
                {
                    NazivProizvodjaca = updateProizvodjacDto.NazivProizvodjaca
                };
                proizvodjac = await proizvodjacRepo.UpdateProizvodjac(IdProizvodjac, proizvodjac);
                if(proizvodjac == null)
                {
                    return NotFound();
                }
                var proizvodjacDto = mapper.Map<ProizvodjacDto>(proizvodjac);
                return Ok(proizvodjacDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at update method");
            }
        }

        [HttpDelete]
        [Route("{IdProizvodjac}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProizvodjac(decimal IdProizvodjac)
        {
            try
            {
                var proizvodjac = await proizvodjacRepo.DeleteProizvodjac(IdProizvodjac);
                if(proizvodjac == null)
                {
                    return NotFound();
                }
                var proizvodjacDto = mapper.Map<Proizvodjac>(proizvodjac);
                return Ok(proizvodjacDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at delete method");
            }
        }

    }
}

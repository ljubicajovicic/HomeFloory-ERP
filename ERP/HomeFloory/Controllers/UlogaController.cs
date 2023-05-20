using AutoMapper;
using HomeFloory.Models;
using HomeFloory.Models.UlogaDto;
using HomeFloory.Repositories.UlogaRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HomeFloory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UlogaController : Controller
    {
        private readonly IUlogaRepo ulogaRepo;
        private readonly IMapper mapper;

        public UlogaController(IUlogaRepo ulogaRepo, IMapper mapper)
        {
            this.ulogaRepo = ulogaRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUloga()
        {
            var uloga = await ulogaRepo.GetAllUloga();
            if (uloga == null)
            {
                return NoContent();
            }
            var ulogaDto = mapper.Map<List<UlogaDto>>(uloga);
            return Ok(ulogaDto);
        }

        [HttpGet]
        [Route("{IdUloga}")]
        public async Task<IActionResult> GetUloga(decimal IdUloga)
        {
            var uloga = await ulogaRepo.GetUloga(IdUloga);
            if(uloga == null)
            {
                return NotFound();
            }
            var ulogaDto = mapper.Map<Uloga>(uloga);
            return Ok(ulogaDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUloga(AddUlogaDto addUlogaDto)
        {
            try
            {
                var uloga = new Uloga()
                {
                    Uloga1 = addUlogaDto.Uloga1
                };
                uloga = await ulogaRepo.AddUloga(uloga);
                var ulogaDto = mapper.Map<Uloga>(uloga);
                return CreatedAtAction(nameof(GetUloga), new {IdUloga = ulogaDto.IdUloga}, ulogaDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at create method");
            }
        }

        [HttpPut]
        [Route("{IdUloga}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUloga(decimal IdUloga, UpdateUlogaDto updateUlogaDto)
        {
            try
            {
                var uloga = new Uloga()
                {
                    Uloga1 = updateUlogaDto.Uloga1
                };

                uloga = await ulogaRepo.UpdateUloga(IdUloga, uloga);
                if(uloga == null)
                {
                    return NotFound();
                }
                var ulogaDto = mapper.Map<UlogaDto>(uloga);
                return Ok(ulogaDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at update method");
            }
        }

        [HttpDelete]
        [Route("{IdUloga}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUloga(decimal IdUloga)
        {
            try
            {
                var uloga = await ulogaRepo.DeleteUloga(IdUloga);
                if(uloga == null)
                {
                    return NotFound();
                }
                var ulogaDto = mapper.Map<Uloga>(uloga);
                return Ok(ulogaDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at delete method");
            }
        }

    }
}

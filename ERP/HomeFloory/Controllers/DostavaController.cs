using AutoMapper;
using HomeFloory.Models;
using HomeFloory.Models.DostavaDto;
using HomeFloory.Repositories.DostavaRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HomeFloory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DostavaController : Controller
    {
        private readonly IDostavaRepo dostavaRepo;
        private readonly IMapper mapper;

        public DostavaController(IDostavaRepo dostavaRepo, IMapper mapper)
        {
            this.dostavaRepo = dostavaRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Registrovan")]
        public async Task<IActionResult> GetAllDostava()
        {
            var dostava = await dostavaRepo.GetAllDostava();
            if(dostava == null)
            {
                return NoContent();
            }
            var dostavaDto = mapper.Map<List<Dostava>>(dostava);
            return Ok(dostavaDto);
        }

        [HttpGet]
        [Route("{IdDostava}")]
        [Authorize(Roles = "Registrovan, Admin")]
        public async Task<IActionResult> GetDostava(decimal IdDostava)
        {
            var dostava = await dostavaRepo.GetDostava(IdDostava);
            if(dostava == null)
            {
                return NotFound();
            }
            var dostavaDto = mapper.Map<Dostava>(dostava);
            return Ok(dostavaDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Registrovan")]
        public async Task<IActionResult> AddDostava(AddDostavaDto addDostavaDto)
        {
            try
            {
                var dostava = new Dostava()
                {
                    TipDostave = addDostavaDto.TipDostave,
                    NazivSluzbe = addDostavaDto.NazivSluzbe,
                    CenaUsluge = addDostavaDto.CenaUsluge,
                    RokDostave = addDostavaDto.RokDostave
                };
                dostava = await dostavaRepo.AddDostava(dostava);
                var dostavaDto = mapper.Map<Dostava>(dostava);
                return CreatedAtAction(nameof(GetDostava), new { IdDostava = dostavaDto.IdDostava }, dostavaDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at create method");
            }
        }

        [HttpPut]
        [Route("{IdDostava}")]
        [Authorize(Roles = "Admin, Registrovan")]
        public async Task<IActionResult> UpdateDostava(decimal IdDostava,UpdateDostavaDto updateDostavaDto)
        {
            try
            {
                var dostava = new Dostava()
                {
                    TipDostave = updateDostavaDto.TipDostave,
                    NazivSluzbe = updateDostavaDto.NazivSluzbe,
                    CenaUsluge = updateDostavaDto.CenaUsluge,
                    RokDostave = updateDostavaDto.RokDostave
                };
                dostava = await dostavaRepo.UpdateDostava(IdDostava,dostava);
                if(dostava == null)
                {
                    return NotFound();
                }
                var dostavaDto = mapper.Map<Dostava>(dostava);
                return Ok(dostavaDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at update method");
            }
        }

        
        [HttpDelete]
        [Route("{IdDostava}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDostava(decimal IdDostava)
        {
            try
            {
                var dostava = await dostavaRepo.DeleteDostava(IdDostava);
                if (dostava == null)
                {
                    return NotFound();
                }
                var dostavaDto = mapper.Map<Dostava>(dostava);
                return Ok(dostavaDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at delete method");
            }
        }
    }
}

using AutoMapper;
using HomeFloory.Models;
using HomeFloory.Repositories.KategorijaRepo;
using Microsoft.AspNetCore.Mvc;
using HomeFloory.Models.KategorijaDto.KategorijaDto;
using HomeFloory.Models.KategorijaDto;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HomeFloory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KategorijaController : Controller
    {
        private readonly IKategorijaRepo kategorijaRepo;
        private readonly IMapper mapper;

        public KategorijaController(IKategorijaRepo kategorijaRepo, IMapper mapper)
        {
            this.kategorijaRepo = kategorijaRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKategorija()
        {
            var kategorija = await kategorijaRepo.GetAllKategorija();
            if(kategorija == null)
            {
                return NoContent();
            }
            var kategorijaDto = mapper.Map<List<Kategorija>>(kategorija);
            return Ok(kategorijaDto);
        }

        [HttpGet]
        [Route("{IdKategorija}")]
        public async Task<IActionResult> GetKategorija(decimal IdKategorija)
        {
            var kategorija = await kategorijaRepo.GetKategorija(IdKategorija);
            if(kategorija == null)
            {
                return NotFound();
            }
            var kategorijaDto = mapper.Map<Kategorija>(kategorija);
            return Ok(kategorijaDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddKategorija(AddKategorijaDto addKategorijaDto)
        {
            try
            {
                var kategorija = new Kategorija()
                {
                    NazivKategorije = addKategorijaDto.NazivKategorije,
                };
                kategorija = await kategorijaRepo.AddKategorija(kategorija);
                var kategorijaDto = mapper.Map<Kategorija>(kategorija);
                return CreatedAtAction(nameof(GetKategorija), new {IdKategorija = kategorijaDto.IdKategorija}, kategorijaDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at create method");
            }
        }

        [HttpPut]
        [Route("IdKategorija")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateKategorija(decimal IdKategorija, UpdateKategorijaDto updateKategorijaDto)
        {
            try
            {
                var kategorija = new Kategorija()
                {
                    NazivKategorije = updateKategorijaDto.NazivKategorije
                };
                kategorija = await kategorijaRepo.UpdateKategorija(IdKategorija, kategorija);
                if(kategorija == null)
                {
                    return NotFound();
                }
                var kategorijaDto = mapper.Map<KategorijaDto>(kategorija);
                return Ok(kategorijaDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at update method");
            }
        }

        [HttpDelete]
        [Route("{IdKategorija}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteKategorija(decimal IdKategorija)
        {
            try
            {
                var kategorija = await kategorijaRepo.DeleteKategorija(IdKategorija);
                if(kategorija == null)
                {
                    return NotFound();
                }
                var kategorijaDto = mapper.Map<Kategorija> (kategorija);
                return Ok(kategorijaDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at delete method");
            }
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using HomeFloory.Data;
using HomeFloory.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using HomeFloory.Models.AdresaIsporukeDto;
using HomeFloory.Repositories.AdresaIsporukeRepo;

namespace HomeFloory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdresaIsporukeController : Controller
    {
        private readonly IAdresaIsporukeRepo adresaIsporukeRepo;
        private readonly IMapper mapper;

        public AdresaIsporukeController(IAdresaIsporukeRepo adresaIsporukeRepo, IMapper mapper)
        {
            this.adresaIsporukeRepo = adresaIsporukeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllAdresaIsporuke()
        {
            var adresaIsporuke = await adresaIsporukeRepo.GetAllAdresaIsporuke();
            if (adresaIsporuke == null)
            {
                return NoContent();
            }
            var adresaIsporukeDto = mapper.Map<List<AdresaIsporukeDto>>(adresaIsporuke);
            return Ok(adresaIsporukeDto);
        }

        [HttpGet]
        [Route("{IdAdresaIsporuke}")]
        [Authorize(Roles = "Registrovan, Admin")]
        public async Task<IActionResult> GetAdresaIsporuke(decimal IdAdresaIsporuke)
        {
            var adresaIsporuke = await adresaIsporukeRepo.GetAdresaIsporuke(IdAdresaIsporuke);
            if (adresaIsporuke == null)
            {
                return NotFound();
            }
            var adresaIsporukeDto = mapper.Map<AdresaIsporuke>(adresaIsporuke);
            return Ok(adresaIsporukeDto);
        }

        [HttpPost]
        [Authorize(Roles = "Registrovan")]
        public async Task<IActionResult> AddAdresaIsporuke(AddAdresaIsporukeDto addAdresaIsporukeDto)
        {
            try
            {
                var adresaIsporuke = new AdresaIsporuke()
                {
                    Grad = addAdresaIsporukeDto.Grad,
                    Drzava = addAdresaIsporukeDto.Drzava,
                    Ulica = addAdresaIsporukeDto.Ulica,
                    PostanskiBroj = addAdresaIsporukeDto.PostanskiBroj
                };

                adresaIsporuke = await adresaIsporukeRepo.AddAdresaIsporuke(adresaIsporuke);
                var adresaIsporukeDto = mapper.Map<AdresaIsporukeDto>(adresaIsporuke);
                return CreatedAtAction(nameof(GetAdresaIsporuke), new { IdAdresaIsporuke = adresaIsporukeDto.IdAdresaIsporuke }, adresaIsporukeDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at create method");
            }
        }

        [HttpPut]
        [Route("{IdAdresaIsporuke}")]
        [Authorize(Roles = "Registrovan")]
        public async Task<IActionResult> UpdateAdresaIsporuke(decimal IdAdresaIsporuke, UpdateAdresaIsporukeDto updateAdresaIsporukeDto)
        {
            try
            {
                var adresaIsporuke = new AdresaIsporuke()
                {
                    Grad = updateAdresaIsporukeDto.Grad,
                    Drzava = updateAdresaIsporukeDto.Drzava,
                    Ulica = updateAdresaIsporukeDto.Ulica,
                    PostanskiBroj = updateAdresaIsporukeDto.PostanskiBroj
                };
                adresaIsporuke = await adresaIsporukeRepo.UpdateAdresaIsporuke(IdAdresaIsporuke, adresaIsporuke);
                if (adresaIsporuke == null)
                {
                    return NotFound();
                }
                var adresaIsporukeDto = mapper.Map<AdresaIsporukeDto>(adresaIsporuke);
                return Ok(adresaIsporukeDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at update method");
            }
        }

        [HttpDelete]
        [Route("{IdAdresaIsporuke}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdresaIsporuke([FromRoute] decimal IdAdresaIsporuke)
        {
            try
            {
                var adresaIsporuke = await adresaIsporukeRepo.DeleteAdresaIsporuke(IdAdresaIsporuke);
                if (adresaIsporuke == null)
                {
                    return NotFound();
                }
                var adresaIsporukeDto = mapper.Map<AdresaIsporuke>(adresaIsporuke);
                return Ok(adresaIsporukeDto);
            } 
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error at delete method");
            }
        }
    }
}

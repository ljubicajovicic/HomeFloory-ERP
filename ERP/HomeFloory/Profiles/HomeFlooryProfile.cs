using AutoMapper;
using HomeFloory.Models;
using HomeFloory.Models.AdresaIsporukeDto;
using HomeFloory.Models.DodatiProizvodiDto;
using HomeFloory.Models.DostavaDto;
using HomeFloory.Models.KategorijaDto.KategorijaDto;
using HomeFloory.Models.KorisnikDto;
using HomeFloory.Models.KorpaDto;
using HomeFloory.Models.PlacanjeDto;
using HomeFloory.Models.ProizvodDto;
using HomeFloory.Models.ProizvodjacDto;
using HomeFloory.Models.UlogaDto;

namespace HomeFloory.Profiles
{
    public class HomeFlooryProfile : Profile
    {
        public HomeFlooryProfile()
        {
            CreateMap<AdresaIsporuke, AdresaIsporukeDto>()
                .ReverseMap();

            CreateMap<Uloga, UlogaDto>()
                .ReverseMap();

            CreateMap<Kategorija, KategorijaDto>()
                .ReverseMap();

            CreateMap<Proizvodjac, ProizvodjacDto>()
                .ReverseMap();

            CreateMap<Dostava, DostavaDto>()
                .ReverseMap();

            CreateMap<Proizvod, ProizvodDto>()
                .ReverseMap();

            CreateMap<Korpa, KorpaDto>()
                .ReverseMap();

            CreateMap<DodatiProizvodi, DodatiProizvodiDto>()
                .ReverseMap();

            CreateMap<Placanje, PlacanjeDto>()
                .ReverseMap();

            CreateMap<Korisnik, KorisnikDto>()
                .ReverseMap();

            CreateMap<Korpa, KorpaDto>()
            .ForMember(dest => dest.DodatiProizvodi, opt => opt.MapFrom(src => src.DodatiProizvodi))
            .ReverseMap();
        }
    }
}

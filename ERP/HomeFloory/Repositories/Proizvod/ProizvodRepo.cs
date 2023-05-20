using HomeFloory.Data;
using HomeFloory.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Net.Http.Headers;

namespace HomeFloory.Repositories.ProizvodRepo
{
    public class ProizvodRepo : IProizvodRepo
    {
        private readonly HomeFlooryDbContext homeFlooryDbContext;

        public ProizvodRepo(HomeFlooryDbContext homeFlooryDbContext)
        {
            this.homeFlooryDbContext = homeFlooryDbContext;
        }

        public async Task<Proizvod> AddProizvod(Proizvod proizvod)
        {
            Random random = new Random();
            proizvod.IdProizvod = random.Next(0, 100000);
            await homeFlooryDbContext.Proizvodi.AddAsync(proizvod);
            await homeFlooryDbContext.SaveChangesAsync();
            return proizvod;
        }
 

        public async Task<Proizvod> DeleteProizvod(decimal IdProizvod)
        {
            var existingProizvod = await homeFlooryDbContext.Proizvodi.FindAsync(IdProizvod);
            if (existingProizvod == null)
            {
                return null;
            };
            homeFlooryDbContext.Proizvodi.Remove(existingProizvod);
            homeFlooryDbContext.SaveChangesAsync();
            return existingProizvod;
        }

        public async Task<Pagination<Proizvod>> GetAllProizvod(string? search, string? filterOn = null, decimal? filterQuery = null,
            string? filterOn2 = null, decimal? filterQuery2 = null,
            string? sortBy = null,
            int pageNumber = 1, int pageSize = 1000)
        {
            var proizvodi = homeFlooryDbContext.Proizvodi
                .Include(x => x.IdKategorijaNavigation)
                .Include(x => x.IdProizvodjacNavigation).AsQueryable();

            //search
            if(string.IsNullOrEmpty(search) == false)
            {
                proizvodi = proizvodi.Where(x => x.Naziv.Contains(search) || x.Opis.Contains(search));
            }

            //filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && filterQuery.HasValue)
            {
                if(filterOn.Equals("idKategorija", StringComparison.OrdinalIgnoreCase))
                {
                    proizvodi = proizvodi.Where(x => x.IdKategorija == filterQuery);
                }
            }

            if (string.IsNullOrWhiteSpace(filterOn2) == false && filterQuery2.HasValue)
            {
                if (filterOn2.Equals("idProizvodjac", StringComparison.OrdinalIgnoreCase))
                {
                    proizvodi = proizvodi.Where(x => x.IdProizvodjac == filterQuery2);
                }
            }


            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Cena-rastuce", StringComparison.OrdinalIgnoreCase))
                {
                    proizvodi = proizvodi.OrderBy(x => x.CenaPoM2);
                }
                if (sortBy.Equals("Cena-opdajuce", StringComparison.OrdinalIgnoreCase))
                {
                    proizvodi = proizvodi.OrderByDescending(x => x.CenaPoM2);
                }
            }

            //pagination
            //var skipProizvod = (pageNumber - 1) * pageSize;
            int totalItems = await proizvodi.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            int skipCount = (pageNumber - 1) * pageSize;

            proizvodi = proizvodi.Skip(skipCount).Take(pageSize);

            return new Pagination<Proizvod>(proizvodi.ToList(), pageNumber,pageSize, totalItems, totalPages);

            //return await proizvodi.Skip(skipProizvod).Take(pageSize).ToListAsync();

            /*return await
                homeFlooryDbContext.Proizvodi
                .Include(x => x.IdKategorijaNavigation)
                .Include(x => x.IdProizvodjacNavigation)
                .ToListAsync();*/
        }

        public async Task<Proizvod> GetProizvod(decimal IdProizvod)
        {
            return await homeFlooryDbContext.Proizvodi.FirstOrDefaultAsync(x => x.IdProizvod == IdProizvod);
        }

        public async Task<Proizvod> UpdateProizvod(decimal IdProizvod, Proizvod proizvod)
        {
            var existingProizvod = await homeFlooryDbContext.Proizvodi.FindAsync(IdProizvod);
            if (existingProizvod == null)
            {
                return null;
            }
            existingProizvod.Naziv = proizvod.Naziv;
            existingProizvod.Opis = proizvod.Opis;
            existingProizvod.KolicinaNaStanju = proizvod.KolicinaNaStanju;
            existingProizvod.PaketPoM2 = proizvod.PaketPoM2;
            existingProizvod.CenaPoM2 = proizvod.CenaPoM2;
            existingProizvod.Dimenzija = proizvod.Dimenzija;
            existingProizvod.Nijansa = proizvod.Nijansa;
            existingProizvod.UrlSlike = proizvod.UrlSlike;
            return existingProizvod;
        }
    }
}

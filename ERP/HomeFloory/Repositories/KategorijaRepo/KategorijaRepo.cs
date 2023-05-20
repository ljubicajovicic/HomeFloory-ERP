using HomeFloory.Data;
using HomeFloory.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeFloory.Repositories.KategorijaRepo
{
    public class KategorijaRepo : IKategorijaRepo
    {
        private readonly HomeFlooryDbContext homeFlooryDbContext;

        public KategorijaRepo(HomeFlooryDbContext homeFlooryDbContext)
        {
            this.homeFlooryDbContext = homeFlooryDbContext;
        }

        public async Task<Kategorija> AddKategorija(Kategorija kategorija)
        {
            Random random = new Random();
            kategorija.IdKategorija = random.Next(0, 100000);
            await homeFlooryDbContext.Kategorije.AddAsync(kategorija);
            await homeFlooryDbContext.SaveChangesAsync();
            return kategorija;
        }

        public async Task<Kategorija> DeleteKategorija(decimal IdKategorija)
        {
            var existingKategorija = await homeFlooryDbContext.Kategorije.FindAsync(IdKategorija);

            if (existingKategorija == null)
            {
                return null;
            }
            homeFlooryDbContext.Kategorije.Remove(existingKategorija);
            homeFlooryDbContext.SaveChanges();
            return existingKategorija;
        }

        public async Task<IEnumerable<Kategorija>> GetAllKategorija()
        {
            return await homeFlooryDbContext.Kategorije.ToListAsync();
        }

        public async Task<Kategorija> GetKategorija(decimal IdKategorija)
        {
            return await homeFlooryDbContext.Kategorije.FirstOrDefaultAsync(x => x.IdKategorija == IdKategorija);
        }

        public async Task<Kategorija> UpdateKategorija(decimal IdKategorija, Kategorija kategorija)
        {
            var existingKategorija = await homeFlooryDbContext.Kategorije.FindAsync(IdKategorija);
            if (existingKategorija == null)
            {
                return null;
            }
            existingKategorija.NazivKategorije = kategorija.NazivKategorije;
            await homeFlooryDbContext.SaveChangesAsync();
            return existingKategorija;
        }
    }
}

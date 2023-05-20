using HomeFloory.Data;
using HomeFloory.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeFloory.Repositories.AdresaIsporukeRepo
{
    public class AdresaIsporukeRepo : IAdresaIsporukeRepo
    {
        private readonly HomeFlooryDbContext homeFlooryDbContext;

        public AdresaIsporukeRepo(HomeFlooryDbContext homeFlooryDbContext)
        {
            this.homeFlooryDbContext = homeFlooryDbContext;
        }

        public async Task<AdresaIsporuke> AddAdresaIsporuke(AdresaIsporuke adresaIsporuke)
        {
            Random random = new Random();
            adresaIsporuke.IdAdresaIsporuke = random.Next(0, 100000);
            await homeFlooryDbContext.AdreseIsporuke.AddAsync(adresaIsporuke);
            await homeFlooryDbContext.SaveChangesAsync();
            return adresaIsporuke;
        }

        public async Task<AdresaIsporuke> DeleteAdresaIsporuke(decimal IdAdresaIsporuke)
        {
            var existingAdresaIsporuke = await homeFlooryDbContext.AdreseIsporuke.FindAsync(IdAdresaIsporuke);

            if (existingAdresaIsporuke == null)
            {
                return null;
            }
            homeFlooryDbContext.AdreseIsporuke.Remove(existingAdresaIsporuke);
            homeFlooryDbContext.SaveChanges();
            return existingAdresaIsporuke;
        }

        public async Task<AdresaIsporuke> GetAdresaIsporuke(decimal IdAdresaIsporuke)
        {
            return await homeFlooryDbContext.AdreseIsporuke.FirstOrDefaultAsync(x => x.IdAdresaIsporuke == IdAdresaIsporuke);
        }

        public async Task<IEnumerable<AdresaIsporuke>> GetAllAdresaIsporuke()
        {
            return await homeFlooryDbContext.AdreseIsporuke.ToListAsync();
        }

        public async Task<AdresaIsporuke> UpdateAdresaIsporuke(decimal IdAdresaIsporuke, AdresaIsporuke adresaIsporuke)
        {
            var existingAdresaIsporuke = await homeFlooryDbContext.AdreseIsporuke.FindAsync(IdAdresaIsporuke);
            if (existingAdresaIsporuke == null)
            {
                return null;
            }
            existingAdresaIsporuke.Grad = adresaIsporuke.Grad;
            existingAdresaIsporuke.Drzava = adresaIsporuke.Drzava;
            existingAdresaIsporuke.Ulica = adresaIsporuke.Ulica;
            existingAdresaIsporuke.PostanskiBroj = adresaIsporuke.PostanskiBroj;
            await homeFlooryDbContext.SaveChangesAsync();
            return existingAdresaIsporuke;
        }
    }
}

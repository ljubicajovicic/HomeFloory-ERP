using HomeFloory.Data;
using HomeFloory.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeFloory.Repositories.PlacanjeRepo
{
    public class PlacanjeRepo : IPlacanjeRepo
    {
        private readonly HomeFlooryDbContext homeFlooryDbContext;

        public PlacanjeRepo(HomeFlooryDbContext homeFlooryDbContext)
        {
            this.homeFlooryDbContext = homeFlooryDbContext;
        }

        public async Task<Placanje> AddPlacanje(Placanje placanje)
        {
            Random random = new Random();
            placanje.IdPlacanje = random.Next(0, 100000);
            await homeFlooryDbContext.Placanja.AddAsync(placanje);
            await homeFlooryDbContext.SaveChangesAsync();
            return placanje;
        }

        public async Task<Placanje> DeletePlacanje(decimal IdPlacanje)
        {
            var existingPlacanje = await homeFlooryDbContext.Placanja.FindAsync(IdPlacanje);
            if(existingPlacanje == null)
            {
                return null;
            }
            homeFlooryDbContext.Placanja.Remove(existingPlacanje);
            homeFlooryDbContext.SaveChangesAsync();
            return existingPlacanje;
        }

        public async Task<IEnumerable<Placanje>> GetAllPlacanje()
        {
            return await homeFlooryDbContext.Placanja
                .Include(x => x.IdKorisnikNavigation)
                .ToListAsync();
        }

        public async Task<Placanje> GetPlacanje(decimal IdPlacanje)
        {
            return await homeFlooryDbContext.Placanja.FirstOrDefaultAsync(x => IdPlacanje == IdPlacanje);
        }

        public async Task<Placanje> UpdatePlacanje(decimal IdPlacanje, Placanje placanje)
        {
            var existingPlacanje = await homeFlooryDbContext.Placanja.FindAsync(IdPlacanje);
            if(existingPlacanje == null)
            {
                return null;
            }
            existingPlacanje.Status = placanje.Status;
            existingPlacanje.Datum = placanje.Datum;
            return existingPlacanje;
        }
    }
}

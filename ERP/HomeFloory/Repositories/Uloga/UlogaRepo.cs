using HomeFloory.Data;
using HomeFloory.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeFloory.Repositories.UlogaRepo
{
    public class UlogaRepo : IUlogaRepo
    {
        private readonly HomeFlooryDbContext homeFlooryDbContext;

        public UlogaRepo(HomeFlooryDbContext homeFlooryDbContext)
        {
            this.homeFlooryDbContext = homeFlooryDbContext;
        }

        public async Task<Uloga> AddUloga(Uloga uloga)
        {
            Random random = new Random();
            uloga.IdUloga = random.Next(0, 100000);
            await homeFlooryDbContext.Uloge.AddAsync(uloga);
            await homeFlooryDbContext.SaveChangesAsync();
            return uloga;
        }

        public async Task<Uloga> DeleteUloga(decimal IdUloga)
        {
            var existingUloga = await homeFlooryDbContext.Uloge.FindAsync(IdUloga);

            if (existingUloga == null)
            {
                return null;
            }
            homeFlooryDbContext.Uloge.Remove(existingUloga);
            homeFlooryDbContext.SaveChanges();
            return existingUloga;
        }

        public async Task<IEnumerable<Uloga>> GetAllUloga()
        {
            return await homeFlooryDbContext.Uloge.ToListAsync();
        }

        public async Task<Uloga> GetUloga(decimal IdUloga)
        {
            return await homeFlooryDbContext.Uloge.FirstOrDefaultAsync(x => x.IdUloga == IdUloga);
        }

        public async Task<Uloga> UpdateUloga(decimal IdUloga, Uloga uloga)
        {
            var existingUloga = await homeFlooryDbContext.Uloge.FindAsync(IdUloga);
            if (existingUloga == null)
            {
                return null;
            }
            existingUloga.Uloga1 = uloga.Uloga1;
            await homeFlooryDbContext.SaveChangesAsync();
            return existingUloga;
        }
    }
}

using HomeFloory.Data;
using HomeFloory.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeFloory.Repositories.KorpaRepo
{
    public class KorpaRepo : IKorpaRepo
    {
        private readonly HomeFlooryDbContext homeFlooryDbContext;

        public KorpaRepo(HomeFlooryDbContext homeFlooryDbContext)
        {
            this.homeFlooryDbContext = homeFlooryDbContext;
        }

        public async Task<Korpa> AddKorpa(Korpa korpa)
        {
            Random random = new Random();
            korpa.IdKorpa = random.Next(0, 100000);
            await homeFlooryDbContext.Korpe.AddAsync(korpa);
            await homeFlooryDbContext.SaveChangesAsync();
            return korpa;

        }

        public async Task<Korpa> DeleteKorpa(decimal IdKorpa)
        {
            var existingKorpa = await homeFlooryDbContext.Korpe.FindAsync(IdKorpa);
            if(existingKorpa == null)
            {
                return null;
            }
            homeFlooryDbContext.Korpe.Remove(existingKorpa);
            homeFlooryDbContext.SaveChangesAsync();
            return existingKorpa;
        }

        public async Task<IEnumerable<Korpa>> GetAllKorpa()
        {
            return await
                homeFlooryDbContext.Korpe
                .Include(x => x.IdPlacanjeNavigation)
                .Include(x => x.IdDostavaNavigation)
                .Include(x => x.DodatiProizvodi)
                .ToListAsync();
        }

        public async Task<Korpa> GetKorpa(decimal IdKorpa)
        {
            return await homeFlooryDbContext.Korpe.Include(x => x.DodatiProizvodi).FirstOrDefaultAsync(x => x.IdKorpa == IdKorpa);
        }

        public async Task<Korpa> UpdateKorpa(decimal IdKorpa, Korpa korpa)
        {
            var existingKorpa = await homeFlooryDbContext.Korpe.FindAsync(IdKorpa);
            if(existingKorpa == null)
            {
                return null;
            }
            existingKorpa.CenaDostave = korpa.CenaDostave;
            existingKorpa.UkupnaCena = korpa.UkupnaCena;

            await homeFlooryDbContext.SaveChangesAsync();
            return existingKorpa;
        }
    }
}

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

        public async Task<List<Korpa>> GetKorpaKorisnik(string? filterOn = null, decimal? filterQuery = null)
        {

            var korpa =
                homeFlooryDbContext.Korpe
                .Include(x => x.DodatiProizvodi)
                .AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && filterQuery.HasValue)
            {
                if (filterOn.Equals("idKorisnik", StringComparison.OrdinalIgnoreCase))
                {
                    korpa = korpa.Where(x => x.IdKorisnik == filterQuery);
                }
            }

            return korpa.ToList();

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
            existingKorpa.IdKorisnik = korpa.IdKorisnik;
            existingKorpa.IdDostava = korpa.IdDostava;
            existingKorpa.Datum = korpa.Datum;
            existingKorpa.Status = korpa.Status;
            //existingKorpa.DodatiProizvodi = korpa.DodatiProizvodi;

            await homeFlooryDbContext.SaveChangesAsync();
            return existingKorpa;
        }

        public async Task<Korpa> UpdateStatus(Korpa korpa)
        {
            var existingKorpa = await homeFlooryDbContext.Korpe.FirstOrDefaultAsync();
            if(existingKorpa == null)
            {
                return null;
            }
            existingKorpa.Datum = korpa.Datum;
            existingKorpa.Status = korpa.Status;
            existingKorpa.UkupnaCena = korpa.UkupnaCena;
            existingKorpa.PaymentIntent = korpa.PaymentIntent;
            existingKorpa.ClientSecret = korpa.ClientSecret;
            existingKorpa.IdDostava = korpa.IdDostava;
            existingKorpa.IdKorisnik = korpa.IdKorisnik;
            await homeFlooryDbContext.SaveChangesAsync();
            return existingKorpa;

        }
    }
}

using HomeFloory.Controllers;
using HomeFloory.Data;
using HomeFloory.Models;
using HomeFloory.Models.KorisnikDto;
using Microsoft.EntityFrameworkCore;

namespace HomeFloory.Repositories.KorisnikRepo
{
    public class KorisnikRepo : IKorisnikRepo
    {
        private readonly HomeFlooryDbContext homeFlooryDbContext;

        public KorisnikRepo(HomeFlooryDbContext homeFlooryDbContext)
        {
            this.homeFlooryDbContext = homeFlooryDbContext;
        }

        public async Task<Korisnik> AddKorisnik(Korisnik korisnik)
        {
            Random random = new Random();
            korisnik.IdKorisnik = random.Next(0, 100000);
            await homeFlooryDbContext.Korisnici.AddAsync(korisnik);
            await homeFlooryDbContext.SaveChangesAsync();
            return korisnik;
        }

        public async Task<Korisnik> DeleteKorisnik(decimal IdKorisnik)
        {
            var existingKorisnik = await homeFlooryDbContext.Korisnici.FindAsync(IdKorisnik);
            if (existingKorisnik == null)
            {
                return null;
            }
            homeFlooryDbContext.Korisnici.Remove(existingKorisnik);
            await homeFlooryDbContext.SaveChangesAsync();
            return existingKorisnik;
        }

        public async Task<IEnumerable<Korisnik>> GetAllKorisnik()
        {
            return await
                homeFlooryDbContext.Korisnici
                .Include(x => x.IdUlogaNavigation)
                .Include(x => x.IdAdresaIsporukeNavigation)
                .ToListAsync();
        }

        public async Task<Korisnik> GetKorisnik(decimal IdKorisnik)
        {
            return await homeFlooryDbContext.Korisnici.FirstOrDefaultAsync(x => x.IdKorisnik == IdKorisnik);
        }

        public async Task<Korisnik> UpdateKorisnik(decimal IdKorisnik, Korisnik korisnik)
        {
            var existingKorisnik = await homeFlooryDbContext.Korisnici.FindAsync(IdKorisnik);
            if (existingKorisnik == null)
            {
                return null;
            }
            existingKorisnik.Ime = korisnik.Ime;
            existingKorisnik.Prezime = korisnik.Prezime;
            existingKorisnik.DatumRodjenja = korisnik.DatumRodjenja;
            existingKorisnik.Kontakt = korisnik.Kontakt;
            existingKorisnik.Email = korisnik.Email;
            existingKorisnik.Lozinka = korisnik.Lozinka;
            return existingKorisnik;
        }
    }
}

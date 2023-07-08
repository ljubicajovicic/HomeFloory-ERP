using HomeFloory.Data;
using HomeFloory.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeFloory.Repositories.DodatiProizvodiRepo
{
    public class DodatiProizvodiRepo : IDodatiProizvodiRepo
    {
        private readonly HomeFlooryDbContext homeFlooryDbContext;

        public DodatiProizvodiRepo(HomeFlooryDbContext homeFlooryDbContext)
        {
            this.homeFlooryDbContext = homeFlooryDbContext;
        }

        public async Task<DodatiProizvodi> AddDodatiProizvodi(DodatiProizvodi dodatiProizvodi)
        {

            Random random = new Random();
            dodatiProizvodi.IdDodatiProizvodi = random.Next(0, 100000);
            await homeFlooryDbContext.DodatiProizvodi.AddAsync(dodatiProizvodi);
            await homeFlooryDbContext.SaveChangesAsync();
            return dodatiProizvodi;
        }

        public async Task<DodatiProizvodi> DeleteDodatiProizvodi(decimal IdDodatiPorizvodi)
        {
            var existingDodatiProizvodi = await homeFlooryDbContext.DodatiProizvodi.FindAsync(IdDodatiPorizvodi);
            if (existingDodatiProizvodi == null)
            {
                return null;
            }
            homeFlooryDbContext.DodatiProizvodi.Remove(existingDodatiProizvodi);
            await homeFlooryDbContext.SaveChangesAsync();
            return existingDodatiProizvodi;
        }

        public async Task<IEnumerable<DodatiProizvodi>> GetAllDodatiProizvodi()
        {
            return await homeFlooryDbContext.DodatiProizvodi
                .Include(x => x.IdProizvodNavigation)
                .Include(x => x.IdKorpaNavigation)
                .ToListAsync();
        }

        public async Task<DodatiProizvodi> GetDodatiProizvodi(decimal IdDodatiProizvodi)
        {
            //return await homeFlooryDbContext.DodatiProizvodi.FirstOrDefaultAsync(x => x.IdProizvod == IdProizvod && x.IdKorpa == IdKorpa);
            return await homeFlooryDbContext.DodatiProizvodi.FirstOrDefaultAsync(x => x.IdDodatiProizvodi == IdDodatiProizvodi);

        }

        public async Task<DodatiProizvodi> UpdateDodatiProizvodi(decimal IdDodatiProizvodi, DodatiProizvodi dodatiProizvodi)
        {

            var existingDodatiProizvodi = await homeFlooryDbContext.DodatiProizvodi.FindAsync(IdDodatiProizvodi);
            if(existingDodatiProizvodi == null)
            {
                return null;
            }
            existingDodatiProizvodi.Cena = dodatiProizvodi.Cena;
            existingDodatiProizvodi.Kolicina = dodatiProizvodi.Kolicina;
            existingDodatiProizvodi.IdProizvod = dodatiProizvodi.IdProizvod;
            existingDodatiProizvodi.IdKorpa = dodatiProizvodi.IdKorpa;
            return existingDodatiProizvodi;

        }
    }
}

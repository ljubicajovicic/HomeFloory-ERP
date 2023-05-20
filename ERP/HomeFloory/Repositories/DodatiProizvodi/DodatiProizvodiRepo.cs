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
            await homeFlooryDbContext.DodatiProizvodi.AddAsync(dodatiProizvodi);
            await homeFlooryDbContext.SaveChangesAsync();
            return dodatiProizvodi;
        }

        public async Task<DodatiProizvodi> DeleteDodatiProizvodi(decimal IdProizvod, decimal IdKorpa)
        {
            var existingDodatiProizvodi = await homeFlooryDbContext.DodatiProizvodi.FindAsync(IdProizvod, IdKorpa);
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

        public async Task<DodatiProizvodi> GetDodatiProizvodi(decimal IdProizvod, decimal IdKorpa)
        {
            return await homeFlooryDbContext.DodatiProizvodi.FirstOrDefaultAsync(x => x.IdProizvod == IdProizvod && x.IdKorpa == IdKorpa);
            
        }

        public async Task<DodatiProizvodi> UpdateDodatiProizvodi(decimal IdProizvod,decimal IdKorpa, DodatiProizvodi dodatiProizvodi)
        {

            var existingDodatiProizvodi = await homeFlooryDbContext.DodatiProizvodi.FindAsync(IdProizvod, IdKorpa);
            if(existingDodatiProizvodi == null)
            {
                return null;
            }
            existingDodatiProizvodi.Cena = dodatiProizvodi.Cena;
            existingDodatiProizvodi.Kolicina = dodatiProizvodi.Kolicina;
            existingDodatiProizvodi.KolicinaPoM2 = dodatiProizvodi.KolicinaPoM2;
            return existingDodatiProizvodi;

        }
    }
}

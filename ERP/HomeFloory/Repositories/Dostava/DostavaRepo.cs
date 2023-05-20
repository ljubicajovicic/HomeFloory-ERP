using HomeFloory.Data;
using HomeFloory.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HomeFloory.Repositories.DostavaRepo
{
    public class DostavaRepo : IDostavaRepo
    {
        private readonly HomeFlooryDbContext homeFlooryDbContext;

        public DostavaRepo(HomeFlooryDbContext homeFlooryDbContext)
        {
            this.homeFlooryDbContext = homeFlooryDbContext;
        }

        public async Task<Dostava> AddDostava(Dostava dostava)
        {
            Random random= new Random();
            dostava.IdDostava = random.Next(0, 100000);
            await homeFlooryDbContext.Dostave.AddAsync(dostava);
            await homeFlooryDbContext.SaveChangesAsync();
            return dostava;

        }

        public async Task<Dostava> DeleteDostava(decimal IdDostava)
        {
            var existingDostava = await homeFlooryDbContext.Dostave.FindAsync(IdDostava);
            if(existingDostava == null)
            {
                return null;
            }
            homeFlooryDbContext.Dostave.Remove(existingDostava);
            homeFlooryDbContext.SaveChangesAsync();
            return existingDostava;
        }

        public async Task<IEnumerable<Dostava>> GetAllDostava()
        {
            return await homeFlooryDbContext.Dostave.ToListAsync();
        }

        public async Task<Dostava> GetDostava(decimal IdDostava)
        {
            return await homeFlooryDbContext.Dostave.FirstOrDefaultAsync(x => x.IdDostava == IdDostava);
        }

        public async Task<Dostava> UpdateDostava(decimal IdDostava, Dostava dostava)
        {
            var existingDostava = await homeFlooryDbContext.Dostave.FindAsync(IdDostava);
            if(existingDostava == null)
            {
                return null;
            }
            existingDostava.TipDostave = dostava.TipDostave;
            existingDostava.NazivSluzbe = dostava.NazivSluzbe;
            existingDostava.CenaUsluge = dostava.CenaUsluge;
            existingDostava.RokDostave = dostava.RokDostave;
            await homeFlooryDbContext.SaveChangesAsync();
            return existingDostava;
        }
    }
}

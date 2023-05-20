using HomeFloory.Data;
using HomeFloory.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace HomeFloory.Repositories.ProizvodjacRepo
{
    public class ProizvodjacRepo : IProizvodjacRepo
    {
        private readonly HomeFlooryDbContext homeFlooryDbContext;

        public ProizvodjacRepo(HomeFlooryDbContext homeFlooryDbContext)
        {
            this.homeFlooryDbContext = homeFlooryDbContext;
        }

        public async Task<Proizvodjac> AddProizvodjac(Proizvodjac proizvodjac)
        {
            Random random = new Random();
            proizvodjac.IdProizvodjac = random.Next(0, 100000);
            await homeFlooryDbContext.Proizvodjaci.AddAsync(proizvodjac);
            await homeFlooryDbContext.SaveChangesAsync();
            return proizvodjac;
        }

        public async Task<Proizvodjac> DeleteProizvodjac(decimal IdProizvodjac)
        {
            var existingProizvodjac = await homeFlooryDbContext.Proizvodjaci.FindAsync(IdProizvodjac);
            if(existingProizvodjac == null)
            {
                return null;
            };
            homeFlooryDbContext.Proizvodjaci.Remove(existingProizvodjac);
            homeFlooryDbContext.SaveChangesAsync();
            return existingProizvodjac;

        }

        public async Task<IEnumerable<Proizvodjac>> GetAllProizvodjac()
        {
            return await homeFlooryDbContext.Proizvodjaci.ToListAsync();
        }

        public async Task<Proizvodjac> GetProizvodjac(decimal IdProizvodjac)
        {
            return await homeFlooryDbContext.Proizvodjaci.FirstOrDefaultAsync(x => x.IdProizvodjac == IdProizvodjac);
        }

        public async Task<Proizvodjac> UpdateProizvodjac(decimal IdProizvodjac, Proizvodjac proizvodjac)
        {
            var existingProizvodjac = await homeFlooryDbContext.Proizvodjaci.FindAsync(IdProizvodjac);
            if(existingProizvodjac == null)
            {
                return null;
            }
            existingProizvodjac.NazivProizvodjaca = proizvodjac.NazivProizvodjaca;
            await homeFlooryDbContext.SaveChangesAsync();
            return existingProizvodjac;
        }
    }
}

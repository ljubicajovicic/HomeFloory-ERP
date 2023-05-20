using HomeFloory.Data;
using HomeFloory.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeFloory.Repositories.UlogaRepo
{
    public interface IUlogaRepo
    {
        Task<IEnumerable<Uloga>> GetAllUloga();

        Task<Uloga> GetUloga(decimal IdUloga);

        Task<Uloga> AddUloga(Uloga uloga);
        Task<Uloga> UpdateUloga(decimal IdUloga, Uloga uloga);
        Task<Uloga> DeleteUloga(decimal IdUloga);
    }
}

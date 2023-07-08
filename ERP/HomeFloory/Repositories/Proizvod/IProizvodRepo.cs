using HomeFloory.Models;

namespace HomeFloory.Repositories.ProizvodRepo
{
    public interface IProizvodRepo
    {
        Task<Pagination<Proizvod>> GetAllProizvod(string? search, string? filterOn = null, decimal? filterQuery = null, 
            string? filterOn2 = null, decimal? filterQuery2 = null,
            string? sortBy = null,
            int pageNumber = 1, int pageSize = 1000);

        Task<IEnumerable<Proizvod>> GetProizvodNoParam();

        Task<Proizvod> GetProizvod(decimal IdProizvod);

        Task<Proizvod> AddProizvod(Proizvod proizvod);
        Task<Proizvod> UpdateProizvod(decimal IdProizvod, Proizvod proizvod);
        Task<Proizvod> DeleteProizvod(decimal IdProizvod);

    }
}

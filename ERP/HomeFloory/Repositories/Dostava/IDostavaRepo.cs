using HomeFloory.Models;

namespace HomeFloory.Repositories.DostavaRepo
{
    public interface IDostavaRepo
    {
        Task<IEnumerable<Dostava>> GetAllDostava();

        Task<Dostava> GetDostava(decimal IdDostava);

        Task<Dostava> AddDostava(Dostava dostava);
        Task<Dostava> UpdateDostava(decimal IdDostava, Dostava dostava);
        Task<Dostava> DeleteDostava(decimal IdDostava);
    }
}

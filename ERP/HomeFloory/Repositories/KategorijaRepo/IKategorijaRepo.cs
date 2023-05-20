using HomeFloory.Models;

namespace HomeFloory.Repositories.KategorijaRepo
{
    public interface IKategorijaRepo
    {
        Task<IEnumerable<Kategorija>> GetAllKategorija();

        Task<Kategorija> GetKategorija(decimal IdKategorija);

        Task<Kategorija> AddKategorija(Kategorija kategorija);
        Task<Kategorija> UpdateKategorija(decimal IdKategorija, Kategorija kategorija);
        Task<Kategorija> DeleteKategorija(decimal IdKategorija);
    }
}

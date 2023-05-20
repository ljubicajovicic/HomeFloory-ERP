using HomeFloory.Models;

namespace HomeFloory.Repositories.AdresaIsporukeRepo
{
    public interface IAdresaIsporukeRepo
    {
        Task<IEnumerable<AdresaIsporuke>> GetAllAdresaIsporuke();

        Task<AdresaIsporuke> GetAdresaIsporuke(decimal IdAdresaIsporuke);

        Task<AdresaIsporuke> AddAdresaIsporuke(AdresaIsporuke adresaIsporuke);
        Task<AdresaIsporuke> UpdateAdresaIsporuke(decimal IdAdresaIsporuke, AdresaIsporuke adresaIsporuke);
        Task<AdresaIsporuke> DeleteAdresaIsporuke(decimal IdAdresaIsporuke);
    }
}

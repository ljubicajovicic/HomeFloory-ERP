using HomeFloory.Models;

namespace HomeFloory.Repositories.DodatiProizvodiRepo
{
    public interface IDodatiProizvodiRepo 
    {
        Task<IEnumerable<DodatiProizvodi>> GetAllDodatiProizvodi();
        Task<DodatiProizvodi> GetDodatiProizvodi(decimal IdDodatiProizvodi);
        Task<DodatiProizvodi> AddDodatiProizvodi(DodatiProizvodi dodatiProizvodi);
        Task<DodatiProizvodi> UpdateDodatiProizvodi(decimal IdDodatiProizvodi , DodatiProizvodi dodatiProizvodi);
        Task<DodatiProizvodi> DeleteDodatiProizvodi(decimal IdDodatiProizvodi);
    }
}

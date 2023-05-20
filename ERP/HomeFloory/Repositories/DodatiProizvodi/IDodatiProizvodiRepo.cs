using HomeFloory.Models;

namespace HomeFloory.Repositories.DodatiProizvodiRepo
{
    public interface IDodatiProizvodiRepo 
    {
        Task<IEnumerable<DodatiProizvodi>> GetAllDodatiProizvodi();
        Task<DodatiProizvodi> GetDodatiProizvodi(decimal IdProizvod, decimal IdKorpa);
        Task<DodatiProizvodi> AddDodatiProizvodi(DodatiProizvodi dodatiProizvodi);
        Task<DodatiProizvodi> UpdateDodatiProizvodi(decimal IdProizvod,decimal IdKorpa , DodatiProizvodi dodatiProizvodi);
        Task<DodatiProizvodi> DeleteDodatiProizvodi(decimal IdProizvod, decimal IdKorpa);
    }
}

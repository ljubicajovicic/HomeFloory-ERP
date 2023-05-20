using HomeFloory.Models;

namespace HomeFloory.Repositories.KorisnikRepo
{
    public interface IKorisnikRepo
    {
        Task<IEnumerable<Korisnik>> GetAllKorisnik();
        Task<Korisnik> GetKorisnik(decimal IdKorisnik);
        Task<Korisnik> AddKorisnik(Korisnik korisnik);
        Task<Korisnik> UpdateKorisnik(decimal IdKorisnik, Korisnik korisnik);
        Task<Korisnik> DeleteKorisnik(decimal IdKorisnik);
    }
}

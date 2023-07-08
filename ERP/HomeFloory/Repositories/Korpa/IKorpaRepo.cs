using HomeFloory.Models;

namespace HomeFloory.Repositories.KorpaRepo
{
    public interface IKorpaRepo
    {
        Task<IEnumerable<Korpa>> GetAllKorpa();
        Task<Korpa> GetKorpa(decimal IdKorpa);

        Task<List<Korpa>> GetKorpaKorisnik(string? filterOn = null, decimal? filterQuery = null);
        Task<Korpa> AddKorpa(Korpa korpa);
        Task <Korpa> UpdateKorpa(decimal IdKorpa, Korpa korpa);
        Task<Korpa> DeleteKorpa(decimal IdKorpa);

        Task<Korpa> UpdateStatus(Korpa korpa);
    }
}

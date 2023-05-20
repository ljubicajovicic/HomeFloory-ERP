using HomeFloory.Models;

namespace HomeFloory.Repositories.KorpaRepo
{
    public interface IKorpaRepo
    {
        Task<IEnumerable<Korpa>> GetAllKorpa();
        Task<Korpa> GetKorpa(decimal IdKorpa);
        Task<Korpa> AddKorpa(Korpa korpa);
        Task <Korpa> UpdateKorpa(decimal IdKorpa, Korpa korpa);
        Task<Korpa> DeleteKorpa(decimal IdKorpa);
    }
}

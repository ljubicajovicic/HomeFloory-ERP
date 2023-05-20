using HomeFloory.Models;

namespace HomeFloory.Repositories.PlacanjeRepo
{
    public interface IPlacanjeRepo
    {
        Task<IEnumerable<Placanje>> GetAllPlacanje();
        Task<Placanje> GetPlacanje(decimal IdPlacanje);
        Task<Placanje> AddPlacanje(Placanje placanje);
        Task<Placanje> UpdatePlacanje(decimal IdPlacanje, Placanje placanje);
        Task<Placanje> DeletePlacanje(decimal IdPlacanje);
    }
}

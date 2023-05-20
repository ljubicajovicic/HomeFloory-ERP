using HomeFloory.Models;

namespace HomeFloory.Repositories.ProizvodjacRepo
{
    public interface IProizvodjacRepo
    {
        Task<IEnumerable<Proizvodjac>> GetAllProizvodjac();

        Task<Proizvodjac> GetProizvodjac(decimal IdProizvodjac);

        Task<Proizvodjac> AddProizvodjac(Proizvodjac proizvodjac);
        Task<Proizvodjac> UpdateProizvodjac(decimal IdProizvodjac, Proizvodjac proizvodjac);
        Task<Proizvodjac> DeleteProizvodjac(decimal IdProizvodjac);
    }
}

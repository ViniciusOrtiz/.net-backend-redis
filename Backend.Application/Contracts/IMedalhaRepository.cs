using Backend.Application.Contracts.Repositories;
using Backend.Domain;

namespace Backend.Application.Contracts
{
    public interface IMedalhaRepository : IGenericRepository<MedalhaEntity>
    {
        Task<MedalhaEntity?> GetByPais(string pais);
    }
}

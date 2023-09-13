using Backend.Application.Contracts;
using Backend.Domain;
using Backend.Persistence.DatabaseContexts;
using Backend.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence.Repositories
{
    public class MedalhaRepository : GenericRepository<MedalhaEntity>, IMedalhaRepository
    {
        public MedalhaRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<MedalhaEntity?> GetByPais(string pais)
        {
            return await _dbSet.Where(p => p.Pais.Equals(pais)).FirstOrDefaultAsync();
        }
    }
}

using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace DocConnect.Data.Repositories
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> GetPatientIdByUserIdAsync(string userId)
        {
            return await DbSet()
                .Where(p => p.UserId == userId)
                .Select(p => p.Id)
                .SingleAsync();
        }
    }
}

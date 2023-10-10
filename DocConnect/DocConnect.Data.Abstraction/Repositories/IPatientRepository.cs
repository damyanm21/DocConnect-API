using DocConnect.Data.Models.Domains;

namespace DocConnect.Data.Abstraction.Repositories
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<int> GetPatientIdByUserIdAsync(string userId);
    }
}

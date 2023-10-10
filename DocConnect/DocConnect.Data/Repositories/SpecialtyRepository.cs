using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Domains;

namespace DocConnect.Data.Repositories
{
    public class SpecialtyRepository : Repository<Specialty>, ISpecialtyRepository
    {
        public SpecialtyRepository(ApplicationDbContext context)
            : base(context)
        {

        }
    }
}


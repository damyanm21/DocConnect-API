using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.Specialty;
using DocConnect.Data.Abstraction.Repositories;

namespace DocConnect.Business.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialtyService(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public async Task<IEnumerable<SpecialtyGetDTO>> GetAllAsync()
        {
            var specialties = await _specialtyRepository.AllReadonly();

            var specialtiesModel = specialties.Select(s => new SpecialtyGetDTO 
            {
                Id = s.Id,
                ImageUrl = s.ImageUrl,
                Name = s.Name
            })
            .OrderBy(s => s.Name)
            .ToList();

            return specialtiesModel;
        }
    }
}


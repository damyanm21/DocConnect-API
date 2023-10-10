using DocConnect.Business.Abstraction.Services;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Domains;

namespace DocConnect.Business.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task AddAsync(Patient patient)
        {
            await _patientRepository.AddAsync(patient);

            await _patientRepository.SaveChangesAsync();
        }
    }
}
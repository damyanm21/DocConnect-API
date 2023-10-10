using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.SpecialistDTOs;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Domains;
using static DocConnect.Data.Models.Utilities.Constants.DoctorConstants;
using static DocConnect.Data.Models.Utilities.Constants.EnvironmentalVariablesConstants;
using System.Text.RegularExpressions;

namespace DocConnect.Business.Services
{
    public class SpecialistService : ISpecialistService
    {
        private readonly ISpecialistRepository _specialistRepository;
        private readonly string ImageDomain = Environment.GetEnvironmentVariable(AzureImageDomain);

        public SpecialistService(ISpecialistRepository specialistRepository)
        {
            _specialistRepository = specialistRepository;
        }

        public async Task<IEnumerable<SpecialistNamesAndIdDTO>> GetSpecialistsNamesSuggestionsAsync(string startingWith)
        {
            string textWithoutWhiteSpaces = Regex.Replace(startingWith, @"\s", string.Empty);

            var dtos = await _specialistRepository.SpecialistsNamesSuggestionsAsync(textWithoutWhiteSpaces);

            return dtos;
        }

        public async Task<IEnumerable<SpecialistFilteredInfoDTO>> FilterSpecialistsByCriteriaAsync(string? name, int? specialtyId, int? cityId)
        {
            if (name != null)
            {
                name = Regex.Replace(name, @"\s", string.Empty);
            }

            var dtos = await _specialistRepository.FilterSpecialistsAsync(name, specialtyId, cityId);

            return dtos;
        }

        public async Task<SpecialistDetailedInfoDTO> GetSpecialistByIdAsync(int id)
        {
            var doctor = await _specialistRepository.GetSpecialistInfoByIdAsync(id);

            if(doctor == null)
            {
                return null;
            }

            var specialistDetailedInfoDto = new SpecialistDetailedInfoDTO
            {
                ImageUrl = FormatImageUrl(doctor.ImageUrl),
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                SpecialityName = doctor.Specialty.Name,
                Address = string.Format(FullAddressBuilder, doctor.Address, doctor.Location.CityName),
                DoctorSummary = doctor.DoctorSummary,
                Rating = CalculateDoctorRating(doctor.Ratings)
            };

            return specialistDetailedInfoDto;
        }

        private double CalculateDoctorRating(IEnumerable<Rating> ratings)
        {
            return ratings.Any() ? ratings.Average(r => r.RatingPoints) : 0;
        }

        private string FormatImageUrl(string imageUrl)
        {
            return string.Format(ImageUrlBuilder, ImageDomain, imageUrl);
        }
    }
}

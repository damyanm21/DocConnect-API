using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.Helpers.ResponseResult;
using DocConnect.Business.Models.SpecialistDTOs;
using DocConnect.Business.Services;
using DocConnect.Data.Models.Domains;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DocConnect.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistsController : ControllerBase
    {
        private readonly ISpecialistService _specialistService;
        private readonly ILocationService _locationService;

        public SpecialistsController(ISpecialistService specialistService, ILocationService locationService)
        {
            _specialistService = specialistService;
            _locationService = locationService;
        }

        [HttpGet("SpecialistNameSuggestion")]
        public async Task<ResponseModel> GetSpecialistNameSuggestion(string startingWith)
        {
            var dtos = await _specialistService.GetSpecialistsNamesSuggestionsAsync(startingWith);

            var result = HttpResponseHelper.Success(HttpStatusCode.OK, dtos);

            return result;
        }

        [HttpGet("CityNameSuggestion")]
        public async Task<ResponseModel> GetCityNameSuggestion(string startingWith)
        {
            var dtos = await _locationService.GetCitiesNamesSuggestionsAsync(startingWith);

            var result = HttpResponseHelper.Success(HttpStatusCode.OK, dtos);

            return result;
        }

        [HttpGet()]
        public async Task<ResponseModel> GetSpecialists(string? doctorName, int? specialtyId, int? cityId)
        {
            var dtos = await _specialistService.FilterSpecialistsByCriteriaAsync(doctorName, specialtyId, cityId);

            var result = HttpResponseHelper.Success(HttpStatusCode.OK, dtos);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseModel> GetSpecialistById(int id)
        {
            var dtos = await _specialistService.GetSpecialistByIdAsync(id);

            var result = HttpResponseHelper.Success(HttpStatusCode.OK, dtos);

            return result;
        }
    }
}

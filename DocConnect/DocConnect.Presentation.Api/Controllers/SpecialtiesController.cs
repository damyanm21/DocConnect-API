using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.Helpers.ResponseResult;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DocConnect.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtyService _specialtiesService;

        public SpecialtiesController(ISpecialtyService specialtiesService)
        {
            _specialtiesService = specialtiesService;
        }

        [HttpGet]
        public async Task<ResponseModel> Get()
        {
            var result = await _specialtiesService.GetAllAsync();

            return HttpResponseHelper.Success(HttpStatusCode.OK, result);
        }
    }
}

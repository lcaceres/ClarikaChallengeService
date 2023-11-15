using ClarikaChallengeService.Application.Interfaces;
using ClarikaChallengeService.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClarikaChallengeService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet("{cityId}")]
        public IActionResult GetById(int cityId)
        {
            CityDto cityDto = cityService.GetById(cityId);

            if (cityDto != null)
            {
                return Ok(cityDto);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("province/{provinceId}")]
        public IActionResult GetAll(int provinceId)
        {
            List<CityDto> cityDtos = cityService.GetAll(provinceId);

            return Ok(cityDtos);
        }
    }
}

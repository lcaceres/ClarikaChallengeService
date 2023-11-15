using ClarikaChallengeService.Application.Interfaces;
using ClarikaChallengeService.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClarikaChallengeService.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceService provinceService;

        public ProvinceController(IProvinceService provinceService)
        {
            this.provinceService = provinceService;
        }

        [HttpGet("{provinceId}")]
        public IActionResult GetById(int provinceId)
        {
            ProvinceDto provinceDto = provinceService.GetById(provinceId);

            if (provinceDto != null)
            {
                return Ok(provinceDto);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("country/{countryId}")]
        public IActionResult GetAll(int countryId)
        {
            List<ProvinceDto> provinceDtos = provinceService.GetAll(countryId);

            return Ok(provinceDtos);
        }
    }
}

using ClarikaChallengeService.Application.Interfaces;
using ClarikaChallengeService.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClarikaChallengeService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet("{countryId}")]
        public IActionResult GetById(int countryId)
        {
            CountryDto countryDto = countryService.GetById(countryId);

            if (countryDto != null)
            {
                return Ok(countryDto);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<CountryDto> countryDtos = countryService.GetAll();

            return Ok(countryDtos);
        }
    }
}

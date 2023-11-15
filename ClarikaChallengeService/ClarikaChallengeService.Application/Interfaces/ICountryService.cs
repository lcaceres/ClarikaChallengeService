using ClarikaChallengeService.Application.Models;

namespace ClarikaChallengeService.Application.Interfaces
{
    public interface ICountryService
    {
        CountryDto GetById(int countryId);
        List<CountryDto> GetAll();
    }
}

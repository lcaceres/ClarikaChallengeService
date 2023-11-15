using ClarikaChallengeService.Application.Models;

namespace ClarikaChallengeService.Application.Interfaces
{
    public interface ICityService
    {
        List<CityDto> GetAll(int? provinceId);
        CityDto GetById(int cityId);
    }
}

using ClarikaChallengeService.Repository.Models;

namespace ClarikaChallengeService.Repository.Interfaces
{
    public interface ICityRepository
    {
        City GetById(int cityId);
        List<City> GetAll(int? provinceId);
    }
}

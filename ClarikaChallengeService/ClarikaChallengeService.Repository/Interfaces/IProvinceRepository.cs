using ClarikaChallengeService.Repository.Models;

namespace ClarikaChallengeService.Repository.Interfaces
{
    public interface IProvinceRepository
    {
        List<Province> GetAll(int? countryId);
        Province GetById(int provinceId);
    }
}

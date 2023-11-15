using ClarikaChallengeService.Application.Models;

namespace ClarikaChallengeService.Application.Interfaces
{
    public interface IProvinceService
    {
        ProvinceDto GetById(int provinceId);
        List<ProvinceDto> GetAll(int? countryId);
    }
}


using ClarikaChallengeService.Repository.Models;

namespace ClarikaChallengeService.Repository.Interfaces
{
    public interface ICountryRepository
    {
        Country GetById(int countryId);
        List<Country> GetAll();
    }
}

using ClarikaChallengeService.Repository.Models;

namespace ClarikaChallengeService.Repository.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        User GetById(int userId);
        List<User> GetWithFilters(int page, int pageSize, int? age, int? countryId);
        User GetByUserName(string userName);
        void GenerateRandomUsers(int rowCount, string fixedPasswordHash);
    }
}

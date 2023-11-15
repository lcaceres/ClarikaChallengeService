
using ClarikaChallengeService.Application.Models;
using ClarikaChallengeService.Repository.Models;

namespace ClarikaChallengeService.Application.Interfaces
{
    public interface IUserService
    {
        UserDto Add(UserDto user, string password);
        List<UserDto> GetAll(int page, int pageSize, int? age, int? countryId);
        UserDto GetById(int userId);
        UserDto ValidateLogin(string userName, string password);
        void GenerateRandomUsers(int count);
    }
}

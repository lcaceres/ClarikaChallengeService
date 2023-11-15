using ClarikaChallengeService.Repository.Models;

namespace ClarikaChallengeService.Repository.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        User GetById(int userId);
        List<User> GetAll();
        User GetByUserName(string userName);
    }
}

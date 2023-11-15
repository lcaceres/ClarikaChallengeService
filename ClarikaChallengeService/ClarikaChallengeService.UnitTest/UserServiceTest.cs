using ClarikaChallengeService.Application.Interfaces;
using NUnit.Framework;
using Moq;
using ClarikaChallengeService.Application.Services;
using ClarikaChallengeService.Repository.Interfaces;

namespace ClarikaChallengeService.UnitTest
{
    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IUserRepository> userRepositoryMock;
        //private Mock<ICountryRepository> countryRepositoryMock;
        //private Mock<IProvinceRepository> provinceRepositoryMock;
        //private Mock<ICityRepository> cityRepositoryMock;
        //private Mock<IPasswordHashService> passwordHashServiceMock;

        private UserService userService;

        //[TestInitialize]
        //public void Setup()
        //{
        //    userRepositoryMock = new Mock<IUserRepository>();
        //    countryRepositoryMock = new Mock<ICountryRepository>();
        //    provinceRepositoryMock = new Mock<IProvinceRepository>();
        //    cityRepositoryMock = new Mock<ICityRepository>();
        //    passwordHashServiceMock = new Mock<IPasswordHashService>();

        //    userService = new UserService(
        //        userRepositoryMock.Object,
        //        countryRepositoryMock.Object,
        //        provinceRepositoryMock.Object,
        //        cityRepositoryMock.Object,
        //        passwordHashServiceMock.Object
        //    );
        //}

    }
}
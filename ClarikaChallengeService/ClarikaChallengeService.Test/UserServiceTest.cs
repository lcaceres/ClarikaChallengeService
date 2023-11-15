using ClarikaChallengeService.Application.Interfaces;
using ClarikaChallengeService.Application.Models;
using ClarikaChallengeService.Application.Services;
using ClarikaChallengeService.Infraestructure.Exceptions;
using ClarikaChallengeService.Repository.Interfaces;
using ClarikaChallengeService.Repository.Models;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ClarikaChallengeService.Test
{
    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IConfiguration> configurationMock;
        private Mock<IUserRepository> userRepositoryMock;
        private Mock<ICountryRepository> countryRepositoryMock;
        private Mock<IProvinceRepository> provinceRepositoryMock;
        private Mock<ICityRepository> cityRepositoryMock;
        private Mock<IPasswordHashService> passwordHashServiceMock;

        private UserService userService;

        [SetUp]
        public void Setup()
        {
            configurationMock = new Mock<IConfiguration>();
            userRepositoryMock = new Mock<IUserRepository>();
            countryRepositoryMock = new Mock<ICountryRepository>();
            provinceRepositoryMock = new Mock<IProvinceRepository>();
            cityRepositoryMock = new Mock<ICityRepository>();
            passwordHashServiceMock = new Mock<IPasswordHashService>();

            userService = new UserService(
                configurationMock.Object,
                passwordHashServiceMock.Object,
                userRepositoryMock.Object,
                countryRepositoryMock.Object,
                provinceRepositoryMock.Object,
                cityRepositoryMock.Object
            );
        }

        [Test]
        public void Add_ValidUserDto_CallsAddMethod()
        {
            // Arrange
            var userDto = new UserDto
            {
                UserName = "TestUser",
                FirstName = "John",
                LastName = "Doe",
                PasswordHash = "hashedPassword",
                Age = 25,
                BirthDate = DateTime.Now,
                CountryID = 1,
                ProvinceID = 2,
                CityID = 3
            };

            countryRepositoryMock.Setup(repo => repo.GetById(userDto.CountryID)).Returns(new Country());
            provinceRepositoryMock.Setup(repo => repo.GetById(userDto.ProvinceID)).Returns(new Province());
            cityRepositoryMock.Setup(repo => repo.GetById(userDto.CityID)).Returns(new City());

            passwordHashServiceMock.Setup(service => service.HashPassword(It.IsAny<string>())).Returns("hashedPassword");

            var result = userService.Add(userDto, "password");

            userRepositoryMock.Verify(repo => repo.Add(It.IsAny<User>()), Times.Once);
            Assert.AreEqual(userDto, result);
        }

        [Test]
        public void GetById_ValidUserId_ReturnsUserDto()
        {
            var userId = 1;
            userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns(new User
            {
                UserName = "Test1",
                Age = 10,
                BirthDate = DateTime.Now,
                FirstName = "Test2",
                LastName = "Test3",
                CityID = 1,
                CountryID = 1,
                ProvinceID = 1,
            });
            countryRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Country)null);
            provinceRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Province)null);
            cityRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((City)null);

            var result = userService.GetById(userId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<UserDto>(result);
        }

        [Test]
        [TestCase(-1)]
        public void GetById_InvalidUserId_ThrowsException(int userId)
        {
            Assert.Throws<BusinessRuleValidationException>(() => userService.GetById(userId));
        }

        [Test]
        public void ValidateLogin_ValidCredentials_ReturnsUserDto()
        {
            var userName = "validUser";
            var password = "validPassword";
            userRepositoryMock.Setup(repo => repo.GetByUserName(userName)).Returns(new User
            {
                UserName = userName,
                PasswordHash= password,
                Age = 10,
                BirthDate = DateTime.Now,
                FirstName = "Test2",
                LastName = "Test3",
                CityID = 1,
                CountryID = 1,
                ProvinceID = 1
            });
            passwordHashServiceMock.Setup(service => service.VerifyPassword(password, It.IsAny<string>())).Returns(true);
            countryRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Country)null);
            provinceRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Province)null);
            cityRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((City)null);

            var result = userService.ValidateLogin(userName, password);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<UserDto>(result);
        }

        [Test]
        [TestCase("", "validPassword")]
        [TestCase(null, "validPassword")]
        [TestCase("validUserName", null)]
        [TestCase("validUserName", "")]
        [TestCase("", "")]
        [TestCase(null, null)]
        public void ValidateLogin_InvalidUserName_ThrowsException(string userName, string password)
        {
            Assert.Throws<ApplicationArgumentException>(() => userService.ValidateLogin(userName, password));
        }
    }
}
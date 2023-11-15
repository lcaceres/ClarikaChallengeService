using ClarikaChallengeService.Application.Services;
using ClarikaChallengeService.Infraestructure.Exceptions;
using ClarikaChallengeService.Repository.Interfaces;
using ClarikaChallengeService.Repository.Models;
using Moq;

namespace ClarikaChallengeService.Test
{
    [TestFixture]
    public class CityServiceTest
    {
        private Mock<ICityRepository> cityRepositoryMock;

        private CityService cityService;

        [SetUp]
        public void Setup()
        {
            cityRepositoryMock = new Mock<ICityRepository>();
            cityService = new CityService(cityRepositoryMock.Object);
        }

        [Test]
        [TestCase(1, "TestCity", 1, "TestProvince")]
        [TestCase(2, "AnotherCity", 2, "AnotherProvince")]
        public void GetById_ValidCityId_ReturnsCityDto(int cityId, string cityName, int provinceId, string provinceName)
        {
            // Arrange
            var city = new City
            {
                CityID = cityId,
                CityName = cityName,
                ProvinceID = provinceId,
                Province = new Province
                {
                    ProvinceID = provinceId,
                    ProvinceName = provinceName
                }
            };
            cityRepositoryMock.Setup(repo => repo.GetById(cityId)).Returns(city);

            // Act
            var result = cityService.GetById(cityId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(cityId, result.CityID);
            Assert.AreEqual(cityName, result.CityName);
            Assert.IsNotNull(result.Province);
            Assert.AreEqual(provinceId, result.Province.ProvinceID);
            Assert.AreEqual(provinceName, result.Province.ProvinceName);
        }

        [Test]
        [TestCase(0)] 
        [TestCase(-1)] 
        public void GetById_InvalidCityId_ThrowsValidationException(int invalidCityId)
        {
            // Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => cityService.GetById(invalidCityId));
        }

        [Test]
        [TestCase(1)] 
        public void GetById_CityNotFound_ThrowsValidationException(int validCityId)
        {
            cityRepositoryMock.Setup(repo => repo.GetById(validCityId)).Returns((City)null);
            Assert.Throws<BusinessRuleValidationException>(() => cityService.GetById(validCityId));
        }
    }
}

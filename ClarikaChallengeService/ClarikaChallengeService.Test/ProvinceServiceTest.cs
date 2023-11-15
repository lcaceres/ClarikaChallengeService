using ClarikaChallengeService.Application.Services;
using ClarikaChallengeService.Infraestructure.Exceptions;
using ClarikaChallengeService.Repository.Interfaces;
using ClarikaChallengeService.Repository.Models;
using Moq;


namespace ClarikaChallengeService.Test
{
    [TestFixture]
    public class ProvinceServiceTest
    {
        private Mock<IProvinceRepository> provinceRepositoryMock;
        private ProvinceService provinceService;

        [SetUp]
        public void Setup()
        {
            provinceRepositoryMock = new Mock<IProvinceRepository>();
            provinceService = new ProvinceService(provinceRepositoryMock.Object);
        }

        [Test]
        [TestCase(1, "TestProvince", 1, "TestCountry")]
        [TestCase(2, "AnotherProvince", 2, "AnotherCountry")]
        public void GetById_ValidProvinceId_ReturnsProvinceDto(int provinceId, string provinceName, int countryId, string countryName)
        {
            var province = new Province
            {
                ProvinceID = provinceId,
                ProvinceName = provinceName,
                CountryID = countryId,
                Country = new Country
                {
                    CountryID = countryId,
                    CountryName = countryName
                }
            };
            provinceRepositoryMock.Setup(repo => repo.GetById(provinceId)).Returns(province);

            var result = provinceService.GetById(provinceId);

            Assert.IsNotNull(result);
            Assert.AreEqual(provinceId, result.ProvinceID);
            Assert.AreEqual(provinceName, result.ProvinceName);
            Assert.IsNotNull(result.Country);
            Assert.AreEqual(countryId, result.Country.CountryID);
            Assert.AreEqual(countryName, result.Country.CountryName);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)] 
        public void GetById_InvalidProvinceId_ThrowsValidationException(int invalidProvinceId)
        {
            Assert.Throws<ApplicationArgumentException>(() => provinceService.GetById(invalidProvinceId));
        }

        [Test]
        public void GetById_ProvinceNotFound_ThrowsValidationException()
        {
            var provinceId = 1;
            provinceRepositoryMock.Setup(repo => repo.GetById(provinceId)).Returns((Province)null);
            Assert.Throws<BusinessRuleValidationException>(() => provinceService.GetById(provinceId));
        }

        [Test]
        [TestCase(null)] 
        [TestCase(1)]
        public void GetAll_ReturnsListOfProvinceDto(int? countryId)
        {
            // Arrange
            var provinces = new List<Province>
            {
                new Province { ProvinceID = 1, ProvinceName = "TestProvince1", CountryID = 1, Country = new Country { CountryID = 1, CountryName = "TestCountry1" } },
                new Province { ProvinceID = 2, ProvinceName = "TestProvince2", CountryID = 1, Country = new Country { CountryID = 1, CountryName = "TestCountry1" } },
                new Province { ProvinceID = 3, ProvinceName = "TestProvince3", CountryID = 2, Country = new Country { CountryID = 2, CountryName = "TestCountry2" } },
            };
            provinceRepositoryMock.Setup(repo => repo.GetAll(countryId)).Returns(provinces);

            var result = provinceService.GetAll(countryId);

            Assert.IsNotNull(result);
            Assert.AreEqual(provinces.Count, result.Count);

            for (int i = 0; i < provinces.Count; i++)
            {
                Assert.AreEqual(provinces[i].ProvinceID, result[i].ProvinceID);
                Assert.AreEqual(provinces[i].ProvinceName, result[i].ProvinceName);
                Assert.IsNotNull(result[i].Country);
                Assert.AreEqual(provinces[i].Country.CountryID, result[i].Country.CountryID);
                Assert.AreEqual(provinces[i].Country.CountryName, result[i].Country.CountryName);
            }
        }
    }
}

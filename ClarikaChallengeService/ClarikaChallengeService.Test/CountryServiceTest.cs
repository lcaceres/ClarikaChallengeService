using ClarikaChallengeService.Application.Services;
using ClarikaChallengeService.Infraestructure.Exceptions;
using ClarikaChallengeService.Repository.Interfaces;
using ClarikaChallengeService.Repository.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarikaChallengeService.Test
{
    [TestFixture]
    public class CountryServiceTest
    {
        private Mock<ICountryRepository> countryRepositoryMock;
        private CountryService countryService;

        [SetUp]
        public void Setup()
        {
            countryRepositoryMock = new Mock<ICountryRepository>();
            countryService = new CountryService(countryRepositoryMock.Object);
        }

        [Test]
        [TestCase(1, "TestCountry")]
        [TestCase(2, "AnotherCountry")]
        public void GetById_ValidCountryId_ReturnsCountryDto(int countryId, string countryName)
        {
            // Arrange
            var country = new Country
            {
                CountryID = countryId,
                CountryName = countryName
            };
            countryRepositoryMock.Setup(repo => repo.GetById(countryId)).Returns(country);

            // Act
            var result = countryService.GetById(countryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(countryId, result.CountryID);
            Assert.AreEqual(countryName, result.CountryName);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void GetById_InvalidCountryId_ThrowsValidationException(int invalidCountryId)
        {
            Assert.Throws<BusinessRuleValidationException>(() => countryService.GetById(invalidCountryId));
        }

        [Test]
        public void GetById_CountryNotFound_ThrowsValidationException()
        {
            var countryId = 1;
            countryRepositoryMock.Setup(repo => repo.GetById(countryId)).Returns((Country)null);
            Assert.Throws<BusinessRuleValidationException>(() => countryService.GetById(countryId));
        }

        [Test]
        public void GetAll_ReturnsListOfCountryDto()
        {

            var countries = new List<Country>
            {
                new Country { CountryID = 1, CountryName = "TestCountry1" },
                new Country { CountryID = 2, CountryName = "TestCountry2" }
            };
            countryRepositoryMock.Setup(repo => repo.GetAll()).Returns(countries);
            var result = countryService.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(countries.Count, result.Count);

            for (int i = 0; i < countries.Count; i++)
            {
                Assert.AreEqual(countries[i].CountryID, result[i].CountryID);
                Assert.AreEqual(countries[i].CountryName, result[i].CountryName);
            }
        }
    }
}

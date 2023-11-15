using ClarikaChallengeService.Application.Interfaces;
using ClarikaChallengeService.Application.Models;
using ClarikaChallengeService.Infraestructure.Exceptions;
using ClarikaChallengeService.Infraestructure.Localization;
using ClarikaChallengeService.Repository.Interfaces;

namespace ClarikaChallengeService.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public CountryDto GetById(int countryId)
        {
            if (countryId <= 0)
            {
                throw new ApplicationArgumentException(SystemMessages.CountryIDNotNegative);
            }

            var country = countryRepository.GetById(countryId);

            if (country == null)
            {
                throw new BusinessRuleValidationException(SystemMessages.CountryNotFound);
            }

            CountryDto countryDto = new CountryDto
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName
            };

            return countryDto;

        }

        public List<CountryDto> GetAll()
        {
            var countries = countryRepository.GetAll();
            List<CountryDto> countryDtos = countries.Select(country => new CountryDto
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName,
            }).ToList();

            return countryDtos;
        }
    }
}

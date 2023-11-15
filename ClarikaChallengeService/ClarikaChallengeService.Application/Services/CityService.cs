using ClarikaChallengeService.Application.Interfaces;
using ClarikaChallengeService.Application.Models;
using ClarikaChallengeService.Infraestructure.Exceptions;
using ClarikaChallengeService.Infraestructure.Localization;
using ClarikaChallengeService.Repository.Interfaces;
using ClarikaChallengeService.Repository.Models;

namespace ClarikaChallengeService.Application.Services
{
    public class CityService: ICityService
    {
        private readonly ICityRepository cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public CityDto GetById(int cityId)
        {

            if (cityId <= 0)
            {
                throw new BusinessRuleValidationException(SystemMessages.CityIDNotNegative);
            }

            var city = cityRepository.GetById(cityId);

            if (city == null)
            {
                throw new BusinessRuleValidationException(SystemMessages.CityNotFound);
            }

            CityDto cityDto = new CityDto
            {
                CityID = city.CityID,
                CityName = city.CityName,
                ProvinceID = city.ProvinceID,
                Province = new ProvinceDto
                {
                    ProvinceID = city.Province.ProvinceID,
                    ProvinceName = city.Province.ProvinceName
                }
            };

            return cityDto;

        }

        public List<CityDto> GetAll(int? provinceId)
        {
            var cities = cityRepository.GetAll(provinceId);
            List<CityDto> cityDtos = cities.Select(city => new CityDto
            {
                CityID = city.CityID,
                CityName = city.CityName,
                ProvinceID= city.ProvinceID,
                Province = new ProvinceDto
                {
                    ProvinceID = city.Province.ProvinceID,
                    ProvinceName = city.Province.ProvinceName
                }
            }).ToList();

            return cityDtos;
        }
    }
}

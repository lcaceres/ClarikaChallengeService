using ClarikaChallengeService.Application.Interfaces;
using ClarikaChallengeService.Application.Models;
using ClarikaChallengeService.Infraestructure.Exceptions;
using ClarikaChallengeService.Infraestructure.Localization;
using ClarikaChallengeService.Repository.Interfaces;
using ClarikaChallengeService.Repository.Models;

namespace ClarikaChallengeService.Application.Services
{
    public class ProvinceService: IProvinceService
    {
        private readonly IProvinceRepository provinceRepository;

        public ProvinceService(IProvinceRepository provinceRepository)
        {
            this.provinceRepository = provinceRepository;
        }

        public ProvinceDto GetById(int provinceId)
        {
            if (provinceId <= 0)
            {
                throw new ApplicationArgumentException(SystemMessages.ProvinceIDNotNegative);
            }

            var province = provinceRepository.GetById(provinceId);

            if (province == null)
            {
                throw new BusinessRuleValidationException(SystemMessages.ProvinceNotFound);
            }

            ProvinceDto provinceDto = new ProvinceDto
            {
                ProvinceID = province.ProvinceID,
                ProvinceName = province.ProvinceName,
                CountryID = province.CountryID,
                Country = new CountryDto
                {
                    CountryID = province.Country.CountryID,
                    CountryName = province.Country.CountryName
                }
            };

            return provinceDto;

        }

        public List<ProvinceDto> GetAll(int? countryId)
        {
            var provinces = provinceRepository.GetAll(countryId);
            List<ProvinceDto> provinceDtos = provinces.Select(province => new ProvinceDto
            {
                ProvinceID = province.ProvinceID,
                ProvinceName = province.ProvinceName,
                CountryID= province.CountryID,
                Country= new CountryDto
                {
                    CountryID=province.Country.CountryID,
                    CountryName=province.Country.CountryName
                }
            }).ToList();

            return provinceDtos;
        }
    }
}

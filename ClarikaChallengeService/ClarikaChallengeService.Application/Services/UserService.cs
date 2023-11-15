using ClarikaChallengeService.Application.Interfaces;
using ClarikaChallengeService.Application.Models;
using ClarikaChallengeService.Infraestructure.Exceptions;
using ClarikaChallengeService.Infraestructure.Localization;
using ClarikaChallengeService.Repository.Interfaces;
using ClarikaChallengeService.Repository.Models;

namespace ClarikaChallengeService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHashService passwordHashService;
        private readonly IUserRepository userRepository;
        private readonly ICountryRepository countryRepository;
        private readonly IProvinceRepository provinceRepository;
        private readonly ICityRepository cityRepository;

        public UserService(IPasswordHashService passwordHashService, 
            IUserRepository userRepository, 
            ICountryRepository countryRepository, 
            IProvinceRepository provinceRepository, 
            ICityRepository cityRepository)
        {
            this.passwordHashService = passwordHashService;
            this.userRepository = userRepository;
            this.countryRepository = countryRepository;
            this.provinceRepository = provinceRepository;
            this.cityRepository = cityRepository;
        }

        public UserDto Add(UserDto userDto, string password)
        {
            var country = countryRepository.GetById(userDto.CountryID);
            if (country == null)
            {
                throw new BusinessRuleValidationException(SystemMessages.CountryNotFound);
            }
            var province = provinceRepository.GetById(userDto.ProvinceID);
            if (province == null)
            {
                throw new BusinessRuleValidationException(SystemMessages.ProvinceNotFound);
            }
            var city = cityRepository.GetById(userDto.CityID);
            if (city == null)
            {
                throw new BusinessRuleValidationException(SystemMessages.CityNotFound);
            }

            var user = new User
            {
                UserName = userDto.UserName,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                PasswordHash = passwordHashService.HashPassword(password),
                Age = userDto.Age,
                BirthDate = userDto.BirthDate,
                CountryID = userDto.CountryID,
                ProvinceID = userDto.ProvinceID,
                CityID = userDto.CityID
            };

            userRepository.Add(user); 

            return userDto;
        }

        public UserDto GetById(int userId)
        {
            if (userId <= 0)
            {
                throw new BusinessRuleValidationException(SystemMessages.UserIDNotNegative);
            }

            var user = userRepository.GetById(userId);

            if (user == null)
            {
                throw new BusinessRuleValidationException(SystemMessages.UserNotFound);
            }

            var userDto = new UserDto
            {
                UserID = user.UserID,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                BirthDate = user.BirthDate,
                CountryID = user.CountryID,
                ProvinceID = user.ProvinceID,
                CityID = user.CityID
            };
            var country = countryRepository.GetById(user.CountryID);
            if (country != null)
            {
                userDto.Country = new CountryDto
                {
                        CountryID = country.CountryID,
                        CountryName = country.CountryName,
                };
            }
            var province = provinceRepository.GetById(user.ProvinceID);
            if (province != null)
            {
                userDto.Province = new ProvinceDto
                {
                    ProvinceID = province.ProvinceID,
                    ProvinceName = province.ProvinceName,
                };
            }
            var city = cityRepository.GetById(user.CityID);
            if (city != null)
            {
                userDto.City = new CityDto
                {
                    CityID = city.CityID,
                    CityName = city.CityName,
                };
            }

            return userDto;

        }

        public List<UserDto> GetAll(int page, int pageSize, int? age, int? countryId)
        {
            var query = userRepository.GetAll().AsQueryable();

            if (age.HasValue)
            {
                query = query.Where(u => u.Age == age.Value);
            }

            if (countryId.HasValue)
            {
                query = query.Where(u => u.CountryID == countryId.Value);
            }

            var pagedResults = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var countries = countryRepository.GetAll();
            var cities= cityRepository.GetAll(null);
            var provinces = provinceRepository.GetAll(null);

            var userDtos = pagedResults.Select(user => new UserDto
            {
                UserID = user.UserID,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                BirthDate = user.BirthDate,
                CountryID = user.CountryID,
                ProvinceID = user.ProvinceID,
                CityID = user.CityID,
                Country = new CountryDto
                {
                    CountryID = user.CountryID,
                    CountryName = countries.FirstOrDefault(c => c.CountryID == user.CountryID)?.CountryName
                },
                Province = new ProvinceDto
                {
                    ProvinceID = user.ProvinceID,
                    ProvinceName = provinces.FirstOrDefault(c => c.ProvinceID == user.ProvinceID)?.ProvinceName
                },
                City = new CityDto
                {
                    CityID = user.CityID,
                    CityName = cities.FirstOrDefault(c => c.CityID == user.CityID)?.CityName
                }
            }).ToList();

            return userDtos;
        }

        public UserDto ValidateLogin(string userName, string password)
        {

            var user = userRepository.GetByUserName(userName);

            if (string.IsNullOrEmpty(userName))
            {
                throw new BusinessRuleValidationException(SystemMessages.UsernameNotEmpty);
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new BusinessRuleValidationException(SystemMessages.PasswordNotEmpty);
            }

            if (user == null)
            {
                throw new BusinessRuleValidationException(SystemMessages.UserNotFound);
            }

            if (passwordHashService.VerifyPassword(password, user.PasswordHash))
            {
                var userDto= new UserDto
                {
                    UserID = user.UserID,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age,
                    BirthDate = user.BirthDate,
                    CountryID = user.CountryID,
                    ProvinceID = user.ProvinceID,
                    CityID = user.CityID
                };

                var country = countryRepository.GetById(user.CountryID);
                if (country != null)
                {
                    userDto.Country = new CountryDto
                    {
                        CountryID = country.CountryID,
                        CountryName = country.CountryName,
                    };
                }
                var province = provinceRepository.GetById(user.ProvinceID);
                if (province != null)
                {
                    userDto.Province = new ProvinceDto
                    {
                        ProvinceID = province.ProvinceID,
                        ProvinceName = province.ProvinceName,
                    };
                }
                var city = cityRepository.GetById(user.CityID);
                if (city != null)
                {
                    userDto.City = new CityDto
                    {
                        CityID = city.CityID,
                        CityName = city.CityName,
                    };
                }

                return userDto;
            }

            return null;
        }

        public void GenerateRandomUsers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                UserDto randomUser = GenerateRandomUser();
                Add(randomUser, "randomPassword");
            }
        }

        private UserDto GenerateRandomUser()
        {
            Random random = new Random();

            UserDto randomUser = new UserDto
            {
                UserName = "User" + random.Next(10000),
                FirstName = "FirstName" + random.Next(10000),
                LastName = "LastName" + random.Next(10000),
                Age = random.Next(18, 60), 
                BirthDate = DateTime.Now.AddYears(-random.Next(18, 60)),
                CountryID = 1,
                ProvinceID = 1, 
                CityID = 1 
            };

            return randomUser;
        }

    }
}

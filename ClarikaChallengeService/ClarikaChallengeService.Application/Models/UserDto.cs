using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarikaChallengeService.Application.Models
{
    public class UserDto
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public int CountryID { get; set; }
        public CountryDto Country { get; set; }
        public int ProvinceID { get; set; }
        public ProvinceDto Province { get; set; }
        public int CityID { get; set; }
        public CityDto City { get; set; }
    }
}

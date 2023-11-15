using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarikaChallengeService.Application.Models
{
    public class ProvinceDto
    {
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public int CountryID { get; set; }
        public CountryDto Country { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarikaChallengeService.Application.Models
{
    public class CityDto
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int ProvinceID { get; set; }
        public ProvinceDto Province { get; set; }
    }
}

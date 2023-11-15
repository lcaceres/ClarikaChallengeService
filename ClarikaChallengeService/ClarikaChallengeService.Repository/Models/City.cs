namespace ClarikaChallengeService.Repository.Models
{
    public class City
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int ProvinceID { get; set; }
        public Province Province { get; set; }
    }
}

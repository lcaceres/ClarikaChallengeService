using ClarikaChallengeService.Repository.Interfaces;
using ClarikaChallengeService.Repository.Models;
using System.Data;

namespace ClarikaChallengeService.Repository.DAL
{
    public class CityRepository : ICityRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;
        private readonly IProvinceRepository provinceRepository;

        public CityRepository(IDbConnectionFactory dbConnectionFactory, IProvinceRepository provinceRepository)
        {
            this.dbConnectionFactory = dbConnectionFactory;
            this.provinceRepository = provinceRepository;
        }

        public City GetById(int cityId)
        {
            using (IDbConnection connection = dbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_cities_getById";  // Ajusta el nombre del procedimiento almacenado según tu implementación

                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@CityId";
                    parameter.Value = cityId;
                    command.Parameters.Add(parameter);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            City city = new City
                            {
                                CityID = (int)reader["CityID"],
                                CityName = (string)reader["CityName"],
                                ProvinceID = (int)reader["ProvinceID"]
                            };

                            city.Province = provinceRepository.GetById(city.ProvinceID);

                            return city;

                        }
                    }
                }
            }

            return null;
        }


        public List<City> GetAll(int? provinceId)
        {
            List<City> cities = new List<City>();

            using (IDbConnection connection = dbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_cities_getAll";


                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@ProvinceId";
                    parameter.Value = provinceId.HasValue?provinceId: DBNull.Value;
                    command.Parameters.Add(parameter);


                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            City city = new City
                            {
                                CityID = (int)reader["CityID"],
                                CityName = (string)reader["CityName"],
                                ProvinceID = (int)reader["ProvinceID"]
                            };

                            city.Province = provinceRepository.GetById(city.ProvinceID);

                            cities.Add(city);
                        }
                    }
                }
            }

            return cities;
        }
    }
}

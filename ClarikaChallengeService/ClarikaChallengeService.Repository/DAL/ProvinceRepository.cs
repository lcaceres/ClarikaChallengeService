using ClarikaChallengeService.Repository.Interfaces;
using ClarikaChallengeService.Repository.Models;
using System.Data;

namespace ClarikaChallengeService.Repository.DAL
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;
        private readonly ICountryRepository countryRepository;

        public ProvinceRepository(IDbConnectionFactory dbConnectionFactory, ICountryRepository countryRepository)
        {
            this.dbConnectionFactory = dbConnectionFactory;
            this.countryRepository = countryRepository;
        }

        public Province GetById(int provinceId)
        {
            using (IDbConnection connection = dbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_provinces_getById";  // Ajusta el nombre del procedimiento almacenado según tu implementación

                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@ProvinceId";
                    parameter.Value = provinceId;
                    command.Parameters.Add(parameter);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var province = new Province
                            {
                                ProvinceID = (int)reader["ProvinceID"],
                                ProvinceName = (string)reader["ProvinceName"],
                                CountryID = (int)reader["CountryID"]
                            };

                            province.Country = countryRepository.GetById(province.CountryID);
                            return province;
                        }
                    }
                }
            }

            return null;
        }

        public List<Province> GetAll(int? countryId)
        {
            List<Province> provinces = new List<Province>();

            using (IDbConnection connection = dbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_provinces_getAll";

                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@CountryId";
                    parameter.Value = countryId;
                    parameter.Value = countryId.HasValue ? countryId : DBNull.Value;
                    command.Parameters.Add(parameter);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Province province = new Province
                            {
                                ProvinceID = (int)reader["ProvinceID"],
                                ProvinceName = (string)reader["ProvinceName"],
                                CountryID = (int)reader["CountryID"]
                            };

                            province.Country = countryRepository.GetById(province.CountryID);
                            provinces.Add(province);
                        }
                    }
                }
            }

            return provinces;
        }
    }
}

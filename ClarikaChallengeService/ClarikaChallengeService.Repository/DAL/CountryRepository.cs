using ClarikaChallengeService.Repository.Interfaces;
using ClarikaChallengeService.Repository.Models;
using System.Data;

namespace ClarikaChallengeService.Repository.DAL
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public CountryRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public Country GetById(int countryId)
        {
            using (IDbConnection connection = dbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_countries_getById";  // Ajusta el nombre del procedimiento almacenado según tu implementación

                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@CountryId";
                    parameter.Value = countryId;
                    command.Parameters.Add(parameter);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Country
                            {
                                CountryID = (int)reader["CountryID"],
                                CountryName = (string)reader["CountryName"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        public List<Country> GetAll()
        {
            List<Country> countries = new List<Country>();

            using (IDbConnection connection = dbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_countries_getAll";

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Country country = new Country
                            {
                                CountryID = (int)reader["CountryID"],
                                CountryName = (string)reader["CountryName"]
                            };

                            countries.Add(country);
                        }
                    }
                }
            }

            return countries;
        }
    }
}

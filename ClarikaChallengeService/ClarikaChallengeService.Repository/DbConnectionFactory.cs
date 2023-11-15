using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ClarikaChallengeService.Repository
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        public IDbDataParameter CreateParameter(string name, object value)
        {
            IDbDataParameter parameter = CreateConnection().CreateCommand().CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }
    }
}

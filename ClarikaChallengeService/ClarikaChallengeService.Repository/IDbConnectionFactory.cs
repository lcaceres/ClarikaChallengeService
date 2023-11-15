using System.Data;

namespace ClarikaChallengeService.Repository
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
        IDbDataParameter CreateParameter(string name, object value);
    }
}

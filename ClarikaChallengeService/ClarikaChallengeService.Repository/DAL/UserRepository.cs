using ClarikaChallengeService.Repository.Interfaces;
using ClarikaChallengeService.Repository.Models;
using System.Data;
using System.Data.SqlClient;

namespace ClarikaChallengeService.Repository.DAL
{
    public class UserRepository: IUserRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public void Add(User user)
        {
            using (IDbConnection connection = dbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_users_add";

                    command.Parameters.Add(dbConnectionFactory.CreateParameter("@UserName", user.UserName));
                    command.Parameters.Add(dbConnectionFactory.CreateParameter("@FirstName", user.FirstName));
                    command.Parameters.Add(dbConnectionFactory.CreateParameter("@LastName", user.LastName));
                    command.Parameters.Add(dbConnectionFactory.CreateParameter("@PasswordHash", user.PasswordHash));
                    command.Parameters.Add(dbConnectionFactory.CreateParameter("@Age", user.Age));
                    command.Parameters.Add(dbConnectionFactory.CreateParameter("@BirthDate", user.BirthDate));
                    command.Parameters.Add(dbConnectionFactory.CreateParameter("@CountryID", user.CountryID));
                    command.Parameters.Add(dbConnectionFactory.CreateParameter("@ProvinceID", user.ProvinceID));
                    command.Parameters.Add(dbConnectionFactory.CreateParameter("@CityID", user.CityID));

                    command.ExecuteNonQuery();
                }
            }
        }

        public User GetById(int userId)
        {
            using (IDbConnection connection = dbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_users_getById";

                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@UserId";
                    parameter.Value = userId;
                    command.Parameters.Add(parameter);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User user = new User
                            {
                                UserID = (int)reader["UserID"],
                                UserName = (string)reader["UserName"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                PasswordHash = (string)reader["PasswordHash"],
                                Age = (int)reader["Age"],
                                BirthDate = (DateTime)reader["BirthDate"],
                                CountryID = (int)reader["CountryID"],
                                ProvinceID = (int)reader["ProvinceID"],
                                CityID = (int)reader["CityID"]
                            };

                            return user;
                        }
                    }
                }
            }

            return null;
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            using (IDbConnection connection = dbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_users_getAll";

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                UserID = (int)reader["UserID"],
                                UserName = (string)reader["UserName"],
                                FirstName= (string)reader["UserName"],
                                LastName = (string)reader["LastName"],
                                Age = (int)reader["Age"],
                                BirthDate = (DateTime)reader["BirthDate"],
                                CountryID= (int)reader["CountryID"],
                                ProvinceID = (int)reader["ProvinceID"],
                                CityID = (int)reader["CityID"]
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }


        public User GetByUserName(string userName)
        {
            using (IDbConnection connection = dbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_users_getByUsername";

                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@UserName";
                    parameter.Value = userName;
                    command.Parameters.Add(parameter);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User user = new User
                            {
                                UserID = (int)reader["UserID"],
                                UserName = (string)reader["UserName"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                PasswordHash = (string)reader["PasswordHash"],
                                Age = (int)reader["Age"],
                                BirthDate = (DateTime)reader["BirthDate"],
                                CountryID = (int)reader["CountryID"],
                                ProvinceID = (int)reader["ProvinceID"],
                                CityID = (int)reader["CityID"]
                            };

                            return user;
                        }
                    }
                }
            }

            return null;
        }
    }
}

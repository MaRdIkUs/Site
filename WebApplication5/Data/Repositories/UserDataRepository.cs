using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using WebApplication5.Data.Interfaces;
using WebApplication5.Data.Models;

namespace WebApplication5.Data.Repositories
{
    public class UserDataRepository : IUserData
    {
        public bool addUser(UserData user)
        {
            if(user == null || user.Password == null || user.Login == null)
                return false;
            using (SqlConnection connection = new SqlConnection(Startup.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_InsertUser", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter passwordParam = new SqlParameter
                {
                    ParameterName = "@Password",
                    Value = user.Password,
                };
                command.Parameters.Add(passwordParam);
                SqlParameter LoginParam = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = user.Login,
                };
                command.Parameters.Add(LoginParam);
                SqlParameter accessLevelParam = new SqlParameter
                {
                    ParameterName = "@AccessLevel",
                    Value = user.AccessLevel,
                };
                command.Parameters.Add(accessLevelParam);

                var result = command.ExecuteScalar();
                return true;
            }
        }

        public bool updateUser(string login, UserData user)
        {
            if (user == null || user.Password == null || user.Login == null)
                return false;
            using (SqlConnection connection = new SqlConnection(Startup.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_EditUser", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter passwordParam = new SqlParameter
                {
                    ParameterName = "@Password",
                    Value = user.Password,
                };
                command.Parameters.Add(passwordParam);
                SqlParameter LoginParam = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = user.Login,
                };
                command.Parameters.Add(LoginParam);
                SqlParameter accessLevelParam = new SqlParameter
                {
                    ParameterName = "@AccessLevel",
                    Value = user.AccessLevel,
                };
                command.Parameters.Add(accessLevelParam);
                var result = command.ExecuteNonQuery();
                return true;
            }
        }

        public List<UserData> GetUsers()
        {
            List<UserData> users = new List<UserData>();
            using (SqlConnection connection = new SqlConnection(Startup.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_GetUsers", connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    
                    while (reader.Read())
                    {
                        UserData userData = new UserData();
                        userData.AccessLevel = reader.GetInt32(0);
                        userData.Login = reader.GetString(1);
                        userData.Password = reader.GetString(2);
                        users.Add(userData);
                    }
                }
                reader.Close();
            }
            return users;
        }

        public bool removeUser(UserData user)
        {
            if (user == null  || user.Password == null || user.Login == null)
                return false;
            using (SqlConnection connection = new SqlConnection(Startup.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_DeleteUser", connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // параметр для ввода имени
                SqlParameter usernameParam = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = user.Login,
                };
                command.Parameters.Add(usernameParam);
                var result = command.ExecuteScalar();
                return true;
            }
        }

        public UserData GetUser(string login) {
            if (login == null)
                return null;
            using (SqlConnection connection = new SqlConnection(Startup.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_GetUser", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter LoginParam = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = login,
                };
                command.Parameters.Add(LoginParam);
                var reader = command.ExecuteReader();
                UserData user = new UserData();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        UserData userData = new UserData();
                        userData.Login = reader.GetString(0);
                        userData.Password = reader.GetString(1);
                        userData.AccessLevel = reader.GetInt32(2);
                        user = userData;
                    }
                }
                reader.Close();
                return user;
            }
        }
    }
}

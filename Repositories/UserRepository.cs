
using System.Data.SqlClient;
using Mao_Na_Massa_blog.Models;

namespace Mao_Na_Massa_blog.Repositories
{
    public class UserRepository
    {
        private readonly SqlConnection _connection;

        public UserRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<User> Buscar()
        {
            // User user = new User();
            List<User> users = new List<User>();

            using (_connection)
            {
                _connection.Open();
                using (var command = new SqlCommand())
                {
                    try
                    {
                        var sql =
                        @"SELECT 
	                        [Id]
	                        ,[Name]
	                        ,[Email]
	                        ,[PasswordHash]
	                        ,[Bio]
	                        ,[Image]
	                        ,[Slug]
	                    FROM [Blog].[dbo].[User]";

                        command.Connection = _connection;
                        command.CommandText = sql;
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var user = new User();
                            user.Id = Convert.ToInt32(reader["Id"]);
                            user.Name = Convert.ToString(reader["Name"]);
                            user.Email = Convert.ToString(reader["Email"]);
                            user.PasswordHash = Convert.ToString(reader["PasswordHash"]);
                            user.Bio = Convert.ToString(reader["Bio"]);
                            user.Image = Convert.ToString(reader["Image"]);
                            user.Slug = Convert.ToString(reader["Slug"]);
                            users.Add(user);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine($"E500 - Erro interno no servidor, Mensagem: {ex.Message}");
                    }
                }
                return users;

            }
        }
    }
}
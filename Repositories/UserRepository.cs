
using System.Reflection.Metadata.Ecma335;
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
            try
            {
                using (_connection)
                {
                    using (var command = new SqlCommand())
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
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"E500 - Erro interno no servidor, Mensagem: {ex.Message}");

            }
            return users;
        }


        public User Busca(int id)
        {
            var user = new User();
            try
            {
                using (_connection)
                {
                    using (var command = new SqlCommand())
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
		                    FROM [Blog].[dbo].[User]
		                    WHERE [Id] = @Id";

                        command.Parameters.AddWithValue("@Id", id);
                        command.Connection = _connection;
                        command.CommandText = sql;
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            user.Id = Convert.ToInt32(reader["Id"]);
                            user.Name = Convert.ToString(reader["Name"]);
                            user.Email = Convert.ToString(reader["Email"]);
                            user.PasswordHash = Convert.ToString(reader["PasswordHash"]);
                            user.Bio = Convert.ToString(reader["Bio"]);
                            user.Image = Convert.ToString(reader["Image"]);
                            user.Slug = Convert.ToString(reader["Slug"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"E501 - Erro interno no servidor, Mensagem: {ex.Message}");

            }
            return user;
        }

        public bool Inserir(User usuario)
        {
            try
            {
                using (_connection)
                {
                    using (var command = new SqlCommand())
                    {
                        var sql =
                        @"
                        INSERT INTO [Blog].[dbo].[User] 
                            VALUES
                            (
                                @Name, 
                                @Email, 
                                @PasswordHash, 
                                @Bio, 
                                @Image, 
                                @Slug
                            )";

                        command.Parameters.AddWithValue("@Name", usuario.Name);
                        command.Parameters.AddWithValue("@Email", usuario.Email);
                        command.Parameters.AddWithValue("@PasswordHash", usuario.PasswordHash);
                        command.Parameters.AddWithValue("@Bio", usuario.Bio);
                        command.Parameters.AddWithValue("@Image", usuario.Image);
                        command.Parameters.AddWithValue("@Slug", usuario.Slug);
                        command.Connection = _connection;
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                        // int result = command.ExecuteNonQuery();
                        // Console.WriteLine($"{result} registro afetados");
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"E502 - Erro interno no servidor, Mensagem: {ex.Message}");
                return false;
            }

            return true;
        }

        public bool Atualizar(int id, User usuario)
        {
            try
            {
                using (_connection)
                {

                    using (var command = new SqlCommand())
                    {
                        // recuperando o usuario
                        var selectSql =
                        @"SELECT 
	                        [Id]
	                        ,[Name]
	                        ,[Email]
	                        ,[PasswordHash]
	                        ,[Bio]
	                        ,[Image]
	                        ,[Slug]
	                    FROM [Blog].[dbo].[User]
	                    WHERE [Id] = @Id";

                        command.Parameters.AddWithValue("@Id", id);
                        command.Connection = _connection;
                        command.CommandText = selectSql;
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var antigoUsuario = new User();
                            antigoUsuario.Id = reader.GetInt32(0);
                            antigoUsuario.Name = reader.GetString(1);
                            antigoUsuario.Email = reader.GetString(2);
                            antigoUsuario.PasswordHash = reader.GetString(3);
                            antigoUsuario.Bio = reader.GetString(4);
                            antigoUsuario.Image = reader.GetString(5);
                            antigoUsuario.Slug = reader.GetString(6);

                            Console.WriteLine($"Id: {antigoUsuario.Id}, Nome: {antigoUsuario.Name}");
                        }
                        reader.Close();

                        // atualizando o dados do usuario
                        var insertSql =
                        @"
	                     UPDATE [Blog].[dbo].[User] 
	                        SET Name = @Name
	                    WHERE Id = @Id";

                        // command.Parameters.AddWithValue("@Id", user.Id);
                        command.Parameters.AddWithValue("@Name", usuario.Name);

                        command.Connection = _connection;
                        command.CommandText = insertSql;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E502 - Erro interno no servidor, Mensagem: {ex.Message}");
                return false;
            }
            return true;
        }

        public bool Deletar(int id)
        {
            try
            {
                var usuario = new User();
                using (_connection)
                {
                    using (var command = new SqlCommand())
                    {
                        // recuperando o usuario
                        var selectSql =
                        @"SELECT 
		                        [Id]
		                        ,[Name]
		                        ,[Email]
		                        ,[PasswordHash]
		                        ,[Bio]
		                        ,[Image]
		                        ,[Slug]
		                    FROM [Blog].[dbo].[User]
		                    WHERE [Id] = @Id";

                        command.Parameters.AddWithValue("@Id", id);
                        command.Connection = _connection;
                        command.CommandText = selectSql;
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {

                            usuario.Id = reader.GetInt32(0);
                            usuario.Name = reader.GetString(1);
                            usuario.Email = reader.GetString(2);
                            usuario.PasswordHash = reader.GetString(3);
                            usuario.Bio = reader.GetString(4);
                            usuario.Image = reader.GetString(5);
                            usuario.Slug = reader.GetString(6);

                            Console.WriteLine($"Id: {usuario.Id}, Nome: {usuario.Name}");
                        }
                        reader.Close();

                        if (usuario.Id == 0  || usuario.Name == null)
                        {
                            return false;
                        }
                        else
                        {
                            // deletando usuario
                            var sql =
                            @"DELETE FROM [Blog].[dbo].[User]
	                    WHERE [Id] = @IdUsuario";

                            command.Parameters.AddWithValue("@IdUsuario", id);
                            command.Connection = _connection;
                            command.CommandText = sql;
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"E502 - Erro interno no servidor, Mensagem: {ex.Message}");
                return false;
            }
            return true;
        }
    }

}

using System.Data.SqlClient;
using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;

namespace Mao_Na_Massa_blog
{
    public class Program
    {
        const string CONNECTION_STRING
            = @"Server=TERMINAL01\SQLEXPRESS;Database=Blog;User ID=admin;Password=12345;Trusted_Connection=false;TrustServerCertificate=true";

        static void Main(string[] args)
        {   
            var connection = new SqlConnection(CONNECTION_STRING);
            connection.Open();

            ListarUsuarios(connection);
            // ListarUsuario();
            // CriarUsuario();
            // AtualizarUsuario();
            // ApagarUsuario();

            connection.Close();
        }

        public static void ListarUsuarios(SqlConnection connection)
        {
            UserRepository repositorio = new UserRepository(connection);
            var  usuarios = repositorio.Buscar();

            foreach (var usuario in usuarios)
                Console.WriteLine($"Id: {usuario.Id}, Nome: {usuario.Name}, E-mail: {usuario.Email}");
                
            
        }

        public static void ListarUsuario()
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
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

                    command.Parameters.AddWithValue("@Id", 2);
                    command.Connection = connection;
                    command.CommandText = sql;
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var user = new User();
                        user.Id = reader.GetInt32(0);
                        user.Name = reader.GetString(1);
                        user.Email = reader.GetString(2);
                        user.PasswordHash = reader.GetString(3);
                        user.Bio = reader.GetString(4);
                        user.Image = reader.GetString(5);
                        user.Slug = reader.GetString(6);

                        Console.WriteLine($"Id: {user.Id}, Nome: {user.Name}");
                    }
                }
            }
        }

        public static void CriarUsuario()
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    var usuario = new User()
                    {
                        Name = "Bruno",
                        Email = "nevesbruno@gmail.com",
                        PasswordHash = "1234",
                        Bio = "Bruno-dev",
                        Image = "https://...",
                        Slug = "bruno Neves"
                    };

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
                    command.Connection = connection;
                    command.CommandText = sql;
                    int result = command.ExecuteNonQuery();

                    Console.WriteLine($"{result} registro afetados");
                }
            }
        }

        public static void AtualizarUsuario()
        {   
            var user = new User();
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
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

                    command.Parameters.AddWithValue("@Id", 2);
                    command.Connection = connection;
                    command.CommandText = selectSql;
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        user.Id = reader.GetInt32(0);
                        user.Name = reader.GetString(1);
                        user.Email = reader.GetString(2);
                        user.PasswordHash = reader.GetString(3);
                        user.Bio = reader.GetString(4);
                        user.Image = reader.GetString(5);
                        user.Slug = reader.GetString(6);

                        Console.WriteLine($"Id: {user.Id}, Nome: {user.Name}");
                    }
                    reader.Close();
                    user.Name = "Bruno";

                    var insertSql =
                    @"
                     UPDATE [Blog].[dbo].[User] 
                        SET Name = @Name
                    WHERE Id = @Id";

                    

                    // command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Name", user.Name);

                    command.Connection = connection;
                    command.CommandText = insertSql;
                    int result = command.ExecuteNonQuery();

                    Console.WriteLine($"{result} registro afetados");
                }
            }
        }

        public static void ApagarUsuario()
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                var user = new User();
                user.Id = 2;
                using (var command = new SqlCommand())
                {

                    var sql =
                    @"DELETE FROM [Blog].[dbo].[User]
                    WHERE [Id] = @Id";

                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Connection = connection;
                    command.CommandText = sql;
                    
                    int linha = command.ExecuteNonQuery();
                    Console.WriteLine($"{linha} afetadas");
                    
                }
            }
        }
    }
}


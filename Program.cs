
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
            User usuario = new User("Fabio", "nevesfabio@gmail.com", "1234", "Fabio-dev", "https://...", "Fabio Neves");
            var connection = new SqlConnection(CONNECTION_STRING);
            connection.Open();

            // ListarUsuarios(connection);
            ListarUsuario(connection, 1);
            // CriarUsuario(usuario, connection);
            // AtualizarUsuario(connection, usuario, 1);
            // ApagarUsuario();

            connection.Close();
        }

        public static void ListarUsuarios(SqlConnection connection)
        {
            UserRepository repositorio = new UserRepository(connection);
            var usuarios = repositorio.Buscar();

            if (usuarios == null)
            {
                Console.WriteLine($"Não existe usuarios cadastrados");
            }
            else
                foreach (var usuario in usuarios)
                    Console.WriteLine($"Id: {usuario?.Id}, Nome: {usuario?.Name}, E-mail: {usuario?.Email}");
        }

        public static void ListarUsuario(SqlConnection connection, int id)
        {
            UserRepository repositorio = new UserRepository(connection);
            var usuario = repositorio.Busca(id);

            if (usuario.Id == 0 || usuario.Name == null)
            {
                Console.WriteLine($"Usuario nao encontrado");
            }
            else
                Console.WriteLine($"Id: {usuario.Id}, Nome: {usuario.Name}, E-mail: {usuario.Email}");
        }

        public static void CriarUsuario(User usuario, SqlConnection connection)
        {
            UserRepository repositorio = new UserRepository(connection);
            var resultado = repositorio.Inserir(usuario);
            if (resultado)
            {
                Console.WriteLine($"Usuario criado com sucesso");
            }
            else
                Console.WriteLine($"Erro ao criar Usuario");
        }

        public static void AtualizarUsuario(SqlConnection connection, User usuario, int id)
        {
            UserRepository repositorio = new UserRepository(connection);
            var result = repositorio.Atualizar(id, usuario);
            if (result)
            {
                Console.WriteLine($"Usuarioatualizado com sucesso");
            }
            else
                Console.WriteLine($"Erro ao atualizar o Usuario");
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


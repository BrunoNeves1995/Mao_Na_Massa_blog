
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
            User usuario = new User("vinicius", "nevesvinicius@gmail.com", "1234", "vinicius-dev", "https://...", "vinicius Neves");
            var connection = new SqlConnection(CONNECTION_STRING);


            connection.Open();

            // ListarUsuarios(connection);
            // ListarUsuario(connection, 1);
            // CriarUsuario(usuario, connection);
            // AtualizarUsuario(connection, usuario, 1);
            //ApagarUsuario(connection, 1);

            connection.Close();
        }

        public static void ListarUsuarios(SqlConnection connection)
        {
            try
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
            catch(System.Exception ex)
            {

                Console.WriteLine($"E5003 - Erro interno no servidor, Mensagem: {ex.Message}");
            }
        }

        public static void ListarUsuario(SqlConnection connection, int id)
        {
            UserRepository repositorio = new UserRepository(connection);
            var usuario = repositorio.Busca(id);

            try
            {
                if (usuario == null)
                {
                    Console.WriteLine($"Usuario nao encontrado");
                }
                else
                    Console.WriteLine($"Id: {usuario.Id}, Nome: {usuario.Name}, E-mail: {usuario.Email}");
            }
            catch(System.Exception ex)
            {

                Console.WriteLine($"E5003 - Erro interno no servidor, Mensagem: {ex.Message}");
            }
        }

        public static void CriarUsuario(User usuario, SqlConnection connection)
        {
            try
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
            catch(System.Exception ex)
            {

                Console.WriteLine($"E5003 - Erro interno no servidor, Mensagem: {ex.Message}");
            }
        }

        public static void AtualizarUsuario(SqlConnection connection, User usuario, int id)
        {

            try
            {
                UserRepository repositorio = new UserRepository(connection);
                var result = repositorio.Atualizar(id, usuario);

                if (result)
                {
                    Console.WriteLine($"Usuario {usuario.Name} atualizado com sucesso");
                }
                else
                    Console.WriteLine($"Erro ao atualizar o Usuario");
            }
            catch(System.Exception ex)
            {

                Console.WriteLine($"E5003 - Erro interno no servidor, Mensagem: {ex.Message}");
            }
        }


        public static void ApagarUsuario(SqlConnection connection, int id)
        {
            try
            {
                UserRepository repositorio = new UserRepository(connection);
                var resultado = repositorio.Deletar(id);

                if (resultado)
                {
                    Console.WriteLine($"Usuario com {id} foi deletado com sucesso");
                }
                else
                    Console.WriteLine($"Erro ao excluir usuario");
            }
            catch(System.Exception ex)
            {

                Console.WriteLine($"E5003 - Erro interno no servidor, Mensagem: {ex.Message}");
            }
        }
    }
}


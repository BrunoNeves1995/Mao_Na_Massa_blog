
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
            User usuario = new("vinicius", "nevesvinicius@gmail.com", "1234", "vinicius-dev", "https://...", "vinicius Neves");
            Role autor = new(name: "Bruno Neves", slug: "Autor");

            var connection = new SqlConnection(CONNECTION_STRING);


            connection.Open();

            //ListarUsuarios(connection);
            //ListarUsuario(connection, 4);
            // CriarUsuario(usuario, connection);
            AtualizarUsuario(connection, usuario, 1);
            //ApagarUsuario(connection, 1);

            //ListarAutores(connection);
            //ListarAutor(connection, 2);
            //CriarAutor(connection, autor);
            AtualizarAutor(connection, autor, 1);
            //ApagarAutor(connection, 1);

            connection.Close();
        }

        // USER
        public static void ListarUsuarios(SqlConnection connection)
        {
            try
            {
                UserRepository repositorio = new UserRepository(connection);
                var usuarios = repositorio.Buscar();

                if (usuarios == null)
                {
                    Console.WriteLine($"Não existe usuario cadastrado");
                }
                else
                    foreach (var usuario in usuarios)
                        Console.WriteLine($"Id: {usuario.Id}, Nome: {usuario.Name}, E-mail: {usuario.Email}");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"OCorreu um erro interno no servidor ao buscar os usuarios, Mensagem: {ex.Message}");
            }
        }

        public static void ListarUsuario(SqlConnection connection, int id)
        {
            UserRepository repositorio = new UserRepository(connection);
            var usuario = repositorio.Busca(id);

            try
            {
                if (usuario.Id == 0 || usuario.Name == null)
                {
                    Console.WriteLine($"Usuario nao encontrado");
                }
                else
                    Console.WriteLine($"Id: {usuario.Id}, Nome: {usuario.Name}, E-mail: {usuario.Email}");
            }
            catch (System.Exception ex)
            {

                Console.WriteLine($"Erro interno no servidor, Mensagem: {ex.Message}");
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
            catch (System.Exception ex)
            {

                Console.WriteLine($"Ocorreu um erro interno no servidor ao criar um novo usuario, Mensagem: {ex.Message}");
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
                    Console.WriteLine($"Usuario {usuario.Name} foi atualizado com sucesso");
                }
                else
                    Console.WriteLine($"Erro ao atualizar o usuario");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Ocorreu um erro interno no servidor ao atualizar o usuario, Mensagem: {ex.Message}");
            }
        }


        public static void ApagarUsuario(SqlConnection connection, int id)
        {
            try
            {
                UserRepository repositorio = new UserRepository(connection);
                var resultado = repositorio.Deletar(id);

                if (resultado)
                    Console.WriteLine($"Usuario com {id} foi deletado com sucesso");
                else
                    Console.WriteLine($"Erro ao excluir usuario");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Ocorreu um erro interno no servidor ao excluir o usuario, Mensagem: {ex.Message}");
            }
        }

        // ROLE
        public static void ListarAutores(SqlConnection connection)
        {
            try
            {
                RoleRepository repositorio = new(connection);
                var autores = repositorio.Buscar();

                if (autores.Count() == 0)
                {
                    Console.WriteLine($"Autor não cadastrado");
                }
                else
                    foreach (var autor in autores)
                        Console.WriteLine($"Id: {autor.Id}, Nome: {autor.Name}, Slug: {autor.Slug}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OCorreu um erro interno no servidor ao buscar os autores, Mensagem: {ex.Message}");
            }
        }

        public static void ListarAutor(SqlConnection connection, int id)
        {
            try
            {
                RoleRepository repositorio = new(connection);
                Role autor = repositorio.Busca(id);

                if (autor.Id == 0 || autor.Name == null)
                    Console.WriteLine($"Autor nao encontrado");
                else
                    Console.WriteLine($"Id: {autor.Id}, Nome: {autor.Name}, Slug: {autor.Slug}");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"OCorreu um erro interno no servidor ao busca o autor, Mensagem: {ex.Message}");
            }
        }

        public static void CriarAutor(SqlConnection connection, Role autor)
        {
            try
            {
                RoleRepository repositorio = new(connection);
                bool resultado = repositorio.Inserir(autor);

                if (resultado)
                    Console.WriteLine($"Autor criado com sucesso");
                else
                    Console.WriteLine($"Erro ao criar autor");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Ocorreu um erro interno no servidor ao criar um novo autor, Mensagem: {ex.Message}");
            }
        }

        public static void AtualizarAutor(SqlConnection connection, Role autor, int id)
        {
            try
            {
                RoleRepository repositorio = new(connection);
                bool resultado = repositorio.Atualizar(autor, id);

                if (resultado)
                    Console.WriteLine($"Autor {autor.Name} foi atualizado com sucesso");
                else
                    Console.WriteLine($"Erro ao atualizar o autor");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro interno no servidor ao atualizar o autor, Mensagem: {ex.Message}");
            }
        }

        public static void ApagarAutor(SqlConnection connection, int id)
        {
            try
            {
                RoleRepository repositorio = new(connection);
                bool resutado = repositorio.Deletar(id);

                if (resutado)
                    Console.WriteLine($"Autor com {id} foi deletado com sucesso");
                else
                    Console.WriteLine($"Erro ao excluir o autor");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro interno no servidor ao excluir o autor, Mensagem: {ex.Message}");
            }
        }
    }
}


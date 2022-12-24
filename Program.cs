
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
            Role cargo = new(name: "Programado C#", slug: "Programado-C#");
            
            var connection = new SqlConnection(CONNECTION_STRING);


            connection.Open();

            // ListarUsuarios(connection);
            // ListarUsuario(connection, 1);
            // CriarUsuario(usuario, connection);
            // AtualizarUsuario(connection, usuario, 1);
            //ApagarUsuario(connection, 1);

            //ListarCargos(connection);
            //ListarCargo(connection, 1);
            //CriarCargo(connection, cargo);
            //AtualizarCargo(connection, cargo, 1);
            ApagarCargo(connection, 1);

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
            catch(Exception ex)
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
            catch(System.Exception ex)
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
            catch(System.Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
            {

                Console.WriteLine($"Ocorreu um erro interno no servidor ao excluir o usuario, Mensagem: {ex.Message}");
            }
        }
    
        // ROLE
        public static void ListarCargos(SqlConnection connection)
        {
            try
            {
                RoleRepository repositorio = new (connection);
                var cargos = repositorio.Buscar();

                if (cargos.Count() == 0)
                {
                    Console.WriteLine($"Cargos não cadastrado");
                }
                else
                    foreach (var cargo in cargos)
                        Console.WriteLine($"Id: {cargo.Id}, Nome: {cargo.Name}, Slug: {cargo.Slug}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OCorreu um erro interno no servidor ao buscar os cargos, Mensagem: {ex.Message}");
            }
        }

        public static void ListarCargo(SqlConnection connection, int id)
        {
            try
            {
                RoleRepository repositorio = new(connection);
                Role cargo = repositorio.Busca(id);

                if(cargo.Id == 0 || cargo.Name == null)
                    Console.WriteLine($"Cargo nao encontrado");
                else
                    Console.WriteLine($"Id: {cargo.Id}, Nome: {cargo.Name}, Slug: {cargo.Slug}");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"OCorreu um erro interno no servidor ao busca o cargo, Mensagem: {ex.Message}");
            }
        }

        public static void CriarCargo(SqlConnection connection, Role cargo)
        {
            try
            {
                RoleRepository repositorio = new(connection);
                bool resultado = repositorio.Inserir(cargo);

                if (resultado)
                    Console.WriteLine($"Cargo criado com sucesso");    
                else
                    Console.WriteLine($"Erro ao criar Usuario");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Ocorreu um erro interno no servidor ao criar um novo cargo, Mensagem: {ex.Message}");
            }
        }

        public static void AtualizarCargo(SqlConnection connection, Role cargo, int id)
        {
            try
            {
                RoleRepository repositorio = new(connection);
                bool resultado = repositorio.Atualizar(cargo, id);

                if(resultado)
                    Console.WriteLine($"Cargo {cargo.Name} foi atualizado com sucesso");
                else
                    Console.WriteLine($"Erro ao atualizar o cargo");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro interno no servidor ao atualizar o cargo, Mensagem: {ex.Message}");
            }
        }

        public static void ApagarCargo(SqlConnection connection, int id)
        {
            try
            {
                RoleRepository repositorio = new(connection);
                bool resutado = repositorio.Deletar(id);

                if (resutado)
                    Console.WriteLine($"Cargo com {id} foi deletado com sucesso");
                else
                    Console.WriteLine($"Erro ao excluir o cargo");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro interno no servidor ao excluir o cargo, Mensagem: {ex.Message}");
            }
        }
    }
}


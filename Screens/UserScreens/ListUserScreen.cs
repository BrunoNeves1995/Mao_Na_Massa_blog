using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;

namespace Mao_Na_Massa_blog.Screens.UserScreens
{
    public class ListUserScreen
    {
        public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Lista de usuario");
            Console.WriteLine("-------------");
            ListarUsuario();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Deseja Voltar ou Menu? (1 = sim, 2= nao)");
            var opcao = short.Parse(Console.ReadLine()!);
            if (opcao == 1)
                MenuUserScreen.CarregartelaPincipalUser();
            else
            {
                Console.WriteLine("Aperte enter 2x para fechar");
                if ("Enter" == Convert.ToString(ConsoleKey.Enter))
                    Console.ReadKey();
            }

        }

        private static void ListarUsuario()
        {
            try
            {
                UserRepository repositorio = new(Database.Conexao);
                IEnumerable<User> usuarios = repositorio.Buscar().OrderByDescending(x => x.Id);

                if(usuarios.Count() == 0)
                    Console.WriteLine("Usuario nao cadastrados");
                else
                    foreach (var usuario in usuarios)
                        Console.WriteLine($"{usuario.Id} - {usuario.Name} - {usuario.Email} - ({usuario.Slug})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao buscar os usuarios, Mensagem: {ex.Message}");
            }



        }
    }
}
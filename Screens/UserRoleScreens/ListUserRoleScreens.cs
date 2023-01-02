using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;
using Mao_Na_Massa_blog.Screens.CategoryScreens;

namespace Mao_Na_Massa_blog.Screens.UserRoleScreens
{
    public static class ListUserRoleScreens
    {
        public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Lista de perfis/usuarios");
            Console.WriteLine("-------------");
            ListarPerfisDosUsuarios();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Deseja Voltar ou Menu? (1 = sim, 2= nao)");
            var opcao = short.Parse(Console.ReadLine()!);
            if (opcao == 1)
                MenuUserRoleScreens.CarregartelaPincipalUserRole();
            else
            {
                Console.WriteLine("Aperte enter 2x para fechar");
                if ("Enter" == Convert.ToString(ConsoleKey.Enter))
                    Console.ReadKey();
            }
        }

        private static void ListarPerfisDosUsuarios()
        {
            try
            {
                UseRoleRepository repositorio = new(Database.Conexao);
                IEnumerable<User> usersRoles = repositorio.Buscar();

                if (usersRoles.Count() is 0)
                    Console.WriteLine("Não existe usuario cadastrado");
                else
                {
                    Role roles = new();
                    foreach (var userRole in usersRoles)
                    {
                        if (userRole.Roles.Any(x => x.Id > 0))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine($"Existe {userRole.Roles.Count()} perfil  vinculado ao usuario");
                            Console.ResetColor();
                        }

                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Não existe perfil  vinculado a este usuario");
                            Console.ResetColor();
                        }
                        Console.WriteLine($"Id: {userRole.Id} - Nome: {userRole.Name}");

                        foreach (var role in userRole.Roles)
                            if (role.Id is not 0)
                                Console.WriteLine($"    Id: {role.Id} - Perfil: {role.Name}");
                        Console.WriteLine();
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Repositories;
using Mao_Na_Massa_blog.Screens.CategoryScreens;

namespace Mao_Na_Massa_blog.Screens.UserRoleScreens
{
    public static class CreateUserRoleScreens
    {
        public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Vincular perfil/usuario");
            Console.WriteLine("-------------");

            Console.Write("Id Usuario: ");
            var idUser = short.Parse(Console.ReadLine() ?? "0");

            Console.Write("Id Perfil: ");
            var idRole = short.Parse(Console.ReadLine() ?? "0");

            VincularPerfilUsuario(idUser, idRole);
            Console.WriteLine("Deseja Voltar ou Menu? (1 = sim, 2= nao)");
            var opcao = short.Parse(Console.ReadLine()!);
            if (opcao == 1)
                MenuUserRoleScreens.CarregartelaPincipalUserRole();
            else
            {
                Console.WriteLine("Aperte enter 2x para fechar");
                if ("Enter" == Convert.ToString(ConsoleKey.Enter))
                    Console.ReadKey();
                Console.Clear();
            }
        }

        private static void VincularPerfilUsuario(short idUser, short idRole)
        {
            try
            {
                UseRoleRepository repositorio = new(Database.Conexao);
                bool resutado = repositorio.AdicionarPerfilAoUsuario(idUser, idRole);

                if(resutado == false)
                    Console.WriteLine("Erro ao vincular perfil ao usuario");
                else
                    Console.WriteLine("Perfil vinculado com sucesso!");
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Screens.Menu;

namespace Mao_Na_Massa_blog.Screens.UserScreens
{
    public static class MenuUserScreen
    {
        public static void CarregartelaPincipalUser()
        {
            Console.Clear();
            Console.WriteLine("Gestão de usuario");
            Console.WriteLine("--------------");
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine();
            Console.WriteLine("1 - Listar usuario");
            Console.WriteLine("2 - Cadastrar usuario");
            Console.WriteLine("3 - Atualizar usuario");
            Console.WriteLine("4 - Excluir usuario");
            Console.WriteLine("5 - Voltar ao inicio");
            Console.WriteLine();
            var opcao = short.Parse(Console.ReadLine()!);


            switch (opcao)
            {

                case 1:
                    ListUserScreen.Carregartela();
                    break;
                case 2:
                    CreateUserScreen.Carregartela();
                    break;
                case 3:
                    UpdateUserScreen.Carregartela();
                    break;
                case 4:
                    DeleteUserScreen.Carregartela();
                    break;
                case 5:
                    TelaPrincipal.CarregarTelas();
                    break;
                default: CarregartelaPincipalUser(); break;

            }
        }
    }
}
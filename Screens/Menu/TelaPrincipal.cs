using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Screens.CategoryScreens;
using Mao_Na_Massa_blog.Screens.RoleScreens;
using Mao_Na_Massa_blog.Screens.TagScreens;
using Mao_Na_Massa_blog.Screens.UserScreens;

namespace Mao_Na_Massa_blog.Screens.Menu
{
    public static class TelaPrincipal
    {
        public static void CarregarTelas()
        {
            Console.Clear();
            Console.WriteLine("Meu  Blog");
            Console.WriteLine("-----------------");
            Console.WriteLine("O que deseja fazer ?");
            Console.WriteLine();
            Console.WriteLine("1 - Gestão de usuário");
            Console.WriteLine("2 - Gestão de perfil");
            Console.WriteLine("3 - Gestão de categoria");
            Console.WriteLine("4 - Gestão de tag");
            Console.WriteLine("5 - Vincular perfil/usuário");
            Console.WriteLine("6 - Vincular post/tag");
            Console.WriteLine("7 - Relatórios");
            Console.WriteLine();
            Console.WriteLine();
            var opcao = short.Parse(Console.ReadLine()!);

            switch (opcao)
            {   
                case 1:
                    MenuUserScreen.CarregartelaPincipalUser();
                    break;
                case 2:
                    MenuRoleScreens.CarregartelaPincipalRole();
                    break;
                case 3:
                    MenuCategoryScreens.CarregartelaPincipalCategory();
                    break;
                case 4:
                    MenuTagScreens.CarregartelaPincipalTag();
                    break;
                 case 5:
                    MenuUserRoleScreens.CarregartelaPincipalUserRole();
                    break;
                default: CarregarTelas(); break;
            }
        }
    }
}
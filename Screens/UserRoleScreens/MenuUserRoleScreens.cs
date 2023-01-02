using Mao_Na_Massa_blog.Screens.Menu;
using Mao_Na_Massa_blog.Screens.RoleScreens;
using Mao_Na_Massa_blog.Screens.UserRoleScreens;

namespace Mao_Na_Massa_blog.Screens.CategoryScreens
{
    public static class MenuUserRoleScreens
    {
        public static void CarregartelaPincipalUserRole()
        {
            Console.Clear();
            Console.WriteLine("Gestão de perfil/usuário");
            Console.WriteLine("--------------");
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine();
            Console.WriteLine("1 - Listar perfis/usuários");
            Console.WriteLine("2 - Vincular perfil ao usuário");
            Console.WriteLine("3 - Atualizar perfil/usuário");
            Console.WriteLine("4 - Excluir perfil/usuário");
            Console.WriteLine("5 - Voltar ao inicio");
            Console.WriteLine();

            var opcao = short.Parse(Console.ReadLine()!);

            switch (opcao)
            {
                
                case 1:
                    ListUserRoleScreens.Carregartela();
                    break;
                 case 2:
                    CreateUserRoleScreens.Carregartela();
                    break;
                case 3:
                    UpdateUserRoleScreens.Carregartela();
                    break;
                case 4:
                    DeleteUserRoleScreens.Carregartela();
                    break;
                 case 5:
                    TelaPrincipal.CarregarTelas();
                    break;
                default: CarregartelaPincipalUserRole(); break;
            }
        }
    }
}
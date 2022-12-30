using Mao_Na_Massa_blog.Screens.Menu;
using Mao_Na_Massa_blog.Screens.RoleScreens;

namespace Mao_Na_Massa_blog.Screens.TagScreens
{
    public static class MenuRoleScreens
    {
        public static void CarregartelaPincipalRole()
        {
            Console.Clear();
            Console.WriteLine("Gestão de Perfil");
            Console.WriteLine("--------------");
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine();
            Console.WriteLine("1 - Listar Perfis");
            Console.WriteLine("2 - Cadastrar Perfil");
            Console.WriteLine("3 - Atualizar Perfil");
            Console.WriteLine("4 - Excluir Perfil");
            Console.WriteLine("5 - Voltar ao inicio");
            Console.WriteLine();

            var opcao = short.Parse(Console.ReadLine()!);

            switch (opcao)
            {
                
                case 1:
                    ListRoleScreen.Carregartela();
                    break;
                 case 2:
                    CreateRoleScreen.Carregartela();
                    break;
                case 3:
                    UpdateRoleScreen.Carregartela();
                    break;
                case 4:
                    DeleteRoleScreen.Carregartela();
                    break;
                 case 5:
                    TelaPrincipal.CarregarTelas();
                    break;
                default: CarregartelaPincipalRole(); break;
            }
        }
    }
}
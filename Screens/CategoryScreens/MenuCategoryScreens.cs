using Mao_Na_Massa_blog.Screens.Menu;
using Mao_Na_Massa_blog.Screens.RoleScreens;

namespace Mao_Na_Massa_blog.Screens.CategoryScreens
{
    public static class MenuCategoryScreens
    {
        public static void CarregartelaPincipalCategory()
        {
            Console.Clear();
            Console.WriteLine("Gestão de Categoria");
            Console.WriteLine("--------------");
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine();
            Console.WriteLine("1 - Listar Categorias");
            Console.WriteLine("2 - Cadastrar Categoria");
            Console.WriteLine("3 - Atualizar Categoria");
            Console.WriteLine("4 - Excluir Categoria");
            Console.WriteLine("5 - Voltar ao inicio");
            Console.WriteLine();

            var opcao = short.Parse(Console.ReadLine()!);

            switch (opcao)
            {
                
                case 1:
                    ListCategoryScreen.Carregartela();
                    break;
                 case 2:
                    CreateCategoryScreen.Carregartela();
                    break;
                case 3:
                    UpdateCategoryScreen.Carregartela();
                    break;
                case 4:
                    DeleteCategoryScreen.Carregartela();
                    break;
                 case 5:
                    TelaPrincipal.CarregarTelas();
                    break;
                default: CarregartelaPincipalCategory(); break;
            }
        }
    }
}
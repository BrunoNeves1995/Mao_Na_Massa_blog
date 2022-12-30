
using Mao_Na_Massa_blog.Screens.Menu;

namespace Mao_Na_Massa_blog.Screens.TagScreens
{
    public static class MenuTagScreens
    {
        public static void CarregartelaPincipalTag()
        {
            Console.Clear();
            Console.WriteLine("Gestão de tags");
            Console.WriteLine("--------------");
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine();
            Console.WriteLine("1 - Listar tags");
            Console.WriteLine("2 - Cadastrar tags");
            Console.WriteLine("3 - Atualizar tag");
            Console.WriteLine("4 - Excluir tag");
            Console.WriteLine("5 - Voltar ao inicio");
            Console.WriteLine();
            var opcao = short.Parse(Console.ReadLine()!);


            switch (opcao)
            {
                
                case 1:
                    ListTagScreens.Carregartela();
                    break;
                 case 2:
                    CreateTegScreens.Carregartela();
                    break;
                case 3:
                    UpdateTagScreens.Carregartela();
                    break;
                case 4:
                    DeleteTagScreens.Carregartela();
                    break;
                case 5:
                    TelaPrincipal.CarregarTelas();
                    break;
                default: CarregartelaPincipalTag(); break;

            }
        }
    }
}
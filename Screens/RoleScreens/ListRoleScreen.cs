using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;
using Mao_Na_Massa_blog.Screens.TagScreens;

namespace Mao_Na_Massa_blog.Screens.RoleScreens
{
    public static class ListRoleScreen
    {
        public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Lista de Perfis");
            Console.WriteLine("-------------");
            ListarPerfis();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Deseja Voltar ou Menu? (1 = sim, 2= nao)");
            var opcao = short.Parse(Console.ReadLine()!);
            if(opcao == 1)
                MenuRoleScreens.CarregartelaPincipalRole();
            else {
                Console.WriteLine("Aperte enter 2x para fechar"); 
               if("Enter" == Convert.ToString( ConsoleKey.Enter))
                    Console.ReadKey();
            }      
        }

        public static void  ListarPerfis()
        {
           try
           {
                RoleRepository repositorio = new(Database.Conexao);
                IEnumerable<Role> perfis = repositorio.Buscar();    

                if (perfis.Count() == 0)
                        Console.WriteLine("Não tem perfis cadastrado");
                else
                    foreach (var perfil in perfis)
                        Console.WriteLine($"{perfil.Id} - {perfil.Name} - ({perfil.Slug})");                 
           }
           catch (Exception ex)
           {
                Console.WriteLine($"Ocorreu um erro ao buscar os perfis, Mensagem: {ex.Message}");
           }

        
        }
    }
}
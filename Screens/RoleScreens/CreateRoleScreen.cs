using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;
using Mao_Na_Massa_blog.Screens.TagScreens;

namespace Mao_Na_Massa_blog.Screens.RoleScreens
{
    public static class CreateRoleScreen
    {
        public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Novo Perfil");
            Console.WriteLine("-------------");

            Console.Write("Nome: ");
            var name = Console.ReadLine();

            Console.Write("Slug: ");
            var slug = Console.ReadLine();

            CadastarPerfil(new Role
            {   
               
                Name = name,
                Slug = slug
            });
            Console.WriteLine("Deseja Voltar ou Menu? (1 = sim, 2= nao)");
            var opcao = short.Parse(Console.ReadLine()!);
            if (opcao == 1)
                MenuRoleScreens.CarregartelaPincipalRole();
            else
            {
                Console.WriteLine("Aperte enter 2x para fechar");
                if ("Enter" == Convert.ToString(ConsoleKey.Enter))
                    Console.ReadKey();
                    Console.Clear();
            }
        }

        public static void CadastarPerfil(Role perfil)
        {
            try
            {
                RoleRepository repositorio = new(Database.Conexao);
                bool resultado = repositorio.Inserir(perfil);

                if (resultado == false)
                    return;
                
                Console.WriteLine("Perfil Criado com Sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao criar um novo perfil, Mensagem: {ex.Message}");
            }


        }
    }
}
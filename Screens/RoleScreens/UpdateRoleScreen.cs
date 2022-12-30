using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;
using Mao_Na_Massa_blog.Screens.TagScreens;

namespace Mao_Na_Massa_blog.Screens.RoleScreens
{
    public static class UpdateRoleScreen
    {
        public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Atualizar Perfil");
            Console.WriteLine("-------------");

            Console.Write("Id: ");
            var id = short.Parse(Console.ReadLine()!);

            Console.Write("Nome: ");
            var name = Console.ReadLine();

            Console.Write("Slug: ");
            var slug = Console.ReadLine();

            atualizarPerfil(new Role
            {   
                Id = id,
                Name = name,
                Slug = slug
            });
            Console.WriteLine();
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

        public static void atualizarPerfil(Role perfil)
        {
            try
            {
                RoleRepository repositorio = new(Database.Conexao);
                bool resultado = repositorio.Atualizar(perfil);

                if(resultado == false)
                    Console.WriteLine("perfil nao encontrado ou nao foi fornecido dados necessario para atualizar");
                else    
                    Console.WriteLine("Perfil atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Ocorreu um erro ao atualizar a tag, Mensagem: {ex.Message}");
            }
        }
    }
}
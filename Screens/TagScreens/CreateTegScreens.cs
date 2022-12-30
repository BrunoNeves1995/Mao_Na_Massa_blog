using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;

namespace Mao_Na_Massa_blog.Screens.TagScreens
{
    public static class CreateTegScreens
    {
        public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Nova tag");
            Console.WriteLine("-------------");

            Console.Write("Nome: ");
            var name = Console.ReadLine();

            Console.Write("Slug: ");
            var slug = Console.ReadLine();

            CriarTag(new Tag
            {   
               
                Name = name,
                Slug = slug
            });
            Console.WriteLine("Deseja Voltar ou Menu? (1 = sim, 2= nao)");
            var opcao = short.Parse(Console.ReadLine()!);
            if (opcao == 1)
                MenuTagScreens.CarregartelaPincipalTag();
            else
            {
                Console.WriteLine("Aperte enter 2x para fechar");
                if ("Enter" == Convert.ToString(ConsoleKey.Enter))
                    Console.ReadKey();
                    Console.Clear();
            }
        }

        public static void CriarTag(Tag tag)
        {
            try
            {
                TagRepository repositorio = new(Database.Conexao);

                bool resultado = repositorio.Inserir(tag);

                if (resultado == false)
                    return;
                else
                    Console.WriteLine("Tag criada com sucesso");

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Ocorreu um erro no servidor ao criar uma nova tags, Mensagem: {ex.Message}");
            }
        }
    }
}
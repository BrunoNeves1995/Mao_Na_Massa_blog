using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Repositories;

namespace Mao_Na_Massa_blog.Screens.CategoryScreens
{
    public static class DeleteCategoryScreen
    {
         public static void Carregartela()
        {
             Console.Clear();
            Console.WriteLine("Apagar categoria");
            Console.WriteLine("-------------");

            Console.Write("Id: ");
            var id = short.Parse(Console.ReadLine()!);

            ApagarCategoria(id);

            Console.WriteLine();
            Console.WriteLine("Deseja Voltar ou Menu? (1 = sim, 2= nao)");
            var opcao = short.Parse(Console.ReadLine()!);
            if (opcao == 1)
                MenuCategoryScreens.CarregartelaPincipalCategory();
            else
            {
                Console.WriteLine("Aperte enter 2x para fechar");
                if ("Enter" == Convert.ToString(ConsoleKey.Enter))
                    Console.ReadKey();
                    Console.Clear();
            }
        }

        private static void ApagarCategoria(short id)
        {
            try
            {
                CategoryRepository repositorio = new(Database.Conexao);
                bool resultado = repositorio.Deletar(id);

                if(resultado == false)
                    Console.WriteLine("Erro ao excluir categoria");
                else
                    Console.WriteLine("Categoria excluida com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro no servidor ao atualizar a categoria, Mensagem: {ex.Message}");
            }
        }
    }
}
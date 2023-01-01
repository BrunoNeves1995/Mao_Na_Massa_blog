using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;

namespace Mao_Na_Massa_blog.Screens.CategoryScreens
{
    public static class UpdateCategoryScreen
    {
        public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Atualizar categoria");
            Console.WriteLine("-------------");

            Console.Write("Id: ");
            var id = short.Parse(Console.ReadLine()!);

            Console.Write("Nome: ");
            var name = Console.ReadLine();

            Console.Write("Slug: ");
            var slug = Console.ReadLine();

            AtualizarCategoria(new Category
            {
                Id = id,
                Name = name,
                Slug = slug
            });
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

        private static void AtualizarCategoria(Category categoria)
        {
            try
            {
                CategoryRepository repositorio = new(Database.Conexao);
                bool resutado = repositorio.Atualizar(categoria);

                if (resutado == false)
                    Console.WriteLine("Categoria nao encontrada");
                else
                    Console.WriteLine("Categoria atualizada com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao atualizar a categoria, Mensagem: {ex.Message}");
            }
        }
    }
}
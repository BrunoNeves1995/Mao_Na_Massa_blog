using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;

namespace Mao_Na_Massa_blog.Screens.CategoryScreens
{
    public static class CreateCategoryScreen
    {
         public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Novo Categoria");
            Console.WriteLine("-------------");

            Console.Write("Nome: ");
            var name = Console.ReadLine();

            Console.Write("Slug: ");
            var slug = Console.ReadLine();

            CadastarCategoria(new Category
            {   
                Name = name,
                Slug = slug
            });
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

        private static void CadastarCategoria(Category categoria)
        {
             try
            {
                CategoryRepository repositorio = new(Database.Conexao);
                bool resultado = repositorio.Inserir(categoria);

                if (resultado == false)
                    Console.WriteLine("Erro ao criar categoria");
                else
                    Console.WriteLine("Categoria Criada com Sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao criar uma nova categoria, Mensagem: {ex.Message}");
            }

        }
    }
}
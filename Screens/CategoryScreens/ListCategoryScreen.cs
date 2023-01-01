using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;

namespace Mao_Na_Massa_blog.Screens.CategoryScreens
{
    public static class ListCategoryScreen
    {
        public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Lista de Categorias");
            Console.WriteLine("-------------");
            ListarCategorias();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Deseja Voltar ou Menu? (1 = sim, 2= nao)");
            var opcao = short.Parse(Console.ReadLine()!);
            if(opcao == 1)
                MenuCategoryScreens.CarregartelaPincipalCategory();
            else {
                Console.WriteLine("Aperte enter 2x para fechar"); 
               if("Enter" == Convert.ToString( ConsoleKey.Enter))
                    Console.ReadKey();
            }      
        }

        public static void ListarCategorias()
        {
            try
            {
                CategoryRepository repositorio = new(Database.Conexao);
                IEnumerable<Category> categorias = repositorio.Buscar();

                if (categorias.Count() == 0)
                    Console.WriteLine("Não tem categorias cadastradas");
                else
                    foreach (var categoria in categorias)
                        Console.WriteLine($"{categoria.Id} - {categoria.Name} - ({categoria.Slug})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao buscar as categorias, Mensagem: {ex.Message}");
            }
        }
    }
}


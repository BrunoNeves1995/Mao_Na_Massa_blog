using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace Mao_Na_Massa_blog.Screens.TagScreens
{
    public static class ListTagScreens
    {
        public static void Carregartela()
        {   
            Console.Clear();
            Console.WriteLine("Lista de tags");
            Console.WriteLine("-------------");
            ListarTags();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Deseja Voltar ou Menu? (1 = sim, 2= nao)");
            var opcao = short.Parse(Console.ReadLine()!);
            if(opcao == 1)
                MenuTagScreens.CarregartelaPincipalTag();
            else {
                Console.WriteLine("Aperte enter 2x para fechar"); 
               if("Enter" == Convert.ToString( ConsoleKey.Enter))
                    Console.ReadKey();
            }           
        }

        public static void ListarTags()
        {
            try
            {
                TagRepository repositorio = new(Database.Conexao!);
                var tags = repositorio.Buscar();

                if (tags.Count() == 0)
                    Console.WriteLine("Tags não cadastradas");
                else
                    foreach (var tag in tags)
                        Console.WriteLine($"{tag.Id} - {tag.Name} - ({tag.Slug})");
            }
            catch (System.Exception ex)
            {
               Console.WriteLine($"Ocorreu um erro ao buscar as tags no servidor, Mensagem: {ex.Message}");
            }
        }
    }
}
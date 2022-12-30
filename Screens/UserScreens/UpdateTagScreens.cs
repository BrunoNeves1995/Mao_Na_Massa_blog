using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;

namespace Mao_Na_Massa_blog.Screens.TagScreens
{
    public static class UpdateTagScreens
    {
      
            public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Atualizar tag");
            Console.WriteLine("-------------");

            Console.Write("Id: ");
            var id = short.Parse(Console.ReadLine()!);

            Console.Write("Nome: ");
            var name = Console.ReadLine();

            Console.Write("Slug: ");
            var slug = Console.ReadLine();

            atualizarTag(new Tag
            {   
                Id = id,
                Name = name,
                Slug = slug
            });
            Console.WriteLine();
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

        public static void atualizarTag(Tag tag)
        {
            try
            {
                TagRepository repositorio = new(Database.Conexao);
                bool resutado = repositorio.Atualizar(tag);

                if(resutado == false)
                    Console.WriteLine("Erro ao atualizar a tag");
                else
                    Console.WriteLine("Tag atualizada com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro no servidor ao atualizar a tag, Mensagem: {ex.Message}");
            }
        }
        
    }
}
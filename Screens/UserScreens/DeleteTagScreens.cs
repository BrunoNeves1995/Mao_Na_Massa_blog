using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;

namespace Mao_Na_Massa_blog.Screens.TagScreens
{
    public class DeleteTagScreens
    {
        public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Apagar tag");
            Console.WriteLine("-------------");

            Console.Write("Id: ");
            var id = short.Parse(Console.ReadLine()!);

            ApagarTag(id);

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

        public static void ApagarTag(int id)
        {
            try
            {
                TagRepository repositorio = new(Database.Conexao);
                bool resultado = repositorio.Deletar(id);

                if(resultado == false)
                    Console.WriteLine("Erro ao excluir tag");
                else
                    Console.WriteLine("Tag excluida com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro no servidor ao atualizar a tag, Mensagem: {ex.Message}");
            }
        }
    }
}
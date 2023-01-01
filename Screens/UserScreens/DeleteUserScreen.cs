using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Repositories;

namespace Mao_Na_Massa_blog.Screens.UserScreens
{
    public static class DeleteUserScreen
    {
        public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Apagar usuario");
            Console.WriteLine("-------------");

            Console.Write("Id: ");
            var id = short.Parse(Console.ReadLine()!);

            ApagarUsuario(id);

            Console.WriteLine();
            Console.WriteLine("Deseja Voltar ou Menu? (1 = sim, 2= nao)");
            var opcao = short.Parse(Console.ReadLine()!);
            if (opcao == 1)
                MenuUserScreen.CarregartelaPincipalUser();
            else
            {
                Console.WriteLine("Aperte enter 2x para fechar");
                if ("Enter" == Convert.ToString(ConsoleKey.Enter))
                    Console.ReadKey();
                Console.Clear();
            }
        }

        private static void ApagarUsuario(short id)
        {
            try
            {
                UserRepository repositorio = new(Database.Conexao);
                bool resultado = repositorio.Deletar(id);

                if (resultado == false)
                    Console.WriteLine("Erro ao excluir usuario");
                else
                    Console.WriteLine("Usuario excluido com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao excluir o usuario, Mensagem: {ex.Message}");
            }
        }
    }
}
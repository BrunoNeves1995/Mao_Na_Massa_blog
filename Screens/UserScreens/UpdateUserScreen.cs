using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;

namespace Mao_Na_Massa_blog.Screens.UserScreens
{
    public class UpdateUserScreen
    {
        public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Atualizar Usuario");
            Console.WriteLine("-------------");

            Console.Write("Id: ");
            var id = short.Parse(Console.ReadLine() ?? "0");

            Console.Write("Nome: ");
            var name = Console.ReadLine();

            Console.Write("E-mail: ");
            var email = Console.ReadLine();

            Console.Write("Senha: ");
            var senha = Console.ReadLine();

            Console.Write("Bio: ");
            var bio = Console.ReadLine();

            Console.Write("Imagem: ");
            var imagem = Console.ReadLine();


            Console.Write("Slug: ");
            var slug = Console.ReadLine();

            atualizarUsuario(new User
            {
                Id = id,
                Name = name,
                Email = email,
                PasswordHash = senha,
                Bio = bio,
                Image = imagem,
                Slug = slug
            });
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

        private static void atualizarUsuario(User usuario)
        {
            try
            {
                UserRepository repositorio = new(Database.Conexao);
                bool resultado = repositorio.Atualizar(usuario);

                if (resultado == false)
                    Console.WriteLine("Usuario nao encontrado ou nao foi fornecido dados necessario para atualizar");
                else
                    Console.WriteLine("Usuario atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao atualizar usuario, Mensagem: {ex.Message}");
            }
        }
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;

namespace Mao_Na_Massa_blog.Screens.UserScreens
{
    public static class CreateUserScreen
    {
        public static void Carregartela()
        {
            Console.Clear();
            Console.WriteLine("Novo Usuario");
            Console.WriteLine("-------------");

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

            CriarUser(new User
            {
                Name = name,
                Email = email,
                PasswordHash = senha,
                Bio = bio,
                Image = imagem,
                Slug = slug
            });
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

        public static void CriarUser(User usuario)
        {
            try
            {
                UserRepository repositorio = new(Database.Conexao);
                bool resultado = repositorio.Inserir(usuario);

                if(resultado == false)
                    Console.WriteLine("Erro ao cadastrar usuario");
                else    
                    Console.WriteLine("Usuario criado com sucesso!");
            }
            catch (Exception ex)
            {
                 Console.WriteLine($"Ocorreu um erro ao criar usuario, Mensagem: {ex.Message}");
            }
        }

    }
}
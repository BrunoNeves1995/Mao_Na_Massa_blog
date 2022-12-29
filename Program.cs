
using System.Data.SqlClient;
using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;
using Mao_Na_Massa_blog.Screens.TagScreens;

namespace Mao_Na_Massa_blog
{
    public class Program
    {
        const string CONNECTION_STRING
            = @"Server=TERMINAL01\SQLEXPRESS;Database=Blog;User ID=admin;Password=12345;Trusted_Connection=false;TrustServerCertificate=true";

        static void Main(string[] args)
        {
            User usuario = new("carol", "nevescarol@gmail.com", "1234", "carol-dev", "https://...", "carol Neves");
            Role autor = new(name: "Aluno", slug: "Aluno");

            var connection = new SqlConnection(CONNECTION_STRING);


            connection.Open();

            CarregarTelas();

           
            Console.ReadKey();
             connection.Close();
        }

        private static void CarregarTelas()
        {
            Console.Clear();
            Console.WriteLine("Meu ao Blog");
            Console.WriteLine("-----------------");
            Console.WriteLine("O que deseja fazer ?");
            Console.WriteLine();
            Console.WriteLine("1 - Gestão de usuário");
            Console.WriteLine("2 - Gestão de perfil");
            Console.WriteLine("3 - Gestão de categoria");
            Console.WriteLine("4 - Gestão de tag");
            Console.WriteLine("5 - Vincular perfil/usuário");
            Console.WriteLine("6 - Vincular post/tag");
            Console.WriteLine("7 - Relatórios");
            Console.WriteLine();
            Console.WriteLine();
            var opcao = short.Parse(Console.ReadLine()!);

            switch (opcao)
            {
                case 4:
                    MenuTagScreens.CarregartelaPincipalTag();
                    break;
                default: CarregarTelas(); break;
            }
        }
    }
}


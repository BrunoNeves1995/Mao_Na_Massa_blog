
using System.Data.SqlClient;
using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories;
using Mao_Na_Massa_blog.Screens.Menu;
using Mao_Na_Massa_blog.Screens.TagScreens;

namespace Mao_Na_Massa_blog
{
    public class Program
    {
        const string CONNECTION_STRING
            = @"Server=TERMINAL01\SQLEXPRESS;Database=Blog;User ID=admin;Password=12345;Trusted_Connection=false;TrustServerCertificate=true";

        static void Main(string[] args)
        {
            // User usuario = new("carol", "nevescarol@gmail.com", "1234", "carol-dev", "https://...", "carol Neves");
            // Role pefil = new(name: "Aluno", slug: "Aluno");

            Database.Conexao = new SqlConnection(CONNECTION_STRING);


            Database.Conexao.Open();

            TelaPrincipal.CarregarTelas();

            Console.ReadKey();
             Database.Conexao.Close();
        }

        
    }
}


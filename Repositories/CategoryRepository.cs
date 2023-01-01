using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Models;
using Mao_Na_Massa_blog.Repositories.Interface;

namespace Mao_Na_Massa_blog.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {   
        private readonly SqlConnection _connection;

        public CategoryRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public Category Busca(short id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> Buscar()
        {   
            List<Category> categorias = new();
            Category categoria = new();
            try 
            {
                var consultaSql = @"
                    SELECT 
                        [Id],
                        [Name],
                        [Slug]
                    FROM [Blog].[dbo].[Category]
                ";

                SqlCommand command = new();
                command.Connection = _connection;
                command.CommandText = consultaSql;
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows is false)
                    return new List<Category>();
                
                while (reader.Read())
                {
                    
                    categoria.Id = Convert.ToInt32(reader["Id"]);
                    categoria.Name = Convert.ToString(reader["Name"]);
                    categoria.Slug = Convert.ToString(reader["Slug"]);
                }

                if(categoria is null)
                    return new List<Category>();
                

            }
            catch (Exception ex)
            {
                 Console.WriteLine($"E514 - Erro interno no servidor, Mensagem: {ex.Message}");
                 new List<Category>();
            }
            return categorias;
        
        }

        public bool Inserir(Category model)
        {
            throw new NotImplementedException();
        }

        public bool Atualizar(Category model)
        {
            throw new NotImplementedException();
        }

        public bool Deletar(short id)
        {
            throw new NotImplementedException();
        }

       
    }
}
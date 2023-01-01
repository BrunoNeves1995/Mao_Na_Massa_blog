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
            Category categoria = new();
            try 
            {
                var consultaSql = @"
                    SELECT 
                        [Id],
                        [Name],
                        [Slug]
                    FROM [Blog].[dbo].[Category]
                    WHERE [Id] = @Id
                ";

                SqlCommand command = new();
                command.CommandText = consultaSql;
                command.Connection = _connection;
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();

                // if(reader.HasRows is false)
                //     return new List<Category>();
                
                while (reader.Read())
                {
                    categoria.Id = Convert.ToInt32(reader["Id"]);
                    categoria.Name = Convert.ToString(reader["Name"]);
                    categoria.Slug = Convert.ToString(reader["Slug"]);
                }
                reader.Close();

                if(categoria is null)
                    return new Category();
              
            }
            catch (Exception ex)
            {
                 Console.WriteLine($"E514 - Erro interno no servidor, Mensagem: {ex.Message}");
                 new List<Category>();
            }
            return categoria;

        }

        public IEnumerable<Category> Buscar()
        {   
            List<Category> categorias = new();
            
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
                command.CommandText = consultaSql;
                command.Connection = _connection;
                SqlDataReader reader = command.ExecuteReader();

                // if(reader.HasRows is false)
                //     return new List<Category>();
                
                while (reader.Read())
                {
                    Category categoria = new();
                    categoria.Id = Convert.ToInt32(reader["Id"]);
                    categoria.Name = Convert.ToString(reader["Name"]);
                    categoria.Slug = Convert.ToString(reader["Slug"]);
                    categorias.Add(categoria);
                }
                reader.Close();

                if(categorias.Count() is 0)
                    return new List<Category>();
                

            }
            catch (Exception ex)
            {
                 Console.WriteLine($"E514 - Erro interno no servidor, Mensagem: {ex.Message}");
                 new List<Category>();
            }
            return categorias;
        
        }

        public bool Inserir(Category categoria)
        {
            try
            {
              
                var InsertSql = @"
                INSERT INTO [Blog].[dbo].[Category] 
                VALUES
                (
                    @Name, 
                    @Slug
                )";
                
                var command = new SqlCommand();
                command.CommandText = InsertSql;
                command.Connection = _connection;
                command.Parameters.AddWithValue("@Name", categoria.Name);
                command.Parameters.AddWithValue("@Slug", categoria.Slug);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E515 - Erro interno no servidor, Mensagem: {ex.Message}");
                return false;
            }
            return true;
        }

        public bool Atualizar(Category categoria)
        {   
            Category antigaCategoria = new ();
            try
            {   
                // recuperando o Perfil
                var sql = @"
                         SELECT 
                             [Id]
                            ,[Name]
                            ,[Slug]
                         FROM [Blog].[dbo].[Role]
		                 WHERE [Id] = @Id";

                SqlCommand command = new();
                command.CommandText = sql;
                command.Connection = _connection;
                command.Parameters.AddWithValue("@Id", categoria.Id);
                SqlDataReader reader = command.ExecuteReader();
                

                while (reader.Read())
                {
                   
                    antigaCategoria.Id = Convert.ToInt32(reader["Id"]);
                    antigaCategoria.Name = Convert.ToString(reader["Name"]);
                    antigaCategoria.Slug = Convert.ToString(reader["Slug"]);
                }
                reader.Close();

                // atualizando o dados do Perfil
                if (antigaCategoria.Id == 0 || string.IsNullOrEmpty(categoria.Name) && string.IsNullOrEmpty(categoria.Slug))
                    return false;
                else
                {
                    var insertSql = @"
	                UPDATE [Blog].[dbo].[Category]
	                    SET 
                        Name = @Name,
                        Slug = @Slug
	                    WHERE Id = @IdAutor";

                    command.Parameters.AddWithValue("@IdAutor", categoria.Id);
                    
                    // se nao for null ou vazio ou igual ao valor antigo
                    if( !string.IsNullOrEmpty(categoria.Name) && categoria.Name != antigaCategoria.Name)
                        command.Parameters.AddWithValue("@Name", categoria.Name);
                    else
                        command.Parameters.AddWithValue("@Name", antigaCategoria.Name);

                   if(!string.IsNullOrEmpty(categoria.Slug) && categoria.Slug != antigaCategoria.Slug)
                        command.Parameters.AddWithValue("@Slug", categoria.Slug);
                    else
                        command.Parameters.AddWithValue("@Slug", antigaCategoria.Slug);    
                    
                    command.CommandText = insertSql;
                    command.Connection = _connection;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E508 - Erro interno no servidor, Mensagem: {ex.Message}");
                return false;
            }
            return true;
        }

        public bool Deletar(short id)
        {
           Category categoria = new();
            try
            {
                SqlCommand command = new();

                // recuperando o cargo
                var sql = @"
                SELECT 
                    [Id]
                    ,[Name]
                    ,[Slug]
                FROM  [Blog].[dbo].[Category]
		        WHERE [Id] = @Id";

                command.CommandText = sql;
                command.Connection = _connection;
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    categoria.Id = Convert.ToInt32(reader["Id"]);
                    categoria.Name = Convert.ToString(reader["Name"]);
                    categoria.Slug = Convert.ToString(reader["Slug"]);
                    Console.WriteLine($"Id: {categoria.Id}, Nome: {categoria.Name} - ({categoria.Slug})");
                }
                reader.Close();

                if (categoria.Id == 0 || categoria.Name == null)
                    return false;

                // deletando tag
                var deleteSql =
                @"DELETE FROM  [Blog].[dbo].[Category]
	                        WHERE 
                                [Id] = @IdTag";

                command.Parameters.AddWithValue("@IdTag", id);
                command.Connection = _connection;
                command.CommandText = deleteSql;
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"E514 - Erro interno no servidor, Mensagem: {ex.Message}");
                return false;
            }
            return true;
        }

        
    }
}
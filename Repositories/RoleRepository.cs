using Mao_Na_Massa_blog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mao_Na_Massa_blog.Repositories
{
    public class RoleRepository
    {
        private readonly SqlConnection _connection;

        public RoleRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Role> Buscar()
        {
            List<Role> autores = new();
            try
            {
                SqlCommand command = new();

                var sql = @"
                SELECT 
                    [Id]
                    ,[Name]
                    ,[Slug]
                FROM [Blog].[dbo].[Role]
                ";

                command.CommandText = sql;
                command.Connection = _connection;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Role autor = new();
                    autor.Id = Convert.ToInt32(reader["Id"]);
                    autor.Name = Convert.ToString(reader["Name"]);
                    autor.Slug = Convert.ToString(reader["Slug"]);
                    autores.Add(autor);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E505 - Erro interno no servidor, Mensagem: {ex.Message}");
            }
            return autores;
        }

        public Role Busca(int id)
        {
            Role autor = new();
            try
            {
                var command = new SqlCommand();
                var sql = @"
                SELECT 
                    [Id]
                    ,[Name]
                    ,[Slug]
                FROM [Blog].[dbo].[Role]
		        WHERE [Id] = @Id";

                command.CommandText = sql;
                command.Connection = _connection;

                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    autor.Id = Convert.ToInt32(reader["Id"]);
                    autor.Name = Convert.ToString(reader["Name"]);
                    autor.Slug = Convert.ToString(reader["Slug"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E506 - Erro interno no servidor, Mensagem: {ex.Message}");
            }
            return autor;
        }


        public bool Inserir(Role autor)
        {
            try
            {
                var command = new SqlCommand();
                var sql = @"
                INSERT INTO [Blog].[dbo].[Role] 
                VALUES
                (
                    @Name, 
                    @Slug
                )";

                command.CommandText = sql;
                command.Connection = _connection;

                command.Parameters.AddWithValue("@Name", autor.Name);
                command.Parameters.AddWithValue("@Slug", autor.Slug);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E507 - Erro interno no servidor, Mensagem: {ex.Message}");
                return false;
            }
            return true;
        }


        public bool Atualizar(Role autor, int id)
        {
            try
            {   
                Role antigoAutor = new Role();
                SqlCommand command = new();
                // recuperando o autor
                var sql = @"
                         SELECT 
                             [Id]
                            ,[Name]
                            ,[Slug]
                         FROM [Blog].[dbo].[Role]
		                 WHERE [Id] = @Id";

                command.CommandText = sql;
                command.Connection = _connection;
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                   
                    antigoAutor.Id = Convert.ToInt32(reader["Id"]);
                    antigoAutor.Name = Convert.ToString(reader["Name"]);
                    antigoAutor.Slug = Convert.ToString(reader["Slug"]);
                    Console.WriteLine($"Id: {antigoAutor.Id}, Nome: {antigoAutor.Name}");
                }
                reader.Close();

                // atualizando o dados do autor
                if (antigoAutor.Id == 0 || antigoAutor.Name == null)
                {
                    return false;
                }
                else
                {
                    var insertSql = @"
	                UPDATE [Blog].[dbo].[Role] 
	                    SET Name = @Name
	                WHERE Id = @IdAutor";

                    command.Parameters.AddWithValue("@IdAutor", id);
                    command.Parameters.AddWithValue("@Name", autor.Name);

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

        public bool Deletar(int id)
        {
            try
            {
                Role autor = new();


                SqlCommand command = new();

                // recuperando o cargo
                var sql = @"
                SELECT 
                    [Id]
                    ,[Name]
                    ,[Slug]
                FROM [Blog].[dbo].[Role]
		        WHERE [Id] = @Id";

                command.CommandText = sql;
                command.Connection = _connection;
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    autor.Id = Convert.ToInt32(reader["Id"]);
                    autor.Name = Convert.ToString(reader["Name"]);
                    autor.Slug = Convert.ToString(reader["Slug"]);

                    Console.WriteLine($"Id: {autor.Id}, Nome: {autor.Name}");
                }
                reader.Close();

                if (autor.Id == 0 || autor.Name == null)
                {
                    return false;
                }
                else
                {
                    // deletando cargo
                    var deleteSql =
                    @"DELETE FROM [Blog].[dbo].[Role]
	                        WHERE [Id] = @IdUsuario";

                    command.Parameters.AddWithValue("@IdUsuario", id);
                    command.Connection = _connection;
                    command.CommandText = deleteSql;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E509 - Erro interno no servidor, Mensagem: {ex.Message}");
                return false;
            }
            return true;
        }
    }
}

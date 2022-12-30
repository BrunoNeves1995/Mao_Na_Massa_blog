using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Models;

namespace Mao_Na_Massa_blog.Repositories
{
    public class TagRepository
    {
        private readonly SqlConnection _connection;

        public TagRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Tag> Buscar()
        {
            List<Tag> tags = new();

            try
            {
                SqlCommand command = new();

                var consultaSql = @"
                    SELECT 
                        [Id],
                        [Name],
                        [Slug]
                    FROM [Blog].[dbo].[Tag]
                ";

                command.CommandText = consultaSql;
                command.Connection = _connection;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Tag tag = new();
                    tag.Id = Convert.ToInt32(reader["Id"]);
                    tag.Name = Convert.ToString(reader["Name"]);
                    tag.Slug = Convert.ToString(reader["Slug"]);
                    tags.Add(tag);
                }
                reader.Close();

            }
            catch (Exception ex)
            {

                Console.WriteLine($"E511 - Erro interno no servidor, Mensagem: {ex.Message}");
            }

            return tags;
        }

        public bool Inserir(Tag tag)
        {
            try
            {
                SqlCommand command = new();

                var insertSql = @"
                    INSERT [Blog].[dbo].[Tag] VALUES(@Name, @Slug)
                ";

                command.CommandText = insertSql;
                command.Connection = _connection;
                command.Parameters.AddWithValue("@Name", tag.Name);
                command.Parameters.AddWithValue("@Slug", tag.Slug);
                int resultado = command.ExecuteNonQuery();

                if (resultado == 0)
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E512 - Erro interno no servidor, Mensagem: {ex.Message}");
            }
            return true;
        }

        public bool Atualizar(Tag tag)
        {
            Tag tagAntiga = new();
            try
            {
                SqlCommand command = new();

                var consultaSql = @"
                    SELECT 
                        [Id]
                        ,[Name]
                        ,[Slug]
                    FROM [Blog].[dbo].[Tag]
                    WHERE [Id] = @Id
                    ";

                command.CommandText = consultaSql;
                command.Connection = _connection;
                command.Parameters.AddWithValue("@id", tag.Id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    tagAntiga.Id = Convert.ToInt32(reader["Id"]);
                    tagAntiga.Name = Convert.ToString(reader["Name"]);
                    tagAntiga.Slug = Convert.ToString(reader["Slug"]);
                }
                reader.Close();

                // atualzando a tag se a tag existir
                if (tagAntiga.Id == 0 || tagAntiga.Name == null)
                {
                    Console.WriteLine("Tag nao encontrada");
                    return false;
                }

                var updateSql = @"
                    UPDATE [Blog].[dbo].[Tag]  
                    SET 
                        [Name] = @Name, 
                        [Slug] = @Slug
                    WHERE [Id] = @Id
                ";

                command.CommandText = updateSql;
                command.Connection = _connection;
                // command.Parameters.AddWithValue("@id", tag.Id);
                command.Parameters.AddWithValue("@Name", tag.Name);
                command.Parameters.AddWithValue("@Slug", tag.Slug);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E513 - Erro interno no servidor, Mensagem: {ex.Message}");
                return false;
            }
            return true;
        }

        public bool Deletar(int id)
        {
            Tag tag = new();
            try
            {
                SqlCommand command = new();

                // recuperando o cargo
                var sql = @"
                SELECT 
                    [Id]
                    ,[Name]
                    ,[Slug]
                FROM [Blog].[dbo].[Tag]
		        WHERE [Id] = @Id";

                command.CommandText = sql;
                command.Connection = _connection;
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tag.Id = Convert.ToInt32(reader["Id"]);
                    tag.Name = Convert.ToString(reader["Name"]);
                    tag.Slug = Convert.ToString(reader["Slug"]);
                    Console.WriteLine($"Id: {tag.Id}, Nome: {tag.Name}");
                }
                reader.Close();

                if (tag.Id == 0 || tag.Name == null)
                    return false;

                // deletando tag
                var deleteSql =
                @"DELETE FROM [Blog].[dbo].[Tag]
	                        WHERE 
                                [Id] = @IdTag";

                command.Parameters.AddWithValue("@IdTag", id);
                command.Connection = _connection;
                command.CommandText = deleteSql;
                command.ExecuteNonQuery();

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
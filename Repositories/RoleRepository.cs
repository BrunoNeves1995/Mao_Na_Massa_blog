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
            List<Role> perfis = new();
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
                    Role perfil = new();
                    perfil.Id = Convert.ToInt32(reader["Id"]);
                    perfil.Name = Convert.ToString(reader["Name"]);
                    perfil.Slug = Convert.ToString(reader["Slug"]);
                    perfis.Add(perfil);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E505 - Erro interno no servidor, Mensagem: {ex.Message}");
            }
            return perfis;
        }

        public Role Busca(int id)
        {
            Role perfil = new();
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
                    perfil.Id = Convert.ToInt32(reader["Id"]);
                    perfil.Name = Convert.ToString(reader["Name"]);
                    perfil.Slug = Convert.ToString(reader["Slug"]);
                }
                reader.Close();
                if(perfil == null)
                    return new Role();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E506 - Erro interno no servidor, Mensagem: {ex.Message}");
            }
            return perfil;
        }


        public bool Inserir(Role perfil)
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

                command.Parameters.AddWithValue("@Name", perfil.Name);
                command.Parameters.AddWithValue("@Slug", perfil.Slug);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E507 - Erro interno no servidor, Mensagem: {ex.Message}");
                return false;
            }
            return true;
        }


        public bool Atualizar(Role perfil)
        {
            try
            {   
                Role antigoPerfil = new Role();
                SqlCommand command = new();
                // recuperando o Perfil
                var sql = @"
                         SELECT 
                             [Id]
                            ,[Name]
                            ,[Slug]
                         FROM [Blog].[dbo].[Role]
		                 WHERE [Id] = @Id";

                command.CommandText = sql;
                command.Connection = _connection;
                command.Parameters.AddWithValue("@Id", perfil.Id);
                SqlDataReader reader = command.ExecuteReader();
                

                while (reader.Read())
                {
                   
                    antigoPerfil.Id = Convert.ToInt32(reader["Id"]);
                    antigoPerfil.Name = Convert.ToString(reader["Name"]);
                    antigoPerfil.Slug = Convert.ToString(reader["Slug"]);
                }
                reader.Close();

                // atualizando o dados do Perfil
                if (antigoPerfil.Id == 0 || string.IsNullOrEmpty(perfil.Name) && string.IsNullOrEmpty(perfil.Slug))
                    return false;
                else
                {
                    var insertSql = @"
	                UPDATE [Blog].[dbo].[Role] 
	                    SET 
                        Name = @Name,
                        Slug = @Slug
	                    WHERE Id = @IdAutor";

                    command.Parameters.AddWithValue("@IdAutor", perfil.Id);
                    
                    // se nao for null ou vazio ou igual ao valor antigo
                    if( !string.IsNullOrEmpty(perfil.Name) && perfil.Name != antigoPerfil.Name)
                        command.Parameters.AddWithValue("@Name", perfil.Name);
                    else
                        command.Parameters.AddWithValue("@Name", antigoPerfil.Name);

                   if(!string.IsNullOrEmpty(perfil.Slug) && perfil.Slug != antigoPerfil.Slug)
                        command.Parameters.AddWithValue("@Slug", perfil.Slug);
                    else
                        command.Parameters.AddWithValue("@Slug", antigoPerfil.Slug);    
                    
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
                Role perfil = new();


                SqlCommand command = new();

                // recuperando o Perfil
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
                    perfil.Id = Convert.ToInt32(reader["Id"]);
                    perfil.Name = Convert.ToString(reader["Name"]);
                    perfil.Slug = Convert.ToString(reader["Slug"]);

                    Console.WriteLine($"Id: {perfil.Id}, Nome: {perfil.Name}");
                }
                reader.Close();

                if (perfil.Id == 0 || perfil.Name == null)
                {
                    return false;
                }
                else
                {
                    // deletando Perfil
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

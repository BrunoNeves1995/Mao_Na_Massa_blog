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
            _connection= connection;
        }

        public IEnumerable<Role> Buscar()
        {
            List<Role> roles = new();
            try
            {
                using (_connection)
                {
                    using (var command = new SqlCommand())
                    {
                        var sql = @"
                         SELECT 
                             [Id]
                            ,[Name]
                            ,[Slug]
                         FROM [Blog].[dbo].[Role]";

                        
                        command.CommandText = sql;
                        command.Connection = _connection;
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Role role = new();
                            role.Id = Convert.ToInt32(reader["Id"]);
                            role.Name= Convert.ToString(reader["Name"]);
                            role.Slug = Convert.ToString(reader["Slug"]);
                            roles.Add(role);

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"E505 - Erro interno no servidor, Mensagem: {ex.Message}");
            }
            return roles;
        }

        public Role Busca(int id)
        {
            Role role = new();
            try
            {
                using (_connection)
                {
                    using (var command = new SqlCommand())
                    {
                        var sql =@"
                         SELECT 
                             [Id]
                            ,[Name]
                            ,[Slug]
                         FROM [Blog].[dbo].[Role]
		                 WHERE [Id] = @Id";

                        command.Parameters.AddWithValue("@Id", id);
                        command.Connection = _connection;
                        command.CommandText = sql;
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            role.Id = Convert.ToInt32(reader["Id"]);
                            role.Name = Convert.ToString(reader["Name"]);
                            role.Slug = Convert.ToString(reader["Slug"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"E506 - Erro interno no servidor, Mensagem: {ex.Message}");

            }
            return role;
        }

        public bool Inserir(Role cargo)
        {
            try
            {
                using (_connection)
                {
                    using (var command = new SqlCommand())
                    {
                        var sql =
                        @"
                        INSERT INTO [Blog].[dbo].[Role] 
                            VALUES
                        (
                            @Name, 
                            @Slug
                        )";

                        command.Parameters.AddWithValue("@Name", cargo.Name);
                        command.Parameters.AddWithValue("@Slug", cargo.Slug);

                        command.Connection = _connection;
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                        // int result = command.ExecuteNonQuery();
                        // Console.WriteLine($"{result} registro afetados");
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"E507 - Erro interno no servidor, Mensagem: {ex.Message}");
                return false;
            }

            return true;
        }

        public bool Atualizar(Role cargo, int id)
        {
            try
            {
                using (_connection)
                {
                    using (SqlCommand command = new())
                    {
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
                            Role antigoCargo = new();
                            antigoCargo.Id = Convert.ToInt32(reader["Id"]);
                            antigoCargo.Name = Convert.ToString(reader["Name"]);
                            antigoCargo.Slug = Convert.ToString(reader["Slug"]);

                            Console.WriteLine($"Id: {antigoCargo.Id}, Nome: {antigoCargo.Name}");
                        }
                        reader.Close();

                        // atualizando o dados do caro
                        var insertSql =
                        @"
	                     UPDATE [Blog].[dbo].[Role] 
	                        SET Name = @Name
	                    WHERE Id = @Id";

                        // command.Parameters.AddWithValue("@Id", cargo.Id);
                        command.Parameters.AddWithValue("@Name", cargo.Name);

                        command.CommandText = insertSql;
                        command.Connection = _connection;
                        command.ExecuteNonQuery();
                    }
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
                Role cargo = new();
                using (_connection)
                {
                    using (SqlCommand command = new())
                    {
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

                            cargo.Id = Convert.ToInt32(reader["Id"]);
                            cargo.Name = Convert.ToString(reader["Name"]);
                            cargo.Slug = Convert.ToString(reader["Slug"]);

                            Console.WriteLine($"Id: {cargo.Id}, Nome: {cargo.Name}");
                        }
                        reader.Close();

                        if (cargo.Id == 0 || cargo.Name == null)
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

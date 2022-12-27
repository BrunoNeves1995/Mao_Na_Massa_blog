using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Mao_Na_Massa_blog.Models;

namespace Mao_Na_Massa_blog.Repositories
{
    public class UseRoleRepository
    {
        private readonly SqlConnection _connection;

        public UseRoleRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<User> Buscar()
        {
            List<User> userRoles = new();
            try
            {
                var consultaSql = @"
                    SELECT 
                        U.[Id],
                        U.[Name],
                        U.[Email],
                        U.[Bio],
                        U.[Image],
                        U.[Slug],
                        R.[Id] AS IdRole,
                        R.[Name] AS RoleName,
                        R.[Slug] AS RoleSlug 
                    FROM [Blog].[dbo].[User] AS U 
                    LEFT JOIN [Blog].[dbo].[UserRole] AS UR ON U.Id = UR.[UserId]
                    LEFT JOIN [Blog].[dbo].[Role] AS R ON UR.[RoleId] = R.[Id]
                ";

                SqlCommand command = new();
                command.CommandText = consultaSql;
                command.Connection = _connection;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User user = new();
                    Role role = new();

                    user.Id = Convert.ToInt32(reader["Id"]);
                    user.Name = Convert.ToString(reader["Name"]);
                    user.Email = Convert.ToString(reader["Email"]);
                    user.Bio = Convert.ToString(reader["Bio"]);
                    user.Image = Convert.ToString(reader["Image"]);
                    user.Slug = Convert.ToString(reader["Slug"]);

                    // se o usuario não existir na lista
                    var usr = userRoles.FirstOrDefault(x => x.Id == user.Id);
                    if (usr == null)
                    {
                        usr = user;

                        if (reader["IdRole"] is not DBNull)
                        {
                            role.Id = Convert.ToInt32(reader["IdRole"]);
                            role.Name = Convert.ToString(reader["RoleName"]);
                            role.Slug = Convert.ToString(reader["RoleSlug"]);
                            usr.Roles.Add(role);
                            userRoles.Add(usr);
                           
                        }
                        else
                        {
                            role.Id = 0;
                            role.Name = null;
                            role.Slug = null;
                            usr.Roles.Add(role);
                            userRoles.Add(usr);
                        }
                       
                    }
                    
                    //  usuario ja existir na lista, então adiciona apenas os roles
                    else if (reader["IdRole"] is not DBNull)
                    {
                        role.Id = Convert.ToInt32(reader["IdRole"]);
                        role.Name = Convert.ToString(reader["RoleName"]);
                        role.Slug = Convert.ToString(reader["RoleSlug"]);
                        usr.Roles.Add(role);
                         
                    }
                    else
                    {
                        role.Id = 0;
                        role.Name = null;
                        role.Slug = null;
                       usr.Roles.Add(role);
                    }
                   
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E510 - Erro interno no servidor, Mensagem: {ex.Message}");
            }
            return userRoles;
        }
    }
}
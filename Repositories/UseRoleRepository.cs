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

        public User Busca(int id)
        {
            User userRole = new();
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
                    WHERE U .[Id] = @Id
                ";

                SqlCommand command = new();
                command.CommandText = consultaSql;
                command.Connection = _connection;
                command.Parameters.AddWithValue("@Id", id);
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

                    if(user == null)
                        return new User();

                    if (user != null && userRole == null)
                    { 
                        if (reader["IdRole"] is not DBNull)
                        {
                            role.Id = Convert.ToInt32(reader["IdRole"]);
                            role.Name = Convert.ToString(reader["RoleName"]);
                            role.Slug = Convert.ToString(reader["RoleSlug"]);
                            user?.Roles.Add(role);
                            userRole = user!;
                        }
                        else
                        {
                            role.Id = 0;
                            role.Name = null;
                            role.Slug = null;
                            user?.Roles.Add(role);
                            userRole = user!;
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E510 - Erro interno no servidor, Mensagem: {ex.Message}");
            }
            return userRole;
        }

        public User AdicionarPerfilAoUsuario(int idUser, int idRole)
        {
            User usuario = new();
            Role perfil = new();
            try
            {   
                UserRepository userRepositorio = new(_connection);
                usuario = userRepositorio.Busca(idUser);

                RoleRepository roleRepository = new(_connection);
                perfil = roleRepository.Busca(idRole);

                if(usuario == null || perfil == null)
                    return new User();

                var insertUserrole = @"
                    INSERT INTO [Blog].[dbo].[UserRole] 
                        VALUES
                        (
                            @UserId, @RoleId
                        )
                ";
                
                SqlCommand command = new();
                command.CommandText = insertUserrole;
                command.Connection = _connection;
                command.Parameters.AddWithValue("@UserId", usuario.Id);
                command.Parameters.AddWithValue("@RoleId", perfil.Id);
                command.ExecuteNonQuery();

                // retornando o usuario que foi vinculado ao perfil
                usuario =  Busca(idUser);

                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E510 - Erro interno no servidor, Mensagem: {ex.Message}");
            }
            return usuario;
        }

    }
}
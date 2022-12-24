using Mao_Na_Massa_blog.Models;
using System;
using System.Collections.Generic;
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
            List<Role> roles = new List<Role>();

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
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

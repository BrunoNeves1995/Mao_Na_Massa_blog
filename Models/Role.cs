using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mao_Na_Massa_blog.Models
{
    public class Role
    {

        public Role()
        {

        }

        public Role(string name, string slug)
        {
            Name = name;
            Slug = slug;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        
    }
}

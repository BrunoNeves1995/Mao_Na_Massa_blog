using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mao_Na_Massa_blog.Models
{
    public class Category
    {   

        public Category()
        {
   
        }
        
        public Category(int id, string name, string slug)
        {
            Id = id;
            Name = name;
            Slug = slug;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
    }
}
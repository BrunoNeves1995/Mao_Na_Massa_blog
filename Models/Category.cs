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
            Posts = new List<Post>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }


        // relarionamento 1 para N -> cada categoria pode ter varios posts
        public IEnumerable<Post>? Posts { get; set; }
    }
}
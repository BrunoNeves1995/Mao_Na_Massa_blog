using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mao_Na_Massa_blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Body { get; set; }
        public string? Slug { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        // relarionamento 1 para N -> cada post pode ter uma categoria
        public Category? Category { get; set; }
    }
}

namespace Mao_Na_Massa_blog.Models
{
    public class User
    {   
        public User()
        {
            
        }

        public User(string name, string email, string passwordHash, string bio, string image, string slug)
        {

            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Bio = bio;
            Image = image;
            Slug = slug;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
        public string? Slug { get; set; }

    }
}
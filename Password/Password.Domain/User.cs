using System.ComponentModel.DataAnnotations;

namespace Password.Domain
{
    public class User
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

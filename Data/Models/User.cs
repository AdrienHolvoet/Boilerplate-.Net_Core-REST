using Boilerplate.Data.Models;
using System.ComponentModel.DataAnnotations;


namespace Boilerplate.Data.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}

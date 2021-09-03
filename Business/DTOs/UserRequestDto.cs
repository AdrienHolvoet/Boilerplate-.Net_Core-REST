using System;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Business.Dtos
{
    public class UserRequestDto : BaseDto
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

    }
}

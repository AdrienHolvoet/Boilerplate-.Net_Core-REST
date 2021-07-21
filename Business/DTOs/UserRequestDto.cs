using System;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate_REST.Business.DTOs
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

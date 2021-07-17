using System;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate_REST.Business.DTOs
{
    public class UserResponseDto : BaseDto
    {
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}

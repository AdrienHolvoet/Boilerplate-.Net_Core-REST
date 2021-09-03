using System;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Business.Dtos
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

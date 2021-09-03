using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Boilerplate.Data.Models;

namespace Boilerplate.Business.Dtos
{
    public class AuthenticateRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

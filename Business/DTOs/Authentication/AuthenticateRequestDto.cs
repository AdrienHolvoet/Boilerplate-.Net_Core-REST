using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Boilerplate_REST.Data.Models;

namespace Boilerplate_REST.Business.DTOs
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

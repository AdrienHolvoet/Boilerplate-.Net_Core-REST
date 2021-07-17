using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Boilerplate_REST.Data.Models;

namespace Boilerplate_REST.Business.DTOs
{
    public class AuthenticateResponseDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}

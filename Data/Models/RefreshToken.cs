using Boilerplate_REST.Data.Models;
using System;


namespace Boilerplate.Data.Models
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;
    }
}

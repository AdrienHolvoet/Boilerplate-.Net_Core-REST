using System;

namespace Boilerplate_REST.Business.DTOs
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
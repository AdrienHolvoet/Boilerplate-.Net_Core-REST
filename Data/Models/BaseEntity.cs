using System;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate_REST.Data.Models
{
    public class BaseEntity
    {
        private bool _isEnabled = true;
        public Guid Id { get; set; }
        public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; } }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
using CustomerTest.Domain.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace CustomerTest.Domain
{
    public class Customer : AggregateRoot
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [Required] 
        public string Address { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string? PhoneNumber { get; set; }

        [Required] 
        public string Email { get; set; } = null!;

        public List<Order>? Orders { get; set; }
    }
}
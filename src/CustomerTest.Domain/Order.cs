using CustomerTest.Domain.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace CustomerTest.Domain
{
    public class Order : Entity
    {
        [Required] 
        public double Price { get; set; }
        
        public DateTimeOffset CreateDate { get; set; }
        
        public DateTimeOffset EditDate { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
        
        public Customer? Customer { get; set; }
        
    }
}
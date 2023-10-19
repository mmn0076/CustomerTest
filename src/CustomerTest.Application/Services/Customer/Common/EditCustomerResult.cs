﻿namespace CustomerTest.Application.Services.Customer.Common
{
    public class EditCustomerResult
    {
        public Guid Id { get; set; }
        
        public string? FirstName { get; set; }
        
        public string? LastName { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public string? PhoneNumber { get; set; }
        
        public string? Email { get; set; }

        public string? Address { get; set; }
    }
}

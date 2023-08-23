﻿using CustomerTest.Application.Services.Order.Common;

namespace CustomerTest.Application.Services.Customer.Common
{
    public class GetCustomerResult
    {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }
        
        public string Address { get; set; }
        
        public List<GetOrderResult> Orders { get; set; }
        
    }
}
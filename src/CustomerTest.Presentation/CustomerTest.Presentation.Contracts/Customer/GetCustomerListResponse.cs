﻿namespace CustomerTest.Presentation.Contracts.Customer;

public class GetCustomerListResponse
{
    public string Id { get; set; }
        
    public string FirstName { get; set; }
        
    public string LastName { get; set; }
        
    public string PhoneNumber { get; set; }
        
    public string Email { get; set; }
}
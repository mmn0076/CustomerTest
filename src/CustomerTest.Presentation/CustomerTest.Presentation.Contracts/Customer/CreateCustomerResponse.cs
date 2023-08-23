namespace CustomerTest.Presentation.Contracts.Customer;

public record CreateCustomerResponse
{
    public string Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}
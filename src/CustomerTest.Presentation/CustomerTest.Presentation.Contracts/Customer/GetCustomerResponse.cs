using CustomerTest.Presentation.Contracts.Order;

namespace CustomerTest.Presentation.Contracts.Customer;

public record GetCustomerResponse

{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    
    public List<GetOrderResponse>? Orders { get; set; }
}
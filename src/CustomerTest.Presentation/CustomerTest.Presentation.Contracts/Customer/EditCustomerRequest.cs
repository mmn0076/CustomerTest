namespace CustomerTest.Presentation.Contracts.Customer;

public record EditCustomerRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public string? Address { get; set; }
}
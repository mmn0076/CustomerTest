using ErrorOr;
using CustomerTest.Application.Services.Customer.Common;
using MediatR;

namespace CustomerTest.Application.Services.Customer.Commands;

public record CreateCustomerCommand : IRequest<ErrorOr<CreateCustomerResult>>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public string? Address { get; set; }
}
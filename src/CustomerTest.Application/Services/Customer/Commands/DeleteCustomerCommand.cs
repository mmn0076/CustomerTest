using ErrorOr;
using MediatR;

namespace CustomerTest.Application.Services.Customer.Commands;

public record DeleteCustomerCommand : INotification
{
    public Guid Id { get; set; }

}
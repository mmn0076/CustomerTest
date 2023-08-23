using ErrorOr;
using CustomerTest.Application.Services.Customer.Common;
using MediatR;

namespace CustomerTest.Application.Services.Customer.Queries;

public record GetCustomerQuery : IRequest<ErrorOr<GetCustomerResult>>
{
    public Guid Id { get; set; }
}
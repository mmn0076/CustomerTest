using ErrorOr;
using CustomerTest.Application.Services.Customer.Common;
using MediatR;

namespace CustomerTest.Application.Services.Customer.Queries;

public record GetCustomersQuery : IRequest<ErrorOr<List<GetCustomerListResult>>>
{
    public int Offset { get; set; } = 0;
    public int Limit { get; set; } = 10;

}
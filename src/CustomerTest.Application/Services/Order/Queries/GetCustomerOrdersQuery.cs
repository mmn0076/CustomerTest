using CustomerTest.Application.Services.Order.Common;
using MediatR;
using ErrorOr;

namespace CustomerTest.Application.Services.Order.Queries;

public class GetCustomerOrdersQuery : IRequest<ErrorOr<List<GetOrderResult>>>
{
    public Guid CustomerId { get; set; }
    
    public int Offset { get; set; } = 0;
    
    public int Limit { get; set; } = 10;

}
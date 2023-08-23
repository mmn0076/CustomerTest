using CustomerTest.Application.Services.Order.Common;
using MediatR;
using ErrorOr;
namespace CustomerTest.Application.Services.Order.Commands;

public class CreateOrderCommand : IRequest<ErrorOr<CreateOrderResult>>
{
    public double Price { get; set; }
    
    public string CustomerId { get; set; }
}
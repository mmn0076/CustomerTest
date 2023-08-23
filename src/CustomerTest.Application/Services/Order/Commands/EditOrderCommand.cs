using CustomerTest.Application.Services.Order.Common;
using MediatR;
using ErrorOr;

namespace CustomerTest.Application.Services.Order.Commands;

public class EditOrderCommand : IRequest<ErrorOr<EditOrderResult>>
{
    public Guid Id { get; set; }
    
    public double Price { get; set; }

}
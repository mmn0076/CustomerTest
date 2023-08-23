using CustomerTest.Application.Services.Order.Common;
using MediatR;

namespace CustomerTest.Application.Services.Order.Commands;

public class DeleteOrderCommand : INotification
{
    public Guid Id { get; set; }
}
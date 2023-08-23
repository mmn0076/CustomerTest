using MapsterMapper;
using CustomerTest.Application.Common.Interfaces.Persistence;
using CustomerTest.Application.Services.Order.Common;
using CustomerTest.Domain.Common.Errors;
using MediatR;

namespace CustomerTest.Application.Services.Order.Commands;

public class DeleteOrderCommandHandler : INotificationHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;


    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        await _orderRepository.DeleteOrderAsync(request.Id, cancellationToken);
    }
}
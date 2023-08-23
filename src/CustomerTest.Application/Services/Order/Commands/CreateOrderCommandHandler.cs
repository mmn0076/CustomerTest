using MapsterMapper;
using CustomerTest.Application.Common.Interfaces.Persistence;
using CustomerTest.Application.Services.Order.Common;
using CustomerTest.Domain.Common.Errors;
using MediatR;
using ErrorOr;

namespace CustomerTest.Application.Services.Order.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<CreateOrderResult>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;


    public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<CreateOrderResult>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Domain.Order>(request);
        order.CreateDate = DateTimeOffset.UtcNow;
        
        try
        {
            await _orderRepository.CreateOrderAsync(order, cancellationToken);
        }
        catch (Exception e)
        {
            return Errors.Customer.NotFound;
        }

        return _mapper.Map<CreateOrderResult>(order);
    }
}
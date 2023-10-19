using MapsterMapper;
using CustomerTest.Application.Common.Interfaces.Persistence;
using CustomerTest.Application.Services.Order.Common;
using CustomerTest.Domain.Common.Errors;
using MediatR;
using ErrorOr;
using CustomerTest.Application.Services.Customer.Common;

namespace CustomerTest.Application.Services.Order.Commands;

public class EditOrderCommandHandler : IRequestHandler<EditOrderCommand, ErrorOr<EditOrderResult>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;


    public EditOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<EditOrderResult>> Handle(EditOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.EditOrderAsync(request, cancellationToken);
            if (order.IsError)
            {
                return order.Errors;
            }
            else
            {
                return _mapper.Map<EditOrderResult>(order.Value);
            }
        }
        catch
        {
            return Errors.Common.InternalServerError;
        }
    }
}
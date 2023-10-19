using ErrorOr;
using MapsterMapper;
using CustomerTest.Application.Common.Interfaces.Persistence;
using CustomerTest.Application.Services.Order.Common;
using CustomerTest.Domain.Common.Errors;
using MediatR;

namespace CustomerTest.Application.Services.Order.Queries
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, ErrorOr<GetOrderResult>>
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;


        public GetOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GetOrderResult>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {

            var order = await _orderRepository.GetOrderAsync(request.Id, cancellationToken);

            if (order == null)
            {
                return Errors.Order.NotFound;
            }

            return _mapper.Map<GetOrderResult>(order);
        }
    }
}
using ErrorOr;
using MapsterMapper;
using CustomerTest.Application.Common.Interfaces.Persistence;
using CustomerTest.Application.Services.Order.Common;
using MediatR;

namespace CustomerTest.Application.Services.Order.Queries
{
    public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, ErrorOr<List<GetOrderResult>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;


        public GetCustomerOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<GetOrderResult>>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetCustomerOrdersAsync(request.CustomerId, cancellationToken, request.Offset, request.Limit);

            return _mapper.Map<List<GetOrderResult>>(orders);
        }
    }
}
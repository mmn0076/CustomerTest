using ErrorOr;
using MapsterMapper;
using CustomerTest.Application.Common.Interfaces.Persistence;
using CustomerTest.Application.Services.Customer.Common;
using MediatR;

namespace CustomerTest.Application.Services.Customer.Queries
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, ErrorOr<List<GetCustomerListResult>>>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }


        public async Task<ErrorOr<List<GetCustomerListResult>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {

            var customers = await _customerRepository.GetCustomersAsync(cancellationToken, request.Offset, request.Limit);

            return _mapper.Map<List<GetCustomerListResult>>(customers);
        }
    }
}
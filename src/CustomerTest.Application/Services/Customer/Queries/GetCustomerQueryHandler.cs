using ErrorOr;
using MapsterMapper;
using CustomerTest.Application.Common.Interfaces.Persistence;
using CustomerTest.Application.Services.Customer.Common;
using CustomerTest.Domain.Common.Errors;
using MediatR;

namespace CustomerTest.Application.Services.Customer.Queries
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, ErrorOr<GetCustomerResult>>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }


        public async Task<ErrorOr<GetCustomerResult>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {

            var customer = await _customerRepository.GetCustomerWithOrdersAsync(request.Id, cancellationToken);

            if (customer == null)
            {
                return Errors.Customer.NotFound;
            }

            return _mapper.Map<GetCustomerResult>(customer);
        }
    }
}
using ErrorOr;
using MapsterMapper;
using CustomerTest.Application.Common.Interfaces.Persistence;
using CustomerTest.Application.Services.Customer.Common;
using CustomerTest.Domain.Common.Errors;
using MediatR;


namespace CustomerTest.Application.Services.Customer.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<CreateCustomerResult>>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {

            _customerRepository = customerRepository;
            _mapper = mapper;
        }


        public async Task<ErrorOr<CreateCustomerResult>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Domain.Customer>(request);


            var isDuplicate = await _customerRepository.IsDuplicateEmailAsync(customer.Id, customer.Email, cancellationToken);
            if (isDuplicate)
            {
                return Errors.Customer.DuplicateUser;
            }

            await _customerRepository.CreateCustomerAsync(customer, cancellationToken);

            return _mapper.Map<CreateCustomerResult>(customer);
        }
    }
}

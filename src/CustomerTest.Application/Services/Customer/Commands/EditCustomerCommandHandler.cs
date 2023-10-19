using System.Diagnostics;
using ErrorOr;
using MapsterMapper;
using CustomerTest.Application.Common.Interfaces.Persistence;
using CustomerTest.Application.Services.Customer.Common;
using CustomerTest.Domain.Common.Errors;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CustomerTest.Application.Services.Customer.Commands
{
    public class EditCustomerCommandHandler : IRequestHandler<EditCustomerCommand, ErrorOr<EditCustomerResult>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public EditCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }


        public async Task<ErrorOr<EditCustomerResult>> Handle(EditCustomerCommand request,
            CancellationToken cancellationToken)
        {

            var isDuplicate = await _customerRepository.IsDuplicateEmailAsync(request.Id, request.Email!, cancellationToken);
            if (isDuplicate)
            {
                return Errors.Customer.DuplicateUser;
            }

            try
            {
                var customer  = await _customerRepository.EditCustomerAsync(request, cancellationToken);
                if (customer.IsError)
                {
                    return customer.Errors;
                }
                else
                {
                    return _mapper.Map<EditCustomerResult>(customer.Value);
                }
            }
            catch
            {
                return Errors.Common.InternalServerError;
            }
           
        }
    }
}
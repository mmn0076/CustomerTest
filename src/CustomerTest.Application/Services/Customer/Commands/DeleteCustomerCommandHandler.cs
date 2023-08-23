using MapsterMapper;
using CustomerTest.Application.Common.Interfaces.Persistence;
using MediatR;


namespace CustomerTest.Application.Services.Customer.Commands
{
    public class DeleteCustomerCommandHandler : INotificationHandler<DeleteCustomerCommand>
    {

        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerRepository.DeleteCustomerAsync(request.Id, cancellationToken);
        }
    }
}

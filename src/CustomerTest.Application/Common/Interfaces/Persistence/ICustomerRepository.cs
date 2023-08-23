using ErrorOr;
using CustomerTest.Application.Services.Customer.Commands;
using CustomerTest.Application.Services.Customer.Common;
using CustomerTest.Domain;

namespace CustomerTest.Application.Common.Interfaces.Persistence
{
    public interface ICustomerRepository
    {
        public Task CreateCustomerAsync(Customer customer, CancellationToken cancellationToken);
        public Task<Customer?> GetCustomerAsync(Guid id, CancellationToken cancellationToken);
        public Task<Customer?> GetCustomerWithOrdersAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<Customer>> GetCustomersAsync(CancellationToken cancellationToken, int offset = 0, int limit = 10);
        public Task<bool> IsDuplicateEmailAsync(Guid id,string Email, CancellationToken cancellationToken);
        public Task<ErrorOr<EditCustomerResult>> EditCustomerAsync(EditCustomerCommand command, CancellationToken cancellationToken);
        public Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken);
    }
}

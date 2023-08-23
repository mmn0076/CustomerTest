using ErrorOr;
using MapsterMapper;
using CustomerTest.Application.Common.Interfaces.Persistence;
using CustomerTest.Application.Services.Customer.Commands;
using CustomerTest.Application.Services.Customer.Common;
using CustomerTest.Application.Services.Order.Common;
using CustomerTest.Domain.Common.Errors;
using CustomerTest.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Customer = CustomerTest.Domain.Customer;

namespace CustomerTest.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerTestDbContext _dbContext;
        private readonly IMapper _mapper;


        public CustomerRepository(CustomerTestDbContext customerTestDbContext, IMapper mapper)
        {
            _dbContext = customerTestDbContext;
            _mapper = mapper;
        }

        public async Task CreateCustomerAsync(Customer customer, CancellationToken ct)
        {
            _dbContext.Set<Customer>().Add(customer);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task<Customer?> GetCustomerAsync(Guid id, CancellationToken ct)
        {
            return await _dbContext.Set<Customer>().AsQueryable().AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<Customer?> GetCustomerWithOrdersAsync(Guid id, CancellationToken ct)
        {
            return await _dbContext.Set<Customer>().AsQueryable().AsNoTracking().Include(o => o.Orders)
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<List<Customer>> GetCustomersAsync(CancellationToken ct, int offset = 0,
            int limit = 10)
        {
            var customers = await _dbContext.Set<Customer>()
                .AsQueryable()
                .Skip(offset)
                .Take(limit)
                .ToListAsync(ct);

            return customers;
        }

        public async Task<bool> IsDuplicateEmailAsync(Guid id, string Email, CancellationToken ct)
        {
            return await _dbContext.Set<Customer>().AsQueryable().AnyAsync(x => x.Id != id && x.Email == Email, cancellationToken: ct);
        }



        public async Task<ErrorOr<EditCustomerResult>> EditCustomerAsync(EditCustomerCommand command, CancellationToken ct)
        {
            var customer = await _dbContext.Set<Customer>().AsQueryable().FirstOrDefaultAsync(x => x.Id == command.Id, ct);

            if (customer == null)
            {
                return Errors.Customer.NotFound;
            }

            customer.FirstName = command.FirstName;
            customer.LastName = command.FirstName;
            customer.DateOfBirth = command.DateOfBirth;
            customer.PhoneNumber = command.PhoneNumber;
            customer.Email = command.Email;
            customer.Address = command.Address;
            
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync(ct);

            return _mapper.Map<EditCustomerResult>(customer);
        }


        public async Task DeleteCustomerAsync(Guid id, CancellationToken ct)
        {
            await _dbContext.Set<Customer>().AsQueryable().Where(x => x.Id == id).ExecuteDeleteAsync(ct);
        }
    }
}
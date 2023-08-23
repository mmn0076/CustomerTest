using ErrorOr;
using MapsterMapper;
using CustomerTest.Application.Common.Interfaces.Persistence;
using CustomerTest.Application.Services.Order.Commands;
using CustomerTest.Application.Services.Order.Common;
using CustomerTest.Domain;
using CustomerTest.Domain.Common.Errors;
using CustomerTest.Infrastructure.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Customer = CustomerTest.Domain.Customer;

namespace CustomerTest.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CustomerTestDbContext _dbContext;
        private readonly IMapper _mapper;


        public OrderRepository(CustomerTestDbContext customerTestDbContext, IMapper mapper)
        {
            _dbContext = customerTestDbContext;
            _mapper = mapper;
        }

        public async Task CreateOrderAsync(Order order, CancellationToken ct)
        {
            _dbContext.Set<Order>().Add(order);
            await _dbContext.SaveChangesAsync(ct);
        }

        public async Task<Order?> GetOrderAsync(Guid id, CancellationToken ct)
        {
            return await _dbContext.Set<Order>().AsQueryable().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<List<Order>> GetCustomerOrdersAsync(Guid customerId, CancellationToken ct, int offset = 0,
            int limit = 10)
        {
            var customers = await _dbContext.Set<Order>()
                .AsQueryable()
                .Where(x => x.CustomerId == customerId)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(ct);

            return customers;
        }

        public async Task<ErrorOr<GetOrderResult>> EditOrderAsync(EditOrderCommand command, CancellationToken ct)
        {
            var order = await _dbContext.Set<Order>().AsQueryable().FirstOrDefaultAsync(x => x.Id == command.Id, ct);

            if (order == null)
            {
                return Errors.Order.NotFound;
            }

            order.Price = command.Price;
            order.EditDate = DateTimeOffset.UtcNow;

            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync(ct);

            return _mapper.Map<GetOrderResult>(order);
        }


        public async Task DeleteOrderAsync(Guid id, CancellationToken ct)
        {
            await _dbContext.Set<Order>().AsQueryable().Where(x => x.Id == id).ExecuteDeleteAsync(ct);
        }
    }
}
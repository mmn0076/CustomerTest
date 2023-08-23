using ErrorOr;
using CustomerTest.Application.Services.Order.Commands;
using CustomerTest.Application.Services.Order.Common;
using CustomerTest.Domain;

namespace CustomerTest.Application.Common.Interfaces.Persistence
{
    public interface IOrderRepository
    {
        public Task CreateOrderAsync(Order order, CancellationToken cancellationToken);
        public Task<Order?> GetOrderAsync(Guid id, CancellationToken cancellationToken);
        public Task<List<Order>> GetCustomerOrdersAsync(Guid customerId,CancellationToken cancellationToken ,int offset = 0, int limit = 10);
        public Task<ErrorOr<GetOrderResult>> EditOrderAsync(EditOrderCommand command, CancellationToken cancellationToken);
        public Task DeleteOrderAsync(Guid id, CancellationToken cancellationToken);
    }
}

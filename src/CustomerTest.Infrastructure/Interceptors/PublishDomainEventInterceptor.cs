using CustomerTest.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CustomerTest.Infrastructure.Interceptors
{
    public class PublishDomainEventInterceptor : SaveChangesInterceptor
    {
        private readonly IMediator _mediator;

        public PublishDomainEventInterceptor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
            CancellationToken cancellationToken = new CancellationToken())
        {
            PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private async Task PublishDomainEvents(DbContext? dbContext)
        {
            if (dbContext is null)
            {
                return;
            }

            var entities = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
                .Where(entry => entry.Entity.DomainEvents.Any()).Select(entry => entry.Entity);

            var domainEvents = entities.SelectMany(entry => entry.DomainEvents).ToList();

            foreach (var entity in entities)
            {
                entity.ClearDomainEvents();
            }

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
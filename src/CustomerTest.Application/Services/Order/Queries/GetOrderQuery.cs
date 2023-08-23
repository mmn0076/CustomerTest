using CustomerTest.Application.Services.Order.Common;
using ErrorOr;
using MediatR;

namespace CustomerTest.Application.Services.Order.Queries;

public class GetOrderQuery : IRequest<ErrorOr<GetOrderResult>>
{
    public Guid Id { get; set; }
}
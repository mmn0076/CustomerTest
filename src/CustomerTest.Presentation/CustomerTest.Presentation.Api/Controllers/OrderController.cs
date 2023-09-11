using MapsterMapper;
using CustomerTest.Application.Services.Customer.Commands;
using CustomerTest.Application.Services.Customer.Queries;
using CustomerTest.Application.Services.Order.Commands;
using CustomerTest.Application.Services.Order.Queries;
using CustomerTest.Presentation.Contracts.Customer;
using CustomerTest.Presentation.Contracts.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerTest.Presentation.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class OrderController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public OrderController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        #region Order Management

        /// <summary>
        /// New Order
        /// </summary>
        /// <param name="order">Order Details</param>
        /// <returns>Order Details</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest order, CancellationToken ct)
        {
            var command = _mapper.Map<CreateOrderCommand>(order);

            var createResult = await _mediator.Send(command, ct);

            return createResult.Match(
                value => Ok(_mapper.Map<CreateOrderReponse>(value)),
                Problem);
        }

        /// <summary>
        /// Get Single Order
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>Order Details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id, CancellationToken ct)
        {
            if (!Utils.Utils.IsGuid(id))
            {
                return BadRequest("Invalid Guid");
            }

            var getResult = await _mediator.Send(new GetOrderQuery() { Id = Guid.Parse(id) }, ct);

            return getResult.Match(
                value => Ok(_mapper.Map<GetOrderResponse>(value)),
                Problem);
        }
        
        /// <summary>
        /// Edit an Order by id
        /// </summary>
        /// <param name="id">Order id</param>
        /// <param name="order">Order Edit Model</param>
        /// <returns>Edited Order Details</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] EditOrderRequest order,
            CancellationToken ct)
        {
            if (!Utils.Utils.IsGuid(id))
            {
                return BadRequest("Invalid Guid");
            }

            var command = _mapper.Map<EditOrderCommand>(order);
            command.Id = Guid.Parse(id);

            var editResult = await _mediator.Send(command, ct);

            return editResult.Match(
                value => Ok(_mapper.Map<EditOrderResponse>(value)),
                Problem);
        }

        /// <summary>
        /// Delete an Order by id
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>200</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken ct)
        {
            if (!Utils.Utils.IsGuid(id))
            {
                return BadRequest("Invalid Guid");
            }

            await _mediator.Publish(new DeleteOrderCommand() { Id = Guid.Parse(id) }, ct);

            return Ok();
        }
        
        public int Offset { get; set; } = 0;
        
        public int Limit { get; set; } = 10;

        #endregion

    }
}
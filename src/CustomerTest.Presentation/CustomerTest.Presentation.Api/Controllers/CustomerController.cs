using MapsterMapper;
using CustomerTest.Application.Services.Customer.Commands;
using CustomerTest.Application.Services.Customer.Queries;
using CustomerTest.Application.Services.Order.Queries;
using CustomerTest.Presentation.Api.Controllers.Abstraction;
using CustomerTest.Presentation.Contracts.Customer;
using CustomerTest.Presentation.Contracts.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerTest.Presentation.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class CustomerController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public CustomerController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        #region Customer Management

        /// <summary>
        /// New Customer
        /// </summary>
        /// <param name="customer">Customer Details</param>
        /// <returns>Customer Details</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerRequest customer, CancellationToken ct)
        {
            var command = _mapper.Map<CreateCustomerCommand>(customer);

            var createResult = await _mediator.Send(command, ct);

            return createResult.Match(
                value => Ok(_mapper.Map<CreateCustomerResponse>(value)),
                Problem);
        }

        /// <summary>
        /// Get Single Customer
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>Customer Details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id, CancellationToken ct)
        {
            if (!Utils.Utils.IsGuid(id))
            {
                return BadRequest("Invalid Guid");
            }

            var getResult = await _mediator.Send(new GetCustomerQuery() { Id = Guid.Parse(id) }, ct);

            return getResult.Match(
                value => Ok(_mapper.Map<GetCustomerResponse>(value)),
                Problem);
        }

        /// <summary>
        /// Get List of Customers With Pagination
        /// </summary>
        /// <param name="offset">offset from start</param>
        /// <param name="limit">Count of Items Client Wants</param>
        /// <returns>List Of Customer Details</returns>
        [HttpGet("List")]
        public async Task<IActionResult> List(CancellationToken ct, [FromQuery] GetCustomersRequst request)
        {
            var command = _mapper.Map<GetCustomersQuery>(request);

            var getResult = await _mediator.Send(command, ct);

            return getResult.Match(
                value => Ok(_mapper.Map<List<GetCustomerListResponse>>(value)),
                Problem);
        }

        /// <summary>
        /// Edit a Customer by id
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <param name="customer">Customer Edit Model</param>
        /// <returns>Edited Customer Details</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] EditCustomerRequest customer,
            CancellationToken ct)
        {
            if (!Utils.Utils.IsGuid(id))
            {
                return BadRequest("Invalid Guid");
            }

            var command = _mapper.Map<EditCustomerCommand>(customer);
            command.Id = Guid.Parse(id);

            var editResult = await _mediator.Send(command, ct);

            return editResult.Match(
                value => Ok(_mapper.Map<EditCustomerResponse>(value)),
                Problem);
        }

        /// <summary>
        /// Delete a Customer by id
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>200</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken ct)
        {
            if (!Utils.Utils.IsGuid(id))
            {
                return BadRequest("Invalid Guid");
            }

            await _mediator.Publish(new DeleteCustomerCommand() { Id = Guid.Parse(id) }, ct);

            return Ok();
        }

        #endregion

        #region Customer Orders

        /// <summary>
        /// Get List of Customer Orders With Pagination
        /// </summary>
        /// <param name="offset">offset from start</param>
        /// <param name="limit">Count of Items Client Wants</param>
        /// <returns>List Of Customer Orders Details</returns>
        [HttpGet("{customerId}/Orders")]
        public async Task<IActionResult> OrdersList([FromRoute] string customerId, CancellationToken ct,
            [FromQuery] int offset = 0, [FromQuery] int Limit = 10)
        {
            if (!Utils.Utils.IsGuid(customerId))
            {
                return BadRequest("Invalid Guid");
            }

            var getResult = await _mediator.Send(new GetCustomerOrdersQuery() { CustomerId = Guid.Parse(customerId), Limit = Limit, Offset = offset }, ct);

            return getResult.Match(
                value => Ok(_mapper.Map<List<GetOrderResponse>>(value)),
                Problem);
        }

        #endregion
    }
}
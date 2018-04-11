using NordicStore.Domain.Commands;
using NordicStore.Domain.Handlers;
using NordicStore.Domain.Repositories;
using NordicStore.Infra.Queries;
using NordicStore.Infra.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NordicStore.API.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerQueryRepository _queryRepository;
        private CustomerHandler _customerHandler;

        public CustomerController(ICustomerQueryRepository queryRepository, CustomerHandler customerHandler)
        {
            _queryRepository = queryRepository;
            _customerHandler = customerHandler;
        }

        [HttpGet]
        [Route("v1/customers/{id}")]
        public async Task<CustomerPopulatedQuery> Get(int id)
        {
            return await _queryRepository.Get(id);
        }

        [HttpGet]
        [Route("v1/customers")]
        public async Task<List<CustomerListedQuery>> Get()
        {
            return await _queryRepository.GetAll();
        }

        [HttpPost]
        [Route("v1/customers")]
        public async Task<ActionResult> Post([FromBody]CreateCustomerCommand command)
        {
            var commandResult = await _customerHandler.Handle(command);

            if (!commandResult.Success)
                return BadRequest(commandResult.Data);

            return Ok(commandResult.Data);
        }

        [HttpPost]
        [Route("v1/customers/{customerId}/orders")]
        public async Task<ActionResult> Post(int customerId, [FromBody]AddOrderToCustomerCommand command)
        {
            command.CustomerId = customerId;
            var commandResult = await _customerHandler.Handle(command);

            if (!commandResult.Success)
                return BadRequest(commandResult.Data);

            return Ok(commandResult.Data);
        }
    }
}
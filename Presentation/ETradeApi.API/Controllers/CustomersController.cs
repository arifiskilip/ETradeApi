using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETradeApi.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomersController : ControllerBase
	{
		private readonly ICustomerWriteRepository _customerWriteRepository;
		private readonly ICustomerReadRepository _customerReadRepository;

		public CustomersController(ICustomerWriteRepository customerWriteRepository, ICustomerReadRepository customerReadRepository)
		{
			_customerWriteRepository = customerWriteRepository;
			_customerReadRepository = customerReadRepository;
		}

		[HttpGet]
		public async Task<ActionResult> GetAll() {
			Customer customer = new()
			{
				Name = "Arif2",
			};
			await _customerWriteRepository.AddAsync(customer);
			await _customerWriteRepository.SaveAsync();
			return Ok(_customerReadRepository.GetAll());
		}
		[HttpPost("Add")]
		public async Task<ActionResult> Add([FromBody]Customer customer)
		{
			return Ok(await _customerWriteRepository.AddAsync(customer));
		}
	}
}

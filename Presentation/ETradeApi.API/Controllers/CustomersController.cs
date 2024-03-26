using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETradeApi.API.Controllers
{
	[Route("api/[controller]/[action]")]
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
			return Ok(await _customerReadRepository.GetAll().ToListAsync());
		}
		[HttpPost]
		public async Task<ActionResult> Add([FromBody]Customer customer)
		{
			return Ok(await _customerReadRepository.GetAll().ToListAsync());
		}
	}
}

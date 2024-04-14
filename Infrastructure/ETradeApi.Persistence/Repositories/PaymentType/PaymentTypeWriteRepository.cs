using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Persistence.Contexts;

namespace ETradeApi.Persistence.Repositories;

public class PaymentTypeWriteRepository : WriteRepository<PaymentType>, IPaymentTypeWriteRepository
{
	public PaymentTypeWriteRepository(ETradeApiContext context) : base(context)
	{
	}
}

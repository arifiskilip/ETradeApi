using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Persistence.Contexts;

namespace ETradeApi.Persistence.Repositories;

public class PaymentTypeReadRepository : ReadRepository<PaymentType>, IPaymentTypeReadRepository
{
	public PaymentTypeReadRepository(ETradeApiContext context) : base(context)
	{
	}
}

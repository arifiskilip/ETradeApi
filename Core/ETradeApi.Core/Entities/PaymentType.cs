using ETradeApi.Core.Entities.Common;

namespace ETradeApi.Core.Entities
{
	public class PaymentType : BaseEntity
	{
        public string Type { get; set; }

        public Order Order { get; set; }
    }
}

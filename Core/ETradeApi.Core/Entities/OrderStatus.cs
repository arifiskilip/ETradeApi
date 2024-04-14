using ETradeApi.Core.Entities.Common;

namespace ETradeApi.Core.Entities
{
	public class OrderStatus : BaseEntity
	{
        public string Name { get; set; }
        public Order Order { get; set; }
    }
}

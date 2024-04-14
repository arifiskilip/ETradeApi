using ETradeApi.Core.Entities.Common;

namespace ETradeApi.Core.Entities
{
	public class OrderDetail : BaseEntity
	{
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }


        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}

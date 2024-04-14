using ETradeApi.Core.Entities.Common;
using ETradeApi.Core.Entities.Identity;

namespace ETradeApi.Core.Entities
{
	public class Basket : BaseEntity
	{
        public string AppUserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }


        public AppUser AppUser { get; set; }
        public Product Product { get; set; }
    }
}

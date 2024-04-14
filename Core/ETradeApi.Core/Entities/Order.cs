using ETradeApi.Core.Entities.Common;

namespace ETradeApi.Core.Entities;

public class Order :BaseEntity
{
    public string Description { get; set; }
    public string Address { get; set; }
    public Guid OrderStatusId { get; set; }
    public Guid PaymentTypeId { get; set; }


    public OrderStatus OrderStatus { get; set; }
    public PaymentType PaymentType { get; set; }
}

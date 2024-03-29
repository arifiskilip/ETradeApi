﻿using ETradeApi.Core.Entities.Common;

namespace ETradeApi.Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public string[]? Images { get; set; }

    public ICollection<Order> ?Orders { get; set; }
}

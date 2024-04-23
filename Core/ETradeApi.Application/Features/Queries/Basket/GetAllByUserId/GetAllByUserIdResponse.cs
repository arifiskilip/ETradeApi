using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.Features.Queries.Basket.GetAllByUserId
{
	public class GetAllByUserIdResponse
	{
        public List<ETradeApi.Core.Entities.Basket> Baskets { get; set; }
    }
}

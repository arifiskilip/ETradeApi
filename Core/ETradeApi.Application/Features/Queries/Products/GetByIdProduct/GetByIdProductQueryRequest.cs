﻿using MediatR;

namespace ETradeApi.Application.Features.Queries.Products.GetByIdProduct
{
	public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>
	{
        public string? Id { get; set; }
    }
}

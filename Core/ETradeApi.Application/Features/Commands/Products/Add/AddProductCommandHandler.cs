using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Infrastructure.Helpers.FileHelper;
using ETradeApi.Infrastructure.Results;
using MediatR;

namespace ETradeApi.Application.Features.Commands.Products.Add
{
	public class AddProductCommandHandler : IRequestHandler<AddProductCommandRequest, AddProductCommandResponse>
	{
		private readonly IProductWriteRepository _productWriteRepository;

		public AddProductCommandHandler(IProductWriteRepository productWriteRepository)
		{
			_productWriteRepository = productWriteRepository;
		}

		public async Task<AddProductCommandResponse> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
		{
			var prductImages = FileHelper.Add(request.Images);
			if (prductImages.Success)
			{
				var addedProduct = await _productWriteRepository.AddAsync(new Product()
				{
					Name = request.Name,
					Price = request.Price,
					Stock = request.Stock,
					Images = prductImages.Data.ToList()
				});
				await _productWriteRepository.SaveAsync();
				return new()
				{
					Data = new SuccessDataResult<Product>(new Product()
					{
						Id = addedProduct.Entity.Id,
						CreatedDate = addedProduct.Entity.CreatedDate,
						Images = addedProduct.Entity.Images,
						Name = addedProduct.Entity.Name,
						Price = addedProduct.Entity.Price,
						Stock = addedProduct.Entity.Stock,
						UpdatedDate = addedProduct.Entity.UpdatedDate

					}, "Ekeleme işlemi başarılı")
				};
			}
			return new()
			{
				Data = new ErrorDataResult<Product>(prductImages.Message),
			};
		}
	}
}

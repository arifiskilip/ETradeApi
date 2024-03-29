using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Infrastructure.Helpers.FileHelper;
using ETradeApi.Infrastructure.Results;

namespace ETradeApi.API.Models;

public class ProductCreateModel
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public IFormFile[] Images { get; set; }
}

public class ProductManager
{
    private readonly IProductWriteRepository repository;

	public ProductManager(IProductWriteRepository repository)
	{
		this.repository = repository;
	}

	public async Task<IDataResult<Product>> AddAsync(ProductCreateModel model)
    {
        //validation
        var prductImages = FileHelper.Add(model.Images);
        if (prductImages.Success)
        {
			var addedProduct = await this.repository.AddAsync(new Product()
			{
				Name = model.Name,
				Price = model.Price,
				Stock = model.Stock,
				Images = prductImages.Data.ToArray()
			});
			await this.repository.SaveAsync();
			return new SuccessDataResult<Product>(new Product()
			{
				Id = addedProduct.Entity.Id,
				CreatedDate = addedProduct.Entity.CreatedDate,
				Images = addedProduct.Entity.Images,
				Name = addedProduct.Entity.Name,
				Price = addedProduct.Entity.Price,
				Stock = addedProduct.Entity.Stock,
				UpdatedDate = addedProduct.Entity.UpdatedDate

			}, "Ekeleme işlemi başarılı");
		}
		return new ErrorDataResult<Product>(prductImages.Message);
       
    }
}

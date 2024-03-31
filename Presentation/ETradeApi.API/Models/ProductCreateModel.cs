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

public class ProductUpdateModel
{
	public string Id { get; set; }
	public string Name { get; set; }
	public double Price { get; set; }
	public int Stock { get; set; }
	public IFormFile[]? Images { get; set; }
}

public class ProductManager
{
	private readonly IProductWriteRepository repository;
	private readonly IProductReadRepository repostiry2;

	public ProductManager(IProductWriteRepository repository, IProductReadRepository repostiry2)
	{
		this.repository = repository;
		this.repostiry2 = repostiry2;
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
				Images = prductImages.Data.ToList()
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

	public async Task<IDataResult<Product>> Update(ProductUpdateModel model)
	{
		//validation
		IDataResult<List<string>> prductImages = new SuccessDataResult<List<string>>();
		try
		{
			var checkProduct = await this.repostiry2.GetByIdAsync(model.Id.ToString());
			prductImages = FileHelper.Add(model.Images);
			if (prductImages.Success)
			{
				checkProduct.Price = model.Price;
				checkProduct.Stock = model.Stock;
				foreach (var item in prductImages.Data)
				{
					checkProduct.Images.Add(item);
				}
				this.repository.Update(checkProduct);
				await this.repository.SaveAsync();
				return new SuccessDataResult<Product>(checkProduct, "Günceleme işlemi başarılı");
			}
			return new ErrorDataResult<Product>(prductImages.Message);
		}
		catch (Exception)
		{
			if (prductImages.Data.Count>0)
			{
				FileHelper.Delete(prductImages.Data);
			}
			return new ErrorDataResult<Product>("İşlem sırasında hata meydana geldi.");
		}


	}
}

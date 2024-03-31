using ETradeApi.Infrastructure.Results;
using Microsoft.AspNetCore.Http;
using IResult = ETradeApi.Infrastructure.Results.IResult;

namespace ETradeApi.Infrastructure.Helpers.FileHelper
{
	public static class FileHelper
	{
		private static string _currentFileDirectory = Environment.CurrentDirectory + "\\wwwroot";
		private static string _folderName = "\\Images\\";

		public static IDataResult<List<string>> Add(IFormFile[] files)
		{
			List<string> addedImages = new();
			try
			{
				foreach (var file in files)
				{
					var fileExist = CheckFileExists(file);
					if (!fileExist.Success)
					{
						DeleteImagesWithErrorStatus(addedImages);
						return new ErrorDataResult<List<string>>(fileExist.Message);
					}

					var type = Path.GetExtension(file.FileName);
					var typeValid = CheckFileTypeValid(type);
					if (!typeValid.Success)
					{
						DeleteImagesWithErrorStatus(addedImages);
						return new ErrorDataResult<List<string>>(typeValid.Message);
					}

					var randomGuid = Guid.NewGuid().ToString();

					var directory = _currentFileDirectory + _folderName;
					var fileDirectory = directory + randomGuid + type;

					CheckFileDirectoryExist(directory);
					CreateImageFile(fileDirectory, file);

					var fileAddressToBeSavedOnDatabase = _folderName + randomGuid + type;
					var productImageUrl = fileAddressToBeSavedOnDatabase.Replace("\\", "/");
					addedImages.Add(productImageUrl);
				}
				return new SuccessDataResult<List<string>>(addedImages,"Ekleme işlemi başarılı!");

			}
			catch (Exception ex)
			{

				DeleteImagesWithErrorStatus(addedImages);
				return new ErrorDataResult<List<string>>("İşlem sırsasında hata meydana geldi!");
			}
		}

		public static IResult Update(IFormFile file, string imagePath)
		{
			var fileExist = CheckFileExists(file);
			if (!fileExist.Success)
			{
				return new ErrorResult(fileExist.Message);
			}

			var type = Path.GetExtension(file.FileName);
			var typeValid = CheckFileTypeValid(type);
			if (!typeValid.Success)
			{
				return new ErrorResult(typeValid.Message);
			}
			var randomGuid = Guid.NewGuid().ToString();

			var directory = _currentFileDirectory + _folderName;
			var fileDirectory = directory + randomGuid + type;

			DeleteOldImageFile(imagePath.Replace("/", "\\"));
			CheckFileDirectoryExist(directory);
			CreateImageFile(fileDirectory, file);

			// Message of the result returns the ImagePath of added image.
			var fileAddressToBeSavedOnDatabase = _folderName + randomGuid + type;
			return new SuccessResult(fileAddressToBeSavedOnDatabase.Replace("\\", "/"));
		}


		public static IResult Delete(string path)
		{
			DeleteOldImageFile(path.Replace("/", "\\"));
			return new SuccessResult();
		}

		public static IResult Delete(string[]? paths)
		{
			if (paths != null && paths.Count() > 0)
			{
				foreach (var path in paths)
				{
					DeleteOldImageFile(path.Replace("/", "\\"));
					return new SuccessResult();
				}
				return new ErrorResult("Resim silinemedi!");
			}
			return new SuccessResult();
		}




		private static IResult CheckFileTypeValid(string type)
		{
			if (type == ".jpeg" || type == ".png" || type == ".jpg")
			{
				return new SuccessResult();
			}
			return new ErrorResult("Ekliyeceğiniz dosya resim dosyası olmalı. ('.jpeg', '.png' or '.jpg')");
		}


		private static IResult CheckFileExists(IFormFile file)
		{
			if (file != null && file.Length > 0)
			{
				return new SuccessResult();
			}
			return new ErrorResult("Lütfen bir resim ekleyiniz!");
		}

		private static void DeleteOldImageFile(string directory)
		{
			var fullDirectory = Environment.CurrentDirectory + "\\wwwroot" + directory;
			if (File.Exists(fullDirectory))
			{
				File.Delete(fullDirectory);
			}
		}

		private static void CheckFileDirectoryExist(string directory)
		{
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}
		}

		private static void CreateImageFile(string directory, IFormFile file)
		{
			using (FileStream fileStream = File.Create(directory))
			{
				file.CopyTo(fileStream);
				fileStream.Flush();
			}
		}

		private static void DeleteImagesWithErrorStatus(List<string> imagesPath)
		{
			if (imagesPath.Count > 0)
			{
				foreach (var item in imagesPath)
				{
					Delete(item);
				}
			}
		}
	}
}

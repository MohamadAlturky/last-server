

using SharedKernal.Utilities.Result;

namespace Presentation.Services.MealsImagesSaver;

public class AssetsSaver : IAssetsSaver
{
	private readonly IWebHostEnvironment _environment;

	public AssetsSaver(IWebHostEnvironment environment)
	{
		_environment = environment;
	}

	public async Task<Result<string>> SaveFile(IFormFile file, string folderPath)
	{
		if (file == null || file.Length == 0)
		{
			return Result.Failure<string>(new SharedKernal.Utilities.Errors.Error("if (file == null || file.Length == 0)", "if (file == null || file.Length == 0)"));
		}
		else
		{

			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";


			fileName = folderPath + "\\" + fileName;


			var wwwrootPath = _environment.WebRootPath;

			var filePath = Path.Combine(wwwrootPath, fileName);


			//using (var stream = new FileStream(filePath, FileMode.Create))
			//{
			//	file.CopyToAsync(stream);
			//}
			using (FileStream stream = System.IO.File.Create(filePath))
			{
				await file.CopyToAsync(stream);
			}


			return Result.Success<string>(fileName);
		}
	}
}
	

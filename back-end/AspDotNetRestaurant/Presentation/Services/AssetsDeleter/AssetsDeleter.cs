namespace Presentation.Services.MealsImagesDeleter;

public class AssetsDeleter : IAssetsDeleter
{
	private readonly IWebHostEnvironment _environment;

	public AssetsDeleter(IWebHostEnvironment environment)
	{
		_environment = environment;
	}


	public bool DeleteImage(string fileName)
	{
		if (string.IsNullOrEmpty(fileName))
		{
			throw new FileNotFoundException("Invalid file name");
		}

		var wwwrootPath = _environment.WebRootPath;

		var filePath = Path.Combine(wwwrootPath, fileName);

		try
		{
			File.Delete(filePath);
		}
		catch (Exception ex)
		{
			throw new FieldAccessException("There Are Some Problems When Try To Delete The File { " + fileName + " }"
				+ "Error Message { " + ex.Message + " }");
		}
		return true;
	}
}


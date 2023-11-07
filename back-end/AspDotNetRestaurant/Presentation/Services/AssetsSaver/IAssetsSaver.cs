using SharedKernal.Utilities.Result;

namespace Presentation.Services.MealsImagesSaver;

public interface IAssetsSaver
{
	Task<Result<string>> SaveFile(IFormFile image, string folderPath);
}

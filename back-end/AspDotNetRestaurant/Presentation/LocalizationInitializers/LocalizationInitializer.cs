using Localization.LocalizationBuilders;

namespace Presentation.DataBaseSeedingExtension;

public static class LocalizationInitializers
{
	public static Task BuildLocalizationRequirements(this WebApplication application)
	{
		using var scope = application.Services.CreateScope();
		LocalizationBuilder domainLocalizationBuilder =
			scope.ServiceProvider.GetRequiredService<LocalizationBuilder>();
		domainLocalizationBuilder.Build();
		return Task.CompletedTask;
	}
}

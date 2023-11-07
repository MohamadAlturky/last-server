using Domain.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Localization.LocalizationBuilders;
public class LocalizationBuilder
{
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public LocalizationBuilder(IServiceScopeFactory serviceScopeFactory)
	{
		_serviceScopeFactory = serviceScopeFactory;
	}


	public void Build()
	{	
		using IServiceScope scope = _serviceScopeFactory.CreateScope();

		IDomainLocalizer localizer = scope.ServiceProvider
			.GetRequiredService<IDomainLocalizer>();

		LocalizationProvider.SetLocalizer(localizer);
	}
}

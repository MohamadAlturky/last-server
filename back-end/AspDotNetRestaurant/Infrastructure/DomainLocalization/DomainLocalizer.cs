using Domain.Localization;
using Microsoft.Extensions.Localization;
using Localization;

namespace Infrastructure.DomainLocalization;
public class DomainLocalizer : IDomainLocalizer
{
	private readonly IStringLocalizer<SharedResourcesProvider> _stringLocalizer;

	public DomainLocalizer(IStringLocalizer<SharedResourcesProvider> stringLocalizer)
	{
		_stringLocalizer = stringLocalizer;
	}

	public string GetResource(string key)
	{
		return _stringLocalizer[key];
	}
}

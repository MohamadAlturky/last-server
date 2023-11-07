namespace Domain.Localization;
public static class LocalizationProvider
{
	private static IDomainLocalizer? _localizer;

	public static void SetLocalizer(IDomainLocalizer localizer)
	{
		_localizer = localizer;
	}


	public static string GetResource(string resourceKey)
	{
		if (_localizer == null)
		{
			throw new ApplicationException("IStringLocalizer<DomainResources> GetLocalizer()");
		}
		return _localizer.GetResource(resourceKey);
	}
}

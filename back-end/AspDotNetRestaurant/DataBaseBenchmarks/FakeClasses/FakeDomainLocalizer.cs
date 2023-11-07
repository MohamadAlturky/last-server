using Domain.Localization;

namespace DataBaseBenchmarks.FakeClasses;
public class FakeDomainLocalizer : IDomainLocalizer
{
	public string GetResource(string key)
	{
		return key;
	}
}

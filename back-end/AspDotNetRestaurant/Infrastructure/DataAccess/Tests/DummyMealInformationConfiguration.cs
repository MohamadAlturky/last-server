using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Tests;
internal class DummyMealInformationConfiguration : IEntityTypeConfiguration<DummyMealInformation>
{
	public void Configure(EntityTypeBuilder<DummyMealInformation> builder)
	{

	}
}

using NetArchTest.Rules;
using Xunit;
using FluentAssertions;
using SharedResources.AssemblyReference;

namespace ArchitectureTests.Dependencies;


public class DependencyTester
{
	private const string DomainNameSpace = "Domain";
	private const string ApplicationNameSpace = "Application";
	private const string InfrastructureNameSpace = "Infrastructure";
	private const string PresentationNameSpace = "Presentation";
	private const string SharedResourcesNameSpace = "SharedResources";

	[Fact]
	public void Domain_ShouldNot_HaveDependencyOnOtherProjectsExceptSharedKernal()
	{

		// Arrange

		var assembly = typeof(SharedResourcesAssemblyReference).Assembly;

		string[] otherProjects = new[]
		{
			ApplicationNameSpace,
			InfrastructureNameSpace,
			PresentationNameSpace,
			SharedResourcesNameSpace
		};


		// Act

		var result = Types.InAssembly(assembly)
						.ShouldNot()
						.HaveDependencyOnAll(otherProjects)
						.GetResult();


		// Assert
		result.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void Application_ShouldNot_HaveDependencyOnOtherProjectsExceptDomain()
	{
		// Arrange

		var assembly = typeof(Application.AssemblyReference.ApplicationAssemblyReference).Assembly;

		string[] otherProjects = new[]
		{
			InfrastructureNameSpace,
			PresentationNameSpace,
			SharedResourcesNameSpace
		};


		// Act

		var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();


		// Assert
		result.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void Infrastructure_ShouldNot_HaveDependencyOnOtherProjectsExceptDomainAndAppliaction()
	{
		// Arrange

		var assembly = typeof(Infrastructure.AssemblyReference.InfrastructureAssemblyReference).Assembly;

		string[] otherProjects = new[]
		{
			PresentationNameSpace,
			SharedResourcesNameSpace
		};


		// Act

		var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();


		// Assert
		result.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void SharedRecources_ShouldNot_HaveDependencyOnOtherProjectsExceptDomain()
	{
		// Arrange

		var assembly = typeof(Infrastructure.AssemblyReference.InfrastructureAssemblyReference).Assembly;

		string[] otherProjects = new[]
		{
			PresentationNameSpace,
			InfrastructureNameSpace
		};


		// Act

		var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();


		// Assert
		result.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void ThisTestShouldFail_Application_ShouldNot_HaveDependencyOnDomain()
	{
		// Arrange

		var assembly = typeof(Application.AssemblyReference.ApplicationAssemblyReference).Assembly;

		string[] otherProjects = new[]
		{
			DomainNameSpace
		};


		// Act

		var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();


		// Assert
		result.IsSuccessful.Should().BeTrue();
	}
}



using System.Reflection;
using NSubstitute;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.GraphQL.Tests;

public class AbpGraphQLAppServiceSchemeConfigurationsTests
{
    [Fact]
    public void Configure_With_Assembly_Should_Scan_IEmptyAppService4()
    {
        // arrange
        var assembly = Substitute.For<Assembly>();
        assembly.DefinedTypes.Returns([typeof(IEmptyAppService4).GetTypeInfo()]);

        var config = new AbpGraphQLAppServiceSchemeConfigurations();

        // act
        config.Configure(assembly);

        // assert
        config.GetConfigurations().Count.ShouldBePositive();
    }

    [Fact]
    public void Configure_With_Assembly_Should_Scan_IEmptyAppService3()
    {
        // arrange
        var assembly = Substitute.For<Assembly>();
        assembly.DefinedTypes.Returns([typeof(IEmptyAppService3).GetTypeInfo()]);

        var config = new AbpGraphQLAppServiceSchemeConfigurations();

        // act
        config.Configure(assembly);

        // assert
        config.GetConfigurations().Count.ShouldBePositive();
    }

    [Fact]
    public void Configure_With_Assembly_Should_Scan_IEmptyAppService2()
    {
        // arrange
        var assembly = Substitute.For<Assembly>();
        assembly.DefinedTypes.Returns([typeof(IEmptyAppService2).GetTypeInfo()]);

        var config = new AbpGraphQLAppServiceSchemeConfigurations();

        // act
        config.Configure(assembly);

        // assert
        config.GetConfigurations().Count.ShouldBePositive();
    }
}
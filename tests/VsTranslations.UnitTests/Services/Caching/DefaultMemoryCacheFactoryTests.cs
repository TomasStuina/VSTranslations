using AutoFixture;
using FluentAssertions;
using VSTranslations.Services.Caching;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using Xunit;

namespace VSTranslations.UnitTests.Services.Caching;

public class DefaultMemoryCacheFactoryTests
{
    [Theory]
    [DefaultAutoData]
    public void Create_WhenInvoked_ShouldReturnDefaultMemoryCacheInstance(IFixture fixture)
    {
        // Arrange
        var memoryCacheFactory = fixture.Create<DefaultMemoryCacheFactory>();

        // Act & Assert
        memoryCacheFactory.Create().Should().BeOfType<DefaultMemoryCache>();
    }
}

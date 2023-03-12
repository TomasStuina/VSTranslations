using AutoFixture.Xunit2;
using Moq;
using VSTranslations.Plugin.Abstractions.Caching;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.Plugin.Abstractions.Extensions;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using Xunit;
using FluentAssertions;

namespace VSTranslations.Plugin.Abstractions.UnitTests.Extensions;

public class CacheExtensionsTests
{
    [Theory]
    [DefaultAutoData]
    public async Task GetOrSetTranslationAsync_WhenSourceAndTargetProvided_ShouldCacheTranslation(
    [Frozen] Mock<ICache> cache, Language source, Language target, string sourceText, string targetText)
    {
        // Arrange
        var key = $"{source.Code}{target.Code}+{sourceText}";
        var targetValueFactory = () => Task.FromResult(targetText);

        cache
            .Setup(self => self.GetOrCreateAsync(key, targetValueFactory))
            .Returns((string _, Func<Task<string>> factory) => factory());

        // Act
        var cachedValue = await cache.Object.GetOrSetTranslationAsync(source, sourceText, target, targetValueFactory);

        // Assert
        cachedValue.Should().Be(targetText);
    }
}

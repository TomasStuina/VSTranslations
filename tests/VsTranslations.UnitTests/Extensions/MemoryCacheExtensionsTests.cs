using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.Sdk.TestFramework;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Threading;
using Moq;
using System.Threading.Tasks;
using VSTranslations.Extensions;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.UnitTests.Xunit;
using Xunit;

namespace VSTranslations.UnitTests.Extensions;

public class MemoryCacheExtensionsTests : VsTestBase
{
    public MemoryCacheExtensionsTests(GlobalServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    [Theory]
    [DefaultAutoData]
    public async Task GetOrCreateLazyAsync_WhenKeyAndValueFactoryProvided_ShouldInvokeReturnCachedLazyValue(
        [Frozen] Mock<IMemoryCache> memoryCache, string key, string value)
    {
        // Arrange
        var valueFactory = () => Task.FromResult(value);
        object lazyValue = new AsyncLazy<string>(valueFactory, ThreadHelper.JoinableTaskFactory);
        memoryCache.Setup(self => self.TryGetValue(key, out lazyValue)).Returns(true);

        // Act
        var cachedValue = await memoryCache.Object.GetOrCreateLazyAsync(key, valueFactory);

        // Assert
        cachedValue.Should().Be(value);
    }
}

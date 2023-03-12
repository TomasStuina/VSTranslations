using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.Sdk.TestFramework;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using Moq;
using System.Threading.Tasks;
using VSTranslations.Options;
using VSTranslations.Services.Caching;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.UnitTests.Xunit;
using Xunit;

namespace VSTranslations.UnitTests.Services.Caching;

public class DefaultMemoryCacheTests : VsTestBase
{
    public DefaultMemoryCacheTests(GlobalServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    [Theory]
    [DefaultMemoryCacheAutoData]
    public async Task GetOrCreateAsync_WhenCacheEnabled_ShouldReturnCachedLazyValue(
        IFixture fixture, IVsSettingsManager settingsManager, Mock<IMemoryCache> internalCache, string key, string value)
    {
        // Arrange
        ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);
        var options = await GeneralOptions.GetLiveInstanceAsync();
        options.EnableCaching = true;

        var valueFactory = () => Task.FromResult(value);
        object lazyValue = new AsyncLazy<string>(valueFactory, ThreadHelper.JoinableTaskFactory);
        internalCache.Setup(self => self.TryGetValue(key, out lazyValue)).Returns(true);

        var memoryCache = fixture.Create<DefaultMemoryCache>();

        // Act
        var cachedValue = await memoryCache.GetOrCreateAsync(key, valueFactory);

        // Assert
        cachedValue.Should().Be(value);
        internalCache.Verify(self => self.TryGetValue(key, out lazyValue), Times.Once);
    }

    [Theory]
    [DefaultMemoryCacheAutoData]
    public async Task GetOrCreateAsync_WhenCacheDisabled_ShouldReturnDirectValue(
        IFixture fixture, IVsSettingsManager settingsManager, Mock<IMemoryCache> internalCache, string key, string value)
    {
        // Arrange
        ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);
        var options = await GeneralOptions.GetLiveInstanceAsync();
        options.EnableCaching = false;

        var valueFactory = () => Task.FromResult(value);
        object lazyValue = new AsyncLazy<string>(valueFactory, ThreadHelper.JoinableTaskFactory);
        internalCache.Setup(self => self.TryGetValue(key, out lazyValue)).Returns(true);

        var memoryCache = fixture.Create<DefaultMemoryCache>();

        // Act
        var cachedValue = await memoryCache.GetOrCreateAsync(key, valueFactory);

        // Assert
        cachedValue.Should().Be(value);
        internalCache.Verify(self => self.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny), Times.Never);
    }
}

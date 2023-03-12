using AutoFixture;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using VSTranslations.Services.Caching;

namespace VSTranslations.UnitTests.AutoFixture.Customizations;

internal class DefaultMemoryCacheCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Freeze<Mock<IMemoryCache>>();
        fixture.Register((IMemoryCache cache) => new DefaultMemoryCache(cache));
    }
}

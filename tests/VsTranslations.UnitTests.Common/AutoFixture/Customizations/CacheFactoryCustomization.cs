using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using VSTranslations.Plugin.Abstractions.Caching;

namespace VSTranslations.UnitTests.Common.AutoFixture.Customizations;

public class CacheFactoryCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var cacheFactory = fixture.Freeze<Mock<ICacheFactory>>();
        cacheFactory
            .Setup(self => self.Create())
            .ReturnsUsingFixture(fixture);
    }
}

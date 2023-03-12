using AutoFixture;
using Moq;
using System.Threading.Tasks;
using System;
using VSTranslations.Plugin.Abstractions.Caching;

namespace VSTranslations.UnitTests.Common.AutoFixture.Customizations;

public class CacheCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var cache = fixture.Freeze<Mock<ICache>>();
        cache
            .Setup(self => self.GetOrCreateAsync(It.IsNotNull<string>(), It.IsNotNull<Func<Task<string>>>()))
            .Returns((string _, Func<Task<string>> factory) => factory());
    }
}

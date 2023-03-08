using AutoFixture;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSTranslations.Plugin.Abstractions.Translating;

namespace VSTranslations.UnitTests.AutoFixture.Customizations;

public class LanguageCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register<IEnumerable<Language>, IReadOnlyList<Language>>(languages => languages.ToList());
        fixture.Freeze<Language>();
        fixture.Register((Language language) => new ValueTask<Language>(language));
    }
}

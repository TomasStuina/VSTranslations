using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using VSTranslations.UnitTests.Common.AutoFixture.Attributes;

namespace VSTranslations.UnitTests.AutoFixture.Attributes;

public class AutoDataAttributeBase : AutoDataAttribute
{
    protected AutoDataAttributeBase(params ICustomization[] customizations)
        : this(AutoDataConfiguration.None, customizations)
    {

    }

    protected AutoDataAttributeBase(AutoDataConfiguration configuration, params ICustomization[] customizations)
        : base(() => CreateFixture(configuration, customizations))
    {
        
    }

    private static IFixture CreateFixture(AutoDataConfiguration configuration, IEnumerable<ICustomization> customizations)
    {
        var fixture = new Fixture
        {
            OmitAutoProperties = configuration.HasFlag(AutoDataConfiguration.OmmitAutoProperties)
        };

        fixture.Customize(new AutoMoqCustomization
        {
            ConfigureMembers = configuration.HasFlag(AutoDataConfiguration.AutoConfigureMocks)
        });

        foreach(var customization in customizations)
        {
            fixture.Customize(customization);
        }

        return fixture;
    }
}

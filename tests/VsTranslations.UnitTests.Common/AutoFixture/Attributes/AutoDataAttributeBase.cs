using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using System.Collections.Generic;

namespace VSTranslations.UnitTests.AutoFixture.Attributes;

public class AutoDataAttributeBase : AutoDataAttribute
{
    protected AutoDataAttributeBase(params ICustomization[] customizations)
        : this(false, customizations)
    {

    }

    protected AutoDataAttributeBase(bool ommitAutoProperties, params ICustomization[] customizations)
        : base(() => CreateFixture(ommitAutoProperties, customizations))
    {
        
    }

    private static IFixture CreateFixture(bool ommitAutoProperties, IEnumerable<ICustomization> customizations)
    {
        var fixture = new Fixture
        {
            OmitAutoProperties = ommitAutoProperties
        };

        fixture.Customize(new AutoMoqCustomization());

        foreach(var customization in customizations)
        {
            fixture.Customize(customization);
        }

        return fixture;
    }
}

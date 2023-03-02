using AutoFixture;
using System;
using VSTranslations.UnitTests.Common.AutoFixture.Attributes;

namespace VSTranslations.UnitTests.AutoFixture.Attributes;

public class DefaultAutoDataAttribute : AutoDataAttributeBase
{
    public DefaultAutoDataAttribute() : this(AutoDataConfiguration.None)
    {
    }

    public DefaultAutoDataAttribute(AutoDataConfiguration configuration) : base(configuration, Array.Empty<ICustomization>())
    {
    }
}

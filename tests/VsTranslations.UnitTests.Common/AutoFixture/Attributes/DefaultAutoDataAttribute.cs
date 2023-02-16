using AutoFixture;
using System;

namespace VSTranslations.UnitTests.AutoFixture.Attributes;

public class DefaultAutoDataAttribute : AutoDataAttributeBase
{
    public DefaultAutoDataAttribute() : this(false)
    {
    }

    public DefaultAutoDataAttribute(bool ommitAutoProperties) : base(ommitAutoProperties, Array.Empty<ICustomization>())
    {
    }
}

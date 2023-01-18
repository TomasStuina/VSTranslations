using AutoFixture;
using System;

namespace VsTranslations.UnitTests.AutoFixture.Attributes;

public class DefaultAutoDataAttribute : AutoDataAttributeBase
{
    public DefaultAutoDataAttribute() : this(false)
    {
    }

    public DefaultAutoDataAttribute(bool ommitAutoProperties) : base(ommitAutoProperties, Array.Empty<ICustomization>())
    {
    }
}

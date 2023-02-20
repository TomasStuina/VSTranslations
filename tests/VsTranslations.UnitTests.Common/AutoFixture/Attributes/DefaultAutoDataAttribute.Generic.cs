using AutoFixture;

namespace VSTranslations.UnitTests.AutoFixture.Attributes;

public class DefaultAutoDataAttribute<T> : AutoDataAttributeBase where T : ICustomization, new()
{
    public DefaultAutoDataAttribute() : this(false)
    {
    }

    public DefaultAutoDataAttribute(bool ommitAutoProperties) : base(ommitAutoProperties, new T())
    {
    }
}

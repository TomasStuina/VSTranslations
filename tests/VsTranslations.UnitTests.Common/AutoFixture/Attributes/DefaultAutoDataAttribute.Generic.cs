using AutoFixture;
using VSTranslations.UnitTests.Common.AutoFixture.Attributes;

namespace VSTranslations.UnitTests.AutoFixture.Attributes;

public class DefaultAutoDataAttribute<T> : AutoDataAttributeBase where T : ICustomization, new()
{
    public DefaultAutoDataAttribute() : this(AutoDataConfiguration.None)
    {
    }

    public DefaultAutoDataAttribute(AutoDataConfiguration configuration) : base(configuration, new T())
    {
    }
}

using System;

namespace VSTranslations.UnitTests.Common.AutoFixture.Attributes;

[Flags]
public enum AutoDataConfiguration
{
    None = 0,
    OmmitAutoProperties,
    AutoConfigureMocks
}

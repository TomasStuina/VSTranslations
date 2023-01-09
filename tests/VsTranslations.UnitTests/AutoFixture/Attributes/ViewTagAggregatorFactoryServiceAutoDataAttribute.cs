using VsTranslations.UnitTests.AutoFixture.Customizations;

namespace VsTranslations.UnitTests.AutoFixture.Attributes
{
    internal class ViewTagAggregatorFactoryServiceAutoDataAttribute : AutoDataAttributeBase
    {
        public ViewTagAggregatorFactoryServiceAutoDataAttribute()
            : base(new TextViewCustomization(), new ViewTagAggregatorFactoryServiceCustomization())
        {
        }
    }
}

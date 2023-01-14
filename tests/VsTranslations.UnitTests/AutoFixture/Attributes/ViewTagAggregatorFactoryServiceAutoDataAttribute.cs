using VsTranslations.UnitTests.AutoFixture.Customizations;

namespace VsTranslations.UnitTests.AutoFixture.Attributes
{
    public class ViewTagAggregatorFactoryServiceAutoDataAttribute : AutoDataAttributeBase
    {
        public ViewTagAggregatorFactoryServiceAutoDataAttribute()
            : base(new TextViewCustomization(), new ViewTagAggregatorFactoryServiceCustomization())
        {
        }
    }
}

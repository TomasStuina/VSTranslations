using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
{
    public class ViewTagAggregatorFactoryServiceAutoDataAttribute : AutoDataAttributeBase
    {
        public ViewTagAggregatorFactoryServiceAutoDataAttribute() : base(
            new TextViewCustomization(),
            new ViewTagAggregatorFactoryServiceCustomization())
        {
        }
    }
}

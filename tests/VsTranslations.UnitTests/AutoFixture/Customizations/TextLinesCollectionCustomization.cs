using AutoFixture;
using System.Collections.Generic;
using VSTranslations.Abstractions.Translating;

namespace VsTranslations.UnitTests.AutoFixture.Customizations
{
    public class TextLinesCollectionCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register((IEnumerable<TextLine> textLines) =>
            {
                var textLinesCollection = new TextLinesCollection();

                foreach(var textLine in textLines)
                {
                    textLinesCollection.Add(textLine);
                }

                return textLinesCollection;
            });
        }
    }
}

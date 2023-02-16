using FluentAssertions;
using System;
using System.Windows;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Extensions;
using Xunit;

namespace VSTranslations.UnitTests.Extensions
{
    public class FontSizeConverterExtensionsTests
    {
        [Theory]
        [DefaultAutoData]
        public void ConvertFromPointSize_WhenFontSizeConverterNull_ShouldThrowArgumentNullException(uint pointSize)
        {
            // Act & Assert
            FluentActions.Invoking(() => FontSizeConverterExtensions.ConvertFromPointSize(null, pointSize))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [DefaultAutoData]
        public void ConvertFromPointSize_WhenPointSizePassed_ShouldReturnWpfPointSize(uint pointSize)
        {
            // Arrange
            var fontConverter = new FontSizeConverter();

            // Act & Assert
            fontConverter.ConvertFromPointSize(pointSize)
                .Should().BeApproximately((double)fontConverter.ConvertFrom($"{pointSize}pt"), precision: double.Epsilon);
        }
    }
}

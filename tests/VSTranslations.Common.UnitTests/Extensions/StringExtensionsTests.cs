using FluentAssertions;
using VsTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Common.Extensions;

namespace VsTranslations.Common.UnitTests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SplitToLines_WhenInputStringIsNullOrEmpty_ShouldReturnEmptyEnumerable(string input)
        {
            // Act & Assert
            StringExtensions.SplitToLines(input).Should().BeEmpty();
        }

        [Theory]
        [DefaultAutoData]
        public void SplitToLines_WhenInputStringHasSingleLine_ShouldReturnSingleElement(string singlelineInput)
        {
            // Act
            var lines = singlelineInput.SplitToLines();

            // Assert
            lines.Should().ContainSingle().Which.Should().Be(singlelineInput);
        }

        [Theory]
        [DefaultAutoData]
        public void SplitToLines_WhenInputStringHasMultipleLines_ShouldReturnMultipleElements(string[] multilineInputArray)
        {
            // Arrange
            var multilineInput = string.Join(Environment.NewLine, multilineInputArray);

            // Act
            var lines = multilineInput.SplitToLines();

            // Assert
            lines.Should().BeEquivalentTo(multilineInputArray);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void IsOpening_WhenInputStringIsNullOrEmpty_ShouldReturnFalse(string input)
        {
            // Act & Assert
            StringExtensions.IsOpening(input).Should().BeFalse();
        }

        [Fact]
        public void IsOpening_WhenInputStringIsMoreThanOneChar_ShouldReturnFalse()
        {
            // Arrange
            var input = "{{";

            // Act & Assert
            input.IsOpening().Should().BeFalse();
        }

        [Fact]
        public void IsOpening_WhenInputStringIsNotOpeningBracket_ShouldReturnFalse()
        {
            // Arrange
            var input = "[";

            // Act & Assert
            input.IsOpening().Should().BeFalse();
        }

        [Fact]
        public void IsOpening_WhenInputStringIsOpeningBracket_ShouldReturnTrue()
        {
            // Arrange
            var input = "{";

            // Act & Assert
            input.IsOpening().Should().BeTrue();
        }
    }
}

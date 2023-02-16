using FluentAssertions;
using Microsoft.VisualStudio.Text;
using System;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Glyphs;
using Xunit;

namespace VSTranslations.UnitTests.Glyphs
{
    public class TranslatedLineGlyphTagComparerTests
    {
        [Fact]
        public void Instance_WhenInvoked_ShouldReturnNotNullInstance()
        {
            // Act & Assert
            TranslatedLineGlyphTagComparer.Instance
                .Should().NotBeNull();
        }

        [Fact]
        public void Instance_WhenInvokedMoreThanOnce_ShouldReturnSameInstance()
        {
            // Act & Assert
            TranslatedLineGlyphTagComparer.Instance
                .Should().BeSameAs(TranslatedLineGlyphTagComparer.Instance);
        }

        [Fact]
        public void Equals_WhenBothNull_ShouldReturnTrue()
        {
            // Act & Assert
            TranslatedLineGlyphTagComparer.Instance.Equals(null, null)
                .Should().BeTrue();
        }

        [Theory]
        [SnapshotSpanAutoData]
        public void Equals_WhenFirstInstanceNull_ShouldReturnFalse(Tuple<SnapshotSpan, PositionAffinity?, TranslatedLineGlyphTag> tagData)
        {
            // Act & Assert
            TranslatedLineGlyphTagComparer.Instance.Equals(null, tagData)
                .Should().BeFalse();
        }

        [Theory]
        [SnapshotSpanAutoData]
        public void Equals_WhenSecondInstanceNull_ShouldReturnFalse(Tuple<SnapshotSpan, PositionAffinity?, TranslatedLineGlyphTag> tagData)
        {
            // Act & Assert
            TranslatedLineGlyphTagComparer.Instance.Equals(tagData, null)
                .Should().BeFalse();
        }

        [Theory]
        [SnapshotSpanAutoData]
        public void Equals_WhenBothSpansAreEqual_ShouldReturnTrue(Tuple<SnapshotSpan, PositionAffinity?, TranslatedLineGlyphTag> tagData)
        {
            // Act & Assert
            TranslatedLineGlyphTagComparer.Instance.Equals(tagData, tagData)
                .Should().BeTrue();
        }

        [Theory]
        [SnapshotSpanAutoData]
        public void Equals_WhenBothSpansAreNotEqual_ShouldReturnFalse(ITextSnapshot textSnapshot,
            PositionAffinity? positionAffinity, TranslatedLineGlyphTag glyphTag)
        {
            // Arrange
            var tagData1 = Tuple.Create(new SnapshotSpan(textSnapshot, 0, 10), positionAffinity, glyphTag);
            var tagData2 = Tuple.Create(new SnapshotSpan(textSnapshot, 0, 20), positionAffinity, glyphTag);

            // Act & Assert
            TranslatedLineGlyphTagComparer.Instance.Equals(tagData1, tagData2)
                .Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_WhenTagDataNull_ShouldReturnZero()
        {
            // Act & Assert
            TranslatedLineGlyphTagComparer.Instance.GetHashCode(null)
                .Should().Be(0);
        }

        [Theory]
        [SnapshotSpanAutoData]
        public void GetHashCode_WhenTagDataNotNull_ShouldReturnHashCodeOfSpan(Tuple<SnapshotSpan, PositionAffinity?, TranslatedLineGlyphTag> tagData)
        {
            // Act & Assert
            TranslatedLineGlyphTagComparer.Instance.GetHashCode(tagData)
                .Should().Be(tagData.Item1.GetHashCode());
        }
    }
}

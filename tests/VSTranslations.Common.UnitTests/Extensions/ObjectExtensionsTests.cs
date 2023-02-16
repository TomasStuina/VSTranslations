using FluentAssertions;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Common.Extensions;

namespace VSTranslations.Common.UnitTests.Extensions
{
    public class ObjectExtensionsTests
    {
        [Theory]
        [DefaultAutoData]
        public void ThrowIfNull_WhenInstanceIsNull_ShouldThrowArgumentNullException(string paramName)
        {
            // Act & Assert
            FluentActions.Invoking(() => ObjectExtensions.ThrowIfNull<string?>(null, paramName))
                .Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be(paramName);
        }

        [Theory]
        [DefaultAutoData]
        public void ThrowIfNull_WhenInstanceIsNotNull_ShouldReturnSameInstance(string instance, string paramName)
        {
            // Act & Assert
            instance.ThrowIfNull(paramName).Should().BeSameAs(instance);
        }
    }
}

using FluentAssertions;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Common.Extensions;

namespace VSTranslations.Common.UnitTests.Extensions
{
    public class DictionaryExtensionsTests
    {
        [Theory]
        [DefaultAutoData]
        public void RemoveAll_WhenDictionaryNull_ShouldThrowArgumentNullException(string[] keys)
        {
            // Act & Assert
            FluentActions.Invoking(() => DictionaryExtensions.RemoveAll<string, string>(null, keys))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [DefaultAutoData]
        public void RemoveAll_WhenKeysNull_ShouldThrowArgumentNullException(Dictionary<string, string> dictionary)
        {
            // Act & Assert
            FluentActions.Invoking(() => dictionary.RemoveAll(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [DefaultAutoData]
        public void RemoveAll_WhenKeysMatchesValues_ShouldRemoveValues(Dictionary<string, string> dictionary)
        {
            // Act
            dictionary.RemoveAll(dictionary.Keys);

            // Assert
            dictionary.Should().BeEmpty();
        }

        [Theory]
        [DefaultAutoData]
        public void RemoveAll_WhenKeysDoNotMatchValues_ShouldNotRemoveAnything(Dictionary<string, string> dictionary,
            string[] nonMatchingKeys)
        {
            // Arrange
            var keys = dictionary.Keys.ToArray();

            // Act
            dictionary.RemoveAll(nonMatchingKeys);

            // Assert
            dictionary.Should().ContainKeys(keys);
        }
    }
}

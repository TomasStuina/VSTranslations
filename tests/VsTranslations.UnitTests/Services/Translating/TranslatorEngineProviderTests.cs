using AutoFixture;
using System.Collections.Generic;
using System;
using VsTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.Services.Translating;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace VsTranslations.UnitTests.Services.Translating
{
    public class TranslatorEngineProviderTests
    {
        [Theory]
        [DefaultAutoData]
        public void GetAllMetadataEntries_WhenEnginesInjected_ShouldReturnAllEnginesMetadataEntries(IFixture fixture,
            IEnumerable<Lazy<ITranslatorEngine, ITranslatorEngineMetadata>> engines)
        {
            // Arrange
            var engineProvider = fixture.Create<TranslatorEngineProvider>();
            engineProvider.TranslatorEngines = engines;

            // Act
            var metadataEntires = engineProvider.GetAllMetadataEntries();

            // Assert
            metadataEntires.Should().BeEquivalentTo(engines.Select(engine => engine.Metadata));
        }

        [Theory]
        [DefaultAutoData]
        public void GetAllMetadataEntries_WhenEnginesNotInjected_ShouldReturnEmpty(IFixture fixture)
        {
            // Arrange
            var engineProvider = fixture.Create<TranslatorEngineProvider>();

            // Act
            var metadataEntires = engineProvider.GetAllMetadataEntries();

            // Assert
            metadataEntires.Should().BeEmpty();
        }
    }
}

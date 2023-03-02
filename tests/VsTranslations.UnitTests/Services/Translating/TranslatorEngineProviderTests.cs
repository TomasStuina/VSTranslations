using AutoFixture;
using System.Collections.Generic;
using System;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.Services.Translating;
using Xunit;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using VSTranslations.Options;
using VSTranslations.UnitTests.Xunit;
using Microsoft.VisualStudio.Sdk.TestFramework;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;

namespace VSTranslations.UnitTests.Services.Translating
{
    public class TranslatorEngineProviderTests : VsTestBase
    {
        public TranslatorEngineProviderTests(GlobalServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [Theory]
        [DefaultAutoData]
        public void TranslatorEngines_WhenNoEnginesSet_ShouldReturnEmpty(IFixture fixture)
        {
            // Arrange
            var engineProvider = fixture.Create<TranslatorEngineProvider>();

            // Act & Assert
            engineProvider.TranslatorEngines.Should().BeEmpty();
        }

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

        [Theory]
        [TranslatorEngineProviderAutoData]
        public async Task GetAsync_WhenEngineSelectedInOptions_ShouldReturnIt(IFixture fixture,
            IEnumerable<Lazy<ITranslatorEngine, ITranslatorEngineMetadata>> engines, IVsSettingsManager settingsManager)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);

            var engineProvider = fixture.Create<TranslatorEngineProvider>();
            engineProvider.TranslatorEngines = engines;
            var expectedEngineDescriptor = engines.Last();

            var options = await GeneralOptions.GetLiveInstanceAsync();
            options.SelectedPluginId = expectedEngineDescriptor.Metadata.Id;

            // Act
            var actualEngine = await engineProvider.GetAsync();

            // Assert
            actualEngine.Should().BeSameAs(expectedEngineDescriptor.Value);
        }

        [Theory]
        [TranslatorEngineProviderAutoData]
        public async Task GetAsync_WhenEngineNotSelected_ShouldReturnFirst(IFixture fixture,
            IEnumerable<Lazy<ITranslatorEngine, ITranslatorEngineMetadata>> engines, IVsSettingsManager settingsManager)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);

            var engineProvider = fixture.Create<TranslatorEngineProvider>();
            engineProvider.TranslatorEngines = engines;

            var options = await GeneralOptions.GetLiveInstanceAsync();
            options.SelectedPluginId = null;

            // Act
            var actualEngine = await engineProvider.GetAsync();

            // Assert
            actualEngine.Should().BeSameAs(engines.First().Value);
        }

        [Theory]
        [TranslatorEngineProviderAutoData]
        public async Task GetAsync_WhenSelectedEngineNotInstalled_ShouldReturnFirst(IFixture fixture,
            IEnumerable<Lazy<ITranslatorEngine, ITranslatorEngineMetadata>> engines, 
            Lazy<ITranslatorEngine, ITranslatorEngineMetadata> unknownEngine, IVsSettingsManager settingsManager)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);

            var engineProvider = fixture.Create<TranslatorEngineProvider>();
            engineProvider.TranslatorEngines = engines;

            var options = await GeneralOptions.GetLiveInstanceAsync();
            options.SelectedPluginId = unknownEngine.Metadata.Id;

            // Act
            var actualEngine = await engineProvider.GetAsync();

            // Assert
            actualEngine.Should().BeSameAs(engines.First().Value);
        }

        [Theory]
        [TranslatorEngineProviderAutoData]
        public async Task GetAsync_WhenEngineNotSelectedAndNoEnginesInstalled_ShouldReturnNull(IFixture fixture,
            IVsSettingsManager settingsManager)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);

            var engineProvider = fixture.Create<TranslatorEngineProvider>();

            var options = await GeneralOptions.GetLiveInstanceAsync();
            options.SelectedPluginId = null;

            // Act
            var actualEngine = await engineProvider.GetAsync();

            // Assert
            actualEngine.Should().BeNull();
        }

        [Theory]
        [TranslatorEngineProviderAutoData]
        public async Task GetAsync_WhenSelectedEngineNotInstalledAndNoEnginesInstalled_ShouldReturnNull(IFixture fixture,
            Lazy<ITranslatorEngine, ITranslatorEngineMetadata> unknownEngine, IVsSettingsManager settingsManager)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);

            var engineProvider = fixture.Create<TranslatorEngineProvider>();

            var options = await GeneralOptions.GetLiveInstanceAsync();
            options.SelectedPluginId = unknownEngine.Metadata.Id;

            // Act
            var actualEngine = await engineProvider.GetAsync();

            // Assert
            actualEngine.Should().BeNull();
        }

        [Theory]
        [TranslatorEngineProviderAutoData]
        public async Task GetWithMetadataAsync_WhenEngineSelectedInOptions_ShouldReturnItWithMetadata(IFixture fixture,
            IEnumerable<Lazy<ITranslatorEngine, ITranslatorEngineMetadata>> engines, IVsSettingsManager settingsManager)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);

            var engineProvider = fixture.Create<TranslatorEngineProvider>();
            engineProvider.TranslatorEngines = engines;
            var expectedEngineDescriptor = engines.Last();

            var options = await GeneralOptions.GetLiveInstanceAsync();
            options.SelectedPluginId = expectedEngineDescriptor.Metadata.Id;

            // Act
            var (actualEngine, metadata) = await engineProvider.GetWithMetadataAsync();

            // Assert
            actualEngine.Should().BeSameAs(expectedEngineDescriptor.Value);
            metadata.Should().BeSameAs(expectedEngineDescriptor.Metadata);
        }

        [Theory]
        [TranslatorEngineProviderAutoData]
        public async Task GetWithMetadataAsync_WhenEngineNotSelected_ShouldReturnFirstWithMetaData(IFixture fixture,
            IEnumerable<Lazy<ITranslatorEngine, ITranslatorEngineMetadata>> engines, IVsSettingsManager settingsManager)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);

            var engineProvider = fixture.Create<TranslatorEngineProvider>();
            engineProvider.TranslatorEngines = engines;

            var options = await GeneralOptions.GetLiveInstanceAsync();
            options.SelectedPluginId = null;

            // Act
            var (actualEngine, metadata) = await engineProvider.GetWithMetadataAsync();

            // Assert
            var firstEngine = engines.First();
            actualEngine.Should().BeSameAs(firstEngine.Value);
            metadata.Should().BeSameAs(firstEngine.Metadata);
        }

        [Theory]
        [TranslatorEngineProviderAutoData]
        public async Task GetWithMetadataAsync_WhenSelectedEngineNotInstalled_ShouldReturnFirstWithMetaData(IFixture fixture,
            IEnumerable<Lazy<ITranslatorEngine, ITranslatorEngineMetadata>> engines,
            Lazy<ITranslatorEngine, ITranslatorEngineMetadata> unknownEngine, IVsSettingsManager settingsManager)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);

            var engineProvider = fixture.Create<TranslatorEngineProvider>();
            engineProvider.TranslatorEngines = engines;

            var options = await GeneralOptions.GetLiveInstanceAsync();
            options.SelectedPluginId = unknownEngine.Metadata.Id;

            // Act
            var (actualEngine, metadata) = await engineProvider.GetWithMetadataAsync();

            // Assert
            var firstEngine = engines.First();
            actualEngine.Should().BeSameAs(firstEngine.Value);
            metadata.Should().BeSameAs(firstEngine.Metadata);
        }

        [Theory]
        [TranslatorEngineProviderAutoData]
        public async Task GetWithMetadataAsync_WhenEngineNotSelectedAndNoEnginesInstalled_ShouldReturnNull(IFixture fixture,
            IVsSettingsManager settingsManager)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);

            var engineProvider = fixture.Create<TranslatorEngineProvider>();

            var options = await GeneralOptions.GetLiveInstanceAsync();
            options.SelectedPluginId = null;

            // Act
            var (actualEngine, metadata) = await engineProvider.GetWithMetadataAsync();

            // Assert
            actualEngine.Should().BeNull();
            metadata.Should().BeNull();
        }

        [Theory]
        [TranslatorEngineProviderAutoData]
        public async Task GetWithMetadataAsync_WhenSelectedEngineNotInstalledAndNoEnginesInstalled_ShouldReturnNull(IFixture fixture,
            Lazy<ITranslatorEngine, ITranslatorEngineMetadata> unknownEngine, IVsSettingsManager settingsManager)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);

            var engineProvider = fixture.Create<TranslatorEngineProvider>();

            var options = await GeneralOptions.GetLiveInstanceAsync();
            options.SelectedPluginId = unknownEngine.Metadata.Id;

            // Act
            var (actualEngine, metadata) = await engineProvider.GetWithMetadataAsync();

            // Assert
            actualEngine.Should().BeNull();
            metadata.Should().BeNull();
        }
    }
}

using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.VisualStudio.Sdk.TestFramework;
using Microsoft.VisualStudio.Shell.Interop;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using VSTranslations.Options;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.UnitTests.Common.AutoFixture.Attributes;
using VSTranslations.UnitTests.Xunit;
using Xunit;

namespace VSTranslations.UnitTests.Options
{
    public class GeneralOptionsViewModelTests : VsTestBase
    {
        public GeneralOptionsViewModelTests(GlobalServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [Theory]
        [TranslatorEngineProviderAutoData(AutoDataConfiguration.OmmitAutoProperties | AutoDataConfiguration.AutoConfigureMocks)]
        public async Task Ctor_WhenPluginIsNotSelected_ShouldSelectFirstPlugin(IFixture fixture, IVsSettingsManager settingsManager,
            [Frozen] IEnumerable<ITranslatorEngineMetadata> translatorEngineMetadata)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);
            var options = await GeneralOptions.GetLiveInstanceAsync();
            options.SelectedPluginId = null;

            // Act
            fixture.Create<GeneralOptionsViewModel>();

            // Assert
            options.SelectedPluginId.Should().Be(translatorEngineMetadata.First().Id);
        }

        [Theory]
        [TranslatorEngineProviderAutoData(AutoDataConfiguration.OmmitAutoProperties | AutoDataConfiguration.AutoConfigureMocks)]
        public async Task Ctor_WhenPluginIsNotSelectedAndNoPlugins_ShouldSetNull (IFixture fixture, IVsSettingsManager settingsManager)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);
            fixture.Inject(Enumerable.Empty<ITranslatorEngineMetadata>());

            var options = await GeneralOptions.GetLiveInstanceAsync();
            options.SelectedPluginId = null;

            // Act
            fixture.Create<GeneralOptionsViewModel>();

            // Assert
            options.SelectedPluginId.Should().BeNull();
        }


        [Theory]
        [TranslatorEngineProviderAutoData(AutoDataConfiguration.OmmitAutoProperties | AutoDataConfiguration.AutoConfigureMocks)]
        public async Task Ctor_WhenPluginIsSelected_ShouldKeepSelectedId(IFixture fixture, IVsSettingsManager settingsManager,
            [Frozen] IEnumerable<ITranslatorEngineMetadata> translatorEngineMetadata)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);
            var options = await GeneralOptions.GetLiveInstanceAsync();
            var selectedPluginId = translatorEngineMetadata.First().Id;
            options.SelectedPluginId = selectedPluginId;

            // Act
            fixture.Create<GeneralOptionsViewModel>();

            // Assert
            options.SelectedPluginId.Should().Be(selectedPluginId);
        }

        [Theory]
        [TranslatorEngineProviderAutoData(AutoDataConfiguration.OmmitAutoProperties | AutoDataConfiguration.AutoConfigureMocks)]
        public async Task Ctor_WhenSelectedPluginDoesNotExist_ShouldSelectFirstPlugin(IFixture fixture, IVsSettingsManager settingsManager,
            [Frozen] IEnumerable<ITranslatorEngineMetadata> translatorEngineMetadata, string selectedPluginId)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);
            var options = await GeneralOptions.GetLiveInstanceAsync();
            options.SelectedPluginId = selectedPluginId;

            // Act
            fixture.Create<GeneralOptionsViewModel>();

            // Assert
            options.SelectedPluginId.Should().Be(translatorEngineMetadata.First().Id);
        }

        [Theory]
        [TranslatorEngineProviderAutoData(AutoDataConfiguration.OmmitAutoProperties | AutoDataConfiguration.AutoConfigureMocks)]
        public void InstalledPlugins_WheInvoked_ShouldReturnInstalledPlugins(IFixture fixture, IVsSettingsManager settingsManager,
            [Frozen] IEnumerable<ITranslatorEngineMetadata> translatorEngineMetadata)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);
            var viewModel = fixture.Create<GeneralOptionsViewModel>();

            // Act & Assert
            viewModel.InstalledPlugins.Should().BeEquivalentTo(translatorEngineMetadata);
        }

        [Theory]
        [TranslatorEngineProviderAutoData(AutoDataConfiguration.OmmitAutoProperties | AutoDataConfiguration.AutoConfigureMocks)]
        public void HasPluginsInstalled_WhenAtLeastOnePluginInstalled_ShouldReturnTrue(IFixture fixture, IVsSettingsManager settingsManager,
            ITranslatorEngineMetadata translatorEngineMetadata)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);
            fixture.Inject<IEnumerable<ITranslatorEngineMetadata>>(new[] { translatorEngineMetadata });
            var viewModel = fixture.Create<GeneralOptionsViewModel>();

            // Act & Assert
            viewModel.HasPluginsInstalled.Should().BeTrue();
        }

        [Theory]
        [TranslatorEngineProviderAutoData(AutoDataConfiguration.OmmitAutoProperties | AutoDataConfiguration.AutoConfigureMocks)]
        public void HasPluginsInstalled_WhenNoPluginsInstalled_ShouldReturnFalse(IFixture fixture, IVsSettingsManager settingsManager)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);
            fixture.Inject(Enumerable.Empty<ITranslatorEngineMetadata>());
            var viewModel = fixture.Create<GeneralOptionsViewModel>();

            // Act & Assert
            viewModel.HasPluginsInstalled.Should().BeFalse();
        }

        [Theory]
        [TranslatorEngineProviderAutoData(AutoDataConfiguration.OmmitAutoProperties | AutoDataConfiguration.AutoConfigureMocks)]
        public async Task SelectedPluginId_WhenPluginIdSet_ShouldSaveOptions(IFixture fixture, IVsSettingsManager settingsManager,
            Mock<Action<GeneralOptions>> savedHandler, string selectedPluginId)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);
            var viewModel = fixture.Create<GeneralOptionsViewModel>();
            var options = await GeneralOptions.GetLiveInstanceAsync();

            // Act
            try
            {
                GeneralOptions.Saved += savedHandler.Object;
                viewModel.SelectedPluginId = selectedPluginId;
            }
            finally
            {
                GeneralOptions.Saved -= savedHandler.Object;
            }

            // Assert
            savedHandler.Verify(self => self(options), Times.Once);
        }

        [Theory]
        [TranslatorEngineProviderAutoData(AutoDataConfiguration.OmmitAutoProperties | AutoDataConfiguration.AutoConfigureMocks)]
        public void SelectedPluginId_WhenPluginIdSet_ShouldNotifyPropertyChanged(IFixture fixture, IVsSettingsManager settingsManager,
            Mock<PropertyChangedEventHandler> propertyChangedHandler, string selectedPluginId)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsSettingsManager), settingsManager);
            var viewModel = fixture.Create<GeneralOptionsViewModel>();
            viewModel.PropertyChanged += propertyChangedHandler.Object;

            // Act
            viewModel.SelectedPluginId = selectedPluginId;

            // Assert
            propertyChangedHandler.Verify(self => self(viewModel, It.Is<PropertyChangedEventArgs>(e =>
                e.PropertyName == nameof(GeneralOptions.SelectedPluginId))),
                    Times.Once);
        }
    }
}

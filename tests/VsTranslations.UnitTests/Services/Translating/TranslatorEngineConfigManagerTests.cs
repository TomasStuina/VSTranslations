using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.Services.Translating;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using Xunit;

namespace VSTranslations.UnitTests.Services.Translating
{
    public class TranslatorEngineConfigManagerTests
    {
        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public async Task GetSupportedLanguagesAsync_WhenInvoked_ShouldInvokeConfigMethod(IFixture fixture, Mock<ITranslatorEngineConfig> engineConfig,
            IReadOnlyList<Language> languages)
        {
            // Arrange
            var configManager = fixture.Create<TranslatorEngineConfigManager>();

            // Act
            var actualLanguages = await configManager.GetSupportedLanguagesAsync();

            // Assert
            engineConfig.Verify(self => self.GetLanguagesAsync(), Times.Once);
            actualLanguages.Should().BeEquivalentTo(languages);
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public async Task GetSupportedLanguagesAsync_WhenEngineNull_ShouldReturnEmpty(IFixture fixture, Mock<ITranslatorEngineProvider> provider)
        {
            // Arrange
            provider.Setup(self => self.GetAsync()).ReturnsAsync((ITranslatorEngine) null);
            var configManager = fixture.Create<TranslatorEngineConfigManager>();

            // Act
            var actualLanguages = await configManager.GetSupportedLanguagesAsync();

            // Assert
            actualLanguages.Should().BeEmpty();
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public async Task GetSupportedLanguagesAsync_WhenEngineConfigNull_ShouldReturnEmpty(IFixture fixture, Mock<ITranslatorEngine> engine)
        {
            // Arrange
            engine.Setup(self => self.TranslatorEngineConfig).Returns((ITranslatorEngineConfig)null);
            var configManager = fixture.Create<TranslatorEngineConfigManager>();

            // Act
            var actualLanguages = await configManager.GetSupportedLanguagesAsync();

            // Assert
            actualLanguages.Should().BeEmpty();
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public async Task GetSourceLanguageAsync_WhenInvoked_ShouldInvokeConfigMethod(IFixture fixture, Mock<ITranslatorEngineConfig> engineConfig,
            Language language)
        {
            // Arrange
            var configManager = fixture.Create<TranslatorEngineConfigManager>();

            // Act
            var actualLanguage = await configManager.GetSourceLanguageAsync();

            // Assert
            engineConfig.Verify(self => self.GetSourceLanguageAsync(), Times.Once);
            actualLanguage.Should().BeSameAs(language);
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public async Task GetSourceLanguageAsync_WhenEngineNull_ShouldReturnInvariantLanguage(IFixture fixture, Mock<ITranslatorEngineProvider> provider)
        {
            // Arrange
            provider.Setup(self => self.GetAsync()).ReturnsAsync((ITranslatorEngine)null);
            var configManager = fixture.Create<TranslatorEngineConfigManager>();

            // Act
            var actualLanguage = await configManager.GetSourceLanguageAsync();

            // Assert
            actualLanguage.Should().BeSameAs(Language.Invariant);
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public async Task GetSourceLanguageAsync_WhenEngineConfigNull_ShouldReturnInvariantLanguage(IFixture fixture, Mock<ITranslatorEngine> engine)
        {
            // Arrange
            engine.Setup(self => self.TranslatorEngineConfig).Returns((ITranslatorEngineConfig)null);
            var configManager = fixture.Create<TranslatorEngineConfigManager>();

            // Act
            var actualLanguage = await configManager.GetSourceLanguageAsync();

            // Assert
            actualLanguage.Should().BeSameAs(Language.Invariant);
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public async Task GetTargetLanguageAsync_WhenInvoked_ShouldInvokeConfigMethod(IFixture fixture, Mock<ITranslatorEngineConfig> engineConfig,
            Language language)
        {
            // Arrange
            var configManager = fixture.Create<TranslatorEngineConfigManager>();

            // Act
            var actualLanguage = await configManager.GetTargetLanguageAsync();

            // Assert
            engineConfig.Verify(self => self.GetTargetLanguageAsync(), Times.Once);
            actualLanguage.Should().BeSameAs(language);
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public async Task GetTargetLanguageAsync_WhenEngineNull_ShouldReturnInvariantLanguage(IFixture fixture, Mock<ITranslatorEngineProvider> provider)
        {
            // Arrange
            provider.Setup(self => self.GetAsync()).ReturnsAsync((ITranslatorEngine)null);
            var configManager = fixture.Create<TranslatorEngineConfigManager>();

            // Act
            var actualLanguage = await configManager.GetTargetLanguageAsync();

            // Assert
            actualLanguage.Should().BeSameAs(Language.Invariant);
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public async Task GetTargetLanguageAsync_WhenEngineConfigNull_ShouldReturnInvariantLanguage(IFixture fixture, Mock<ITranslatorEngine> engine)
        {
            // Arrange
            engine.Setup(self => self.TranslatorEngineConfig).Returns((ITranslatorEngineConfig)null);
            var configManager = fixture.Create<TranslatorEngineConfigManager>();

            // Act
            var actualLanguage = await configManager.GetTargetLanguageAsync();

            // Assert
            actualLanguage.Should().BeSameAs(Language.Invariant);
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public async Task SetSourceLanguageAsync_WhenInvoked_ShouldInvokeConfigMethod(IFixture fixture, Mock<ITranslatorEngineConfig> engineConfig,
            Language language)
        {
            // Arrange
            var configManager = fixture.Create<TranslatorEngineConfigManager>();

            // Act
            await configManager.SetSourceLanguageAsync(language);

            // Assert
            engineConfig.Verify(self => self.SetSourceLanguageAsync(language), Times.Once);
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public async Task SetSourceLanguageAsync_WhenLanguageChanged_ShouldInvokeSourceLanguageChanged(IFixture fixture,
            Mock<Func<Language, ValueTask>> languageChanged, Language language)
        {
            // Arrange
            languageChanged.Setup(self => self(language)).Returns(new ValueTask());

            var configManager = fixture.Create<TranslatorEngineConfigManager>();
            configManager.SourceLanguageChanged = languageChanged.Object;

            // Act
            await configManager.SetSourceLanguageAsync(language);

            // Assert
            languageChanged.Verify(self => self(language), Times.Once);
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public void SetSourceLanguageAsync_WhenEngineNull_ShouldNotThrow(IFixture fixture, Mock<ITranslatorEngineProvider> provider, Language language)
        {
            // Arrange
            provider.Setup(self => self.GetAsync()).ReturnsAsync((ITranslatorEngine)null);
            var configManager = fixture.Create<TranslatorEngineConfigManager>();


            // Act & Assert
            FluentActions.Invoking(() => configManager.SetSourceLanguageAsync(language))
                .Should().NotThrow();
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public void SetSourceLanguageAsync_WhenEngineConfigNull_ShouldNotThrow(IFixture fixture, Mock<ITranslatorEngine> engine, Language language)
        {
            // Arrange
            engine.Setup(self => self.TranslatorEngineConfig).Returns((ITranslatorEngineConfig)null);
            var configManager = fixture.Create<TranslatorEngineConfigManager>();

            // Act & Assert
            FluentActions.Invoking(() => configManager.SetSourceLanguageAsync(language))
                .Should().NotThrow();
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public async Task SetTargetLanguageAsync_WhenInvoked_ShouldInvokeConfigMethod(IFixture fixture, Mock<ITranslatorEngineConfig> engineConfig,
            Language language)
        {
            // Arrange
            var configManager = fixture.Create<TranslatorEngineConfigManager>();

            // Act
            await configManager.SetTargetLanguageAsync(language);

            // Assert
            engineConfig.Verify(self => self.SetTargetLanguageAsync(language), Times.Once);
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public async Task SetTargetLanguageAsync_WhenLanguageChanged_ShouldInvokeTargetLanguageChanged(IFixture fixture,
            Mock<Func<Language, ValueTask>> languageChanged, Language language)
        {
            // Arrange
            languageChanged.Setup(self => self(language)).Returns(new ValueTask());

            var configManager = fixture.Create<TranslatorEngineConfigManager>();
            configManager.TargetLanguageChanged = languageChanged.Object;

            // Act
            await configManager.SetTargetLanguageAsync(language);

            // Assert
            languageChanged.Verify(self => self(language), Times.Once);
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public void SetTargetLanguageAsync_WhenEngineNull_ShouldNotThrow(IFixture fixture, Mock<ITranslatorEngineProvider> provider, Language language)
        {
            // Arrange
            provider.Setup(self => self.GetAsync()).ReturnsAsync((ITranslatorEngine)null);
            var configManager = fixture.Create<TranslatorEngineConfigManager>();


            // Act & Assert
            FluentActions.Invoking(() => configManager.SetTargetLanguageAsync(language))
                .Should().NotThrow();
        }

        [Theory]
        [TranslatorEngineConfigManagerAutoData]
        public void SetTargetLanguageAsync_WhenEngineConfigNull_ShouldNotThrow(IFixture fixture, Mock<ITranslatorEngine> engine, Language language)
        {
            // Arrange
            engine.Setup(self => self.TranslatorEngineConfig).Returns((ITranslatorEngineConfig)null);
            var configManager = fixture.Create<TranslatorEngineConfigManager>();

            // Act & Assert
            FluentActions.Invoking(() => configManager.SetTargetLanguageAsync(language))
                .Should().NotThrow();
        }
    }
}

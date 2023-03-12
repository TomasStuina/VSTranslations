using AutoFixture;
using Microsoft.VisualStudio.Sdk.TestFramework;
using Microsoft.VisualStudio.Shell;
using Moq;
using System.Threading.Tasks;
using VSTranslations.Commands;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.Services.Translating;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.UnitTests.Xunit;
using Xunit;

namespace VSTranslations.UnitTests.Commands;

public class SwapLanguagesCommandTests : VsTestBase
{
    public SwapLanguagesCommandTests(GlobalServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    [Theory]
    [SwapLanguagesCommandAutoData]
    public async Task ExecuteAsync_WhenSourceIsNull_ShouldNotSwapLanguages(IFixture fixture,
        OleMenuCmdEventArgs args, Mock<ITranslatorEngineConfig> engineConfig,
        Language target)
    {
        // Arrange
        var configManager = fixture.Create<TranslatorEngineConfigManager>();
        var command = new FakeSwapLanguagesCommand(configManager);

        target.Direction = LanguageDirection.TwoWay;

        engineConfig.Setup(self => self.GetSourceLanguageAsync()).Returns(new ValueTask<Language>((Language)null));
        engineConfig.Setup(self => self.GetTargetLanguageAsync()).Returns(new ValueTask<Language>(target));

        // Act
        await command.InvokeExecuteAsync(args);

        // Assert
        engineConfig.Verify(self => self.SetSourceLanguageAsync(It.IsAny<Language>()), Times.Never);
        engineConfig.Verify(self => self.SetTargetLanguageAsync(It.IsAny<Language>()), Times.Never);
    }

    [Theory]
    [SwapLanguagesCommandAutoData]
    public async Task ExecuteAsync_WhenTargetIsNull_ShouldNotSwapLanguages(IFixture fixture,
        OleMenuCmdEventArgs args, Mock<ITranslatorEngineConfig> engineConfig,
        Language source)
    {
        // Arrange
        var configManager = fixture.Create<TranslatorEngineConfigManager>();
        var command = new FakeSwapLanguagesCommand(configManager);

        source.Direction = LanguageDirection.TwoWay;

        engineConfig.Setup(self => self.GetSourceLanguageAsync()).Returns(new ValueTask<Language>(source));
        engineConfig.Setup(self => self.GetTargetLanguageAsync()).Returns(new ValueTask<Language>((Language)null));

        // Act
        await command.InvokeExecuteAsync(args);

        // Assert
        engineConfig.Verify(self => self.SetSourceLanguageAsync(It.IsAny<Language>()), Times.Never);
        engineConfig.Verify(self => self.SetTargetLanguageAsync(It.IsAny<Language>()), Times.Never);
    }

    [Theory]
    [SwapLanguagesCommandAutoData]
    public async Task ExecuteAsync_WhenSourceCannotBeTarget_ShouldNotSwapLanguages(IFixture fixture,
        OleMenuCmdEventArgs args, Mock<ITranslatorEngineConfig> engineConfig,
        Language source, Language target)
    {
        // Arrange
        var configManager = fixture.Create<TranslatorEngineConfigManager>();
        var command = new FakeSwapLanguagesCommand(configManager);

        source.Direction = LanguageDirection.Source;
        target.Direction = LanguageDirection.TwoWay;

        engineConfig.Setup(self => self.GetSourceLanguageAsync()).Returns(new ValueTask<Language>(source));
        engineConfig.Setup(self => self.GetTargetLanguageAsync()).Returns(new ValueTask<Language>(target));

        // Act
        await command.InvokeExecuteAsync(args);

        // Assert
        engineConfig.Verify(self => self.SetSourceLanguageAsync(It.IsAny<Language>()), Times.Never);
        engineConfig.Verify(self => self.SetTargetLanguageAsync(It.IsAny<Language>()), Times.Never);
    }

    [Theory]
    [SwapLanguagesCommandAutoData]
    public async Task ExecuteAsync_WhenTargetCannotBeSource_ShouldNotSwapLanguages(IFixture fixture,
        OleMenuCmdEventArgs args, Mock<ITranslatorEngineConfig> engineConfig,
        Language source, Language target)
    {
        // Arrange
        var configManager = fixture.Create<TranslatorEngineConfigManager>();
        var command = new FakeSwapLanguagesCommand(configManager);

        source.Direction = LanguageDirection.TwoWay;
        target.Direction = LanguageDirection.Target;

        engineConfig.Setup(self => self.GetSourceLanguageAsync()).Returns(new ValueTask<Language>(source));
        engineConfig.Setup(self => self.GetTargetLanguageAsync()).Returns(new ValueTask<Language>(target));

        // Act
        await command.InvokeExecuteAsync(args);

        // Assert
        engineConfig.Verify(self => self.SetSourceLanguageAsync(It.IsAny<Language>()), Times.Never);
        engineConfig.Verify(self => self.SetTargetLanguageAsync(It.IsAny<Language>()), Times.Never);
    }

    [Theory]
    [SwapLanguagesCommandAutoData]
    public async Task ExecuteAsync_WhenSourceAndTargetCanBeSwapped_ShouldSwapLanguages(IFixture fixture,
        OleMenuCmdEventArgs args, Mock<ITranslatorEngineConfig> engineConfig,
        Language source, Language target)
    {
        // Arrange
        var configManager = fixture.Create<TranslatorEngineConfigManager>();
        var command = new FakeSwapLanguagesCommand(configManager);

        source.Direction = LanguageDirection.TwoWay;
        target.Direction = LanguageDirection.TwoWay;

        engineConfig.Setup(self => self.GetSourceLanguageAsync()).Returns(new ValueTask<Language>(source));
        engineConfig.Setup(self => self.GetTargetLanguageAsync()).Returns(new ValueTask<Language>(target));

        // Act
        await command.InvokeExecuteAsync(args);

        // Assert
        engineConfig.Verify(self => self.SetSourceLanguageAsync(target), Times.Once);
        engineConfig.Verify(self => self.SetTargetLanguageAsync(source), Times.Once);
    }

    private class FakeSwapLanguagesCommand : SwapLanguagesCommand
    {
        public FakeSwapLanguagesCommand(TranslatorEngineConfigManager configManager) 
            : base(configManager)
        {

        }

        public Task InvokeExecuteAsync(OleMenuCmdEventArgs e) => ExecuteAsync(e);
    }
}
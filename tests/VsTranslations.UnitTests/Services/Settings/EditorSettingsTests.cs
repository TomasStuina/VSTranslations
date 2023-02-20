using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Sdk.TestFramework;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Moq;
using System;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.UnitTests.Moq.Extensions;
using VSTranslations.UnitTests.Xunit;
using VSTranslations.Extensions;
using VSTranslations.Services.Settings;
using Xunit;
using Constants = VSTranslations.Constants;

namespace VSTranslations.UnitTests.Services.Settings
{
    public class EditorSettingsTests : VsTestBase
    {
        public EditorSettingsTests(GlobalServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [Fact]
        public void Ctor_WhenFontAndColorStorageNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            FluentActions.Invoking(() => new EditorSettings(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [EditorSettingsAutoData]
        public async Task Ctor_WhenFontAndColorDataAccessible_ShouldInitializeProperties(IFixture fixture,
            Mock<IVsFontAndColorStorage> fontAndColorStorage)
        {
            // Arrange
            fontAndColorStorage.MockGetFont(nameof(GenericFontFamilies.Monospace), pointSize: 9);
            fontAndColorStorage.MockGetItem("Comment", background: Colors.White.ToRgba(), foreground: Colors.Black.ToRgba());

            // Act
            var editorSettings = fixture.Create<EditorSettings>();

            // Assert
            editorSettings.FontSize.Should().Be(new FontSizeConverter().ConvertFromPointSize(9));
            editorSettings.FontFamily.Should().NotBeNull();
            editorSettings.FontFamily.Source.Should().Be(nameof(GenericFontFamilies.Monospace));

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            editorSettings.CommentBackgroundColor.Should().BeOfType<SolidColorBrush>()
                .Which.Color.Should().Be(Colors.White);
            editorSettings.CommentForegroundColor.Should().BeOfType<SolidColorBrush>()
                .Which.Color.Should().Be(Colors.Black);
        }

        [Theory]
        [EditorSettingsAutoData]
        public void Ctor_WhenInvoked_ShouldOpenAndCloseSettingsCategory(IFixture fixture,
            Mock<IVsFontAndColorStorage> fontAndColorStorage)
        {
            // Arrange
            var textEditorGuid = Constants.Editor.TextEditorGuid;
            var readonlyCategoryFlags = Constants.Editor.ReadonlyCategoryFlags;

            // Act
            var editorSettings = fixture.Create<EditorSettings>();

            // Assert
            fontAndColorStorage.Verify(self => self.OpenCategory(ref textEditorGuid, readonlyCategoryFlags), Times.Once);
            fontAndColorStorage.Verify(self => self.CloseCategory(), Times.Once);
        }

        [Theory]
        [EditorSettingsAutoData]
        public void Ctor_WhenCategoryCannotBeOpen_ShouldStillAttemptClosingSettingsCategory(IFixture fixture,
            Mock<IVsFontAndColorStorage> fontAndColorStorage)
        {
            // Arrange
            var textEditorGuid = Constants.Editor.TextEditorGuid;
            var readonlyCategoryFlags = Constants.Editor.ReadonlyCategoryFlags;

            fontAndColorStorage
                .Setup(self => self.OpenCategory(ref textEditorGuid, readonlyCategoryFlags))
                .Returns(VSConstants.CO_E_CANCEL_DISABLED);

            // Act
            var editorSettings = fixture.Create<EditorSettings>();

            // Assert
            fontAndColorStorage.Verify(self => self.OpenCategory(ref textEditorGuid, readonlyCategoryFlags), Times.Once);
            fontAndColorStorage.Verify(self => self.CloseCategory(), Times.Once);
            fontAndColorStorage.VerifyNoOtherCalls();
        }

        [Theory]
        [EditorSettingsAutoData]
        public void Ctor_WhenFontInfoCannotBeLoaded_ShouldUseDefaultValues(IFixture fixture,
            Mock<IVsFontAndColorStorage> fontAndColorStorage)
        {
            // Arrange
            fontAndColorStorage.MockGetFont(It.IsAny<string>(), It.IsAny<ushort>(), VSConstants.CO_E_CANCEL_DISABLED);

            // Act
            var editorSettings = fixture.Create<EditorSettings>();

            // Assert
            editorSettings.FontSize.Should().Be(Constants.Editor.DefaultFontSize);
            editorSettings.FontFamily.Should().BeSameAs(Constants.Editor.DefaultFontFamily);
        }

        [Theory]
        [EditorSettingsAutoData]
        public async Task Ctor_WhenColorInfoCannotBeLoaded_ShouldUseDefaultValues(IFixture fixture,
            Mock<IVsFontAndColorStorage> fontAndColorStorage)
        {
            // Arrange
            fontAndColorStorage.MockGetItem("Comment", background: Colors.White.ToRgba(), foreground: Colors.Black.ToRgba(),
                VSConstants.CO_E_CANCEL_DISABLED);

            // Act
            var editorSettings = fixture.Create<EditorSettings>();

            // Assert
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            editorSettings.CommentBackgroundColor.Should().BeOfType<SolidColorBrush>()
                .Which.Color.Should().Be(Constants.Editor.DefaultBackgroundColor);
            editorSettings.CommentForegroundColor.Should().BeOfType<SolidColorBrush>()
                .Which.Color.Should().Be(Constants.Editor.DefaultForegroundColor);
        }
    }
}

using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Sdk.TestFramework;
using Microsoft.VisualStudio.Shell.Interop;
using Moq;
using System;
using System.Drawing.Text;
using System.Windows.Media;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.UnitTests.Moq.Extensions;
using VSTranslations.UnitTests.Xunit;
using VSTranslations.Adornments;
using VSTranslations.Extensions;
using VSTranslations.Services.Settings;
using Xunit;

namespace VSTranslations.UnitTests.Extensions
{
    public class EditorSettingsExtensionsTests : VsTestBase
    {
        public EditorSettingsExtensionsTests(GlobalServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [UITheory]
        [EditorSettingsAutoData]
        public void ApplyCommentTextStyleTo_WhenEditorSettingsNull_ShouldThrowArgumentNullException(TranslatedTextAdornment adornment)
        {
            // Act & Assert
            FluentActions.Invoking(() => EditorSettingsExtensions.ApplyCommentTextStyleTo(null, adornment))
                .Should().Throw<ArgumentNullException>();
        }

        [UITheory]
        [EditorSettingsAutoData]
        public void ApplyCommentTextStyleTo_WhenAdornmentNull_ShouldThrowArgumentNullException(IFixture fixture)
        {
            // Arrange
            var editorSettings = fixture.Create<EditorSettings>();

            // Act & Assert
            FluentActions.Invoking(() => editorSettings.ApplyCommentTextStyleTo(null))
                .Should().Throw<ArgumentNullException>();
        }

        [UITheory]
        [EditorSettingsAutoData]
        public void ApplyCommentTextStyleTo_WhenAdornmentNotNull_ShouldSetFontColorAndSize(IFixture fixture, Mock<IVsFontAndColorStorage> fontAndColorStorage,
            TranslatedTextAdornment adornment)
        {
            // Arrange
            fontAndColorStorage.MockGetFont(nameof(GenericFontFamilies.Monospace), pointSize: 10);
            fontAndColorStorage.MockGetItem("Comment", background: Colors.Magenta.ToRgba(), foreground: Colors.DarkMagenta.ToRgba());

            var editorSettings = fixture.Create<EditorSettings>();

            // Act
            editorSettings.ApplyCommentTextStyleTo(adornment);

            // Assert
            adornment.TranslatedTextBox.FontSize.Should().Be(editorSettings.FontSize);
            adornment.TranslatedTextBox.FontFamily.Should().Be(editorSettings.FontFamily);
            adornment.TranslatedTextBox.Foreground.Should().Be(editorSettings.CommentForegroundColor);
        }
    }
}

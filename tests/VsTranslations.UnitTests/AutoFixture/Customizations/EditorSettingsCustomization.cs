using AutoFixture;
using Moq;
using System.Windows.Media;
using VSTranslations;
using VSTranslations.Abstractions.Settings;

namespace VSTranslations.UnitTests.AutoFixture.Customizations
{
    public class EditorSettingsCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var editorSettings = fixture.Freeze<Mock<IEditorSettings>>();

            editorSettings.Setup(self => self.FontSize).Returns(Constants.Editor.DefaultFontSize);
            editorSettings.Setup(self => self.FontFamily).Returns(Constants.Editor.DefaultFontFamily);
            editorSettings.Setup(self => self.CommentForegroundColor).Returns(new SolidColorBrush(Constants.Editor.DefaultForegroundColor));
            editorSettings.Setup(self => self.CommentBackgroundColor).Returns(new SolidColorBrush(Constants.Editor.DefaultBackgroundColor));
        }
    }
}

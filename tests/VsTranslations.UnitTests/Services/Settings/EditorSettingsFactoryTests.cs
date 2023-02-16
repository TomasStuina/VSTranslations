using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.Sdk.TestFramework;
using Microsoft.VisualStudio.Text.Editor;
using System;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.UnitTests.Xunit;
using VSTranslations.Services.Settings;
using Xunit;

namespace VSTranslations.UnitTests.Services.Settings
{
    public class EditorSettingsFactoryTests : VsTestBase
    {
        public EditorSettingsFactoryTests(GlobalServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [Theory]
        [EditorSettingsFactoryAutoData]
        public void Create_WhenTextViewNull_ShouldThrowArgumentNulLException(IFixture fixture)
        {
            // Arrange
            var settingsFactory = fixture.Create<EditorSettingsFactory>();

            // Act & Assert
            FluentActions.Invoking(() => settingsFactory.GetOrCreate(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [EditorSettingsFactoryAutoData]
        public void Create_WhenTextViewNotNull_ShouldReturnEditorSettings(IFixture fixture, IWpfTextView textView)
        {
            // Arrange
            var settingsFactory = fixture.Create<EditorSettingsFactory>();

            // Act & Assert
            settingsFactory.GetOrCreate(textView).Should().BeOfType<EditorSettings>();
        }

        [Theory]
        [EditorSettingsFactoryAutoData]
        public void Create_WhenInvokedMoreThanOnce_ShouldReturnSameInstance(IFixture fixture, IWpfTextView textView)
        {
            // Arrange
            var settingsFactory = fixture.Create<EditorSettingsFactory>();

            // Act & Assert
            settingsFactory.GetOrCreate(textView)
                .Should().BeSameAs(settingsFactory.GetOrCreate(textView));
        }
    }
}

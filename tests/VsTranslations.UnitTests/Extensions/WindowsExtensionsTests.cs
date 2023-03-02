using Community.VisualStudio.Toolkit;
using FluentAssertions;
using Microsoft.VisualStudio.Sdk.TestFramework;
using Microsoft.VisualStudio.Shell.Interop;
using Moq;
using System;
using System.Threading.Tasks;
using VSTranslations.Extensions;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.UnitTests.Xunit;
using Xunit;

namespace VSTranslations.UnitTests.Extensions
{
    public class WindowsExtensionsTests : VsTestBase
    {
        public WindowsExtensionsTests(GlobalServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [Theory]
        [DefaultAutoData]
        public async Task WriteToOutputAsync_WhenWindowsNull_ShouldThrowArgumentNullException(string name, string messages)
        {
            // Act & Assert
            await FluentActions.Invoking(() => WindowsExtensions.WriteToOutputAsync(null, name, messages))
                .Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [WindowsAutoData]
        public async Task WriteToOutputAsync_WhenMessageProvided_ShouldWriteMessageToOutputWindow(string name, string message,
            Mock<IVsOutputWindow> outputWindow, Mock<IVsOutputWindowPaneNoPump> outputWindowPaneNoPumpMock)
        {
            // Arrange
            ServiceProvider.AddService(typeof(SVsOutputWindow), outputWindow.Object);

            // Act
            await VS.Windows.WriteToOutputAsync(name, message);

            // Assert
            outputWindowPaneNoPumpMock
                .Verify(self => self.OutputStringNoPump(message + Environment.NewLine), Times.Once);
        }
    }
}

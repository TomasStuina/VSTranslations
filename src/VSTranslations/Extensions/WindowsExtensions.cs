using VSTranslations.Common.Extensions;
using Toolkit = Community.VisualStudio.Toolkit;

namespace VSTranslations.Extensions
{
    /// <summary>
    /// <see cref="Toolkit.Windows"/> extensions.
    /// </summary>
    internal static class WindowsExtensions
    {
        /// <summary>
        /// Writes <paramref name="message"/> to the output window
        /// with the provided <paramref name="name"/>.
        /// </summary>
        /// <param name="windows"><see cref="Toolkit.Windows"/> instance.</param>
        /// <param name="name">Output pane name.</param>
        /// <param name="message">Message to write.</param>
        /// <returns><see cref="Task"/> indicating the completion.</returns>
        public static async Task WriteToOutputAsync(this Toolkit.Windows windows, string name, string message)
        {
            windows.ThrowIfNull(nameof(windows));

            var pane = await VS.Windows.CreateOutputWindowPaneAsync(name);
            await pane.WriteLineAsync(message);
        }
    }
}

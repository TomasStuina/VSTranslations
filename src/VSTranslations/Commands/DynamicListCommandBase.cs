using System.Runtime.InteropServices;

namespace VSTranslations.Commands;

public abstract class DynamicListCommandBase<TCommand, TItem> : DynamicItemsCommandBase<TCommand, TItem> where TCommand : class, new()
{
    protected override async Task ExecuteAsync(OleMenuCmdEventArgs eventArgs)
    {
        if (eventArgs is null || eventArgs == EventArgs.Empty)
        {
            return;
        }

        var inputValue = eventArgs.InValue;
        var outputValueHandle = eventArgs.OutValue;

        if (inputValue != null || outputValueHandle == IntPtr.Zero)
        {
            throw new ArgumentException();
        }

        var items = await GetItemsAsStringValuesAsync();
        Marshal.GetNativeVariantForObject(items, outputValueHandle);
        return;
    }
}

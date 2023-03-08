using EnvDTE;
using System.Runtime.InteropServices;

namespace VSTranslations.Commands;

public abstract class DynamicSelectCommandBase<TCommand, TItem> : DynamicItemsCommandBase<TCommand, TItem> where TCommand : class, new()
{
    private int _currentIndexChoice = 0;

    protected override async Task ExecuteAsync(OleMenuCmdEventArgs eventArgs)
    {
        if (eventArgs is null || eventArgs == EventArgs.Empty)
        {
            return;
        }

        var inputValue = eventArgs.InValue;
        var outputValueHandle = eventArgs.OutValue;

        if (outputValueHandle != IntPtr.Zero && inputValue != null)
        {
            throw new ArgumentException();
        }

        var items = await GetItemsAsync();
        if (outputValueHandle != IntPtr.Zero)
        {
            var itemsAsStringValues = await GetItemsAsStringValuesAsync();
            Marshal.GetNativeVariantForObject(itemsAsStringValues[_currentIndexChoice], outputValueHandle);
            return;
        }

        if (inputValue is null)
        {
            throw new ArgumentException();
        }

        TItem selectedItem = default;
        if (!int.TryParse(inputValue.ToString(), out var newChoice))
        {
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (string.Equals(DisplayText(item), inputValue.ToString(), StringComparison.Ordinal))
                {
                    newChoice = i;
                    selectedItem = item;
                    break;
                }
            }
        }

        if (newChoice != -1)
        {
            _currentIndexChoice = newChoice;
            await OnItemSelectedAsync(_currentIndexChoice, selectedItem);
            return;
        }

        throw new ArgumentException();
    }

    protected async Task SelectItemAsync(Func<TItem, bool> isMatch)
    {
        var items = await GetItemsAsync();
        for (int i = 0; i < items.Count; i++)
        {
            if (isMatch(items[i]))
            {
                _currentIndexChoice = i;
                return;
            }
        }
    }

    protected abstract Task OnItemSelectedAsync(int index, TItem item);
}

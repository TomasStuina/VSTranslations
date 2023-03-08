using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VSTranslations.Commands
{
    public abstract class DynamicItemsCommandBase<TCommand, TItem> : BaseCommand<TCommand> where TCommand : class, new()
    {
        private readonly AsyncLazy<IReadOnlyList<TItem>> _itemsFactory;

        protected DynamicItemsCommandBase()
        {
            _itemsFactory = new AsyncLazy<IReadOnlyList<TItem>>(InitializeItemsAsync,
                ThreadHelper.JoinableTaskFactory);
        }

        protected async ValueTask<IReadOnlyList<TItem>> GetItemsAsync() => await _itemsFactory.GetValueAsync();

        protected async ValueTask<string[]> GetItemsAsStringValuesAsync() => (await GetItemsAsync()).Select(item => DisplayText(item)).ToArray();

        protected abstract Task<IReadOnlyList<TItem>> InitializeItemsAsync();

        protected abstract string DisplayText(TItem item);
    }
}

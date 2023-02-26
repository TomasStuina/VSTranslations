using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Plugin.Abstractions.Translating;

namespace VSTranslations.Options
{
    public class GeneralOptionsViewModel : INotifyPropertyChanged
    {
        private readonly ITranslatorEngineProvider _translatorEngineProvider;
        private readonly GeneralOptions _generalOptions;
        private readonly List<ITranslatorEngineMetadata> _installedPlugins;

        public GeneralOptionsViewModel(ITranslatorEngineProvider translatorEngineProvider)
        {
            _generalOptions = GeneralOptions.Instance;
            _translatorEngineProvider = translatorEngineProvider;
            _installedPlugins = _translatorEngineProvider.GetAllMetadataEntries().ToList();

            EnsurePluginIsSelected(_generalOptions, _installedPlugins);
        }

        public IEnumerable<ITranslatorEngineMetadata> InstalledPlugins => _installedPlugins;

        public bool HasPluginsInstalled => _installedPlugins.Count > 0;

        public bool EnableCaching
        {
            get => _generalOptions.EnableCaching;
            set
            {
                _generalOptions.EnableCaching = value;
                _generalOptions.Save();
                OnPropertyChanged();
            }
        }

        public string SelectedPluginId
        {
            get => _generalOptions.SelectedPluginId;
            set
            {
                _generalOptions.SelectedPluginId = value;
                _generalOptions.Save();
                OnPropertyChanged();
            }
        }

        private void EnsurePluginIsSelected(GeneralOptions options, IEnumerable<ITranslatorEngineMetadata> installedPlugins)
        {
            var selectedPluginId = options.SelectedPluginId;

            if (!string.IsNullOrEmpty(selectedPluginId)
                && installedPlugins.Any(plugin => string.Equals(plugin.Id, selectedPluginId, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            options.SelectedPluginId = installedPlugins?.FirstOrDefault()?.Id;
            options.Save();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

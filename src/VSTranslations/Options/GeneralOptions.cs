using System.ComponentModel;

namespace VSTranslations.Options
{
    public class GeneralOptions : BaseOptionModel<GeneralOptions>
    {
        [Category("General")]
        [DisplayName("Enable translations caching")]
        [Description("Caches translated text in the memory")]
        public bool EnableCaching { get; set; }

        [Category("General")]
        [DisplayName("Plugin")]
        [Description("Plugin to use for translations")]
        public string SelectedPluginId { get; set; }
    }

}

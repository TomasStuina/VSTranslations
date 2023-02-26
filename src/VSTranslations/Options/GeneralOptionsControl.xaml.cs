using System.Windows.Controls;

namespace VSTranslations.Options
{
    /// <summary>
    /// Interaction logic for GeneralOptionsControl.xaml
    /// </summary>
    public partial class GeneralOptionsControl : UserControl
    {
        public GeneralOptionsControl(GeneralOptionsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public void Initialize()
        {
        }
    }
}

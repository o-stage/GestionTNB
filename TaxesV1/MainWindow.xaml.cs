using System.Windows;

namespace TaxesV1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            if (Properties.Settings.Default.Language == "ar") SetFlowDirection(Body, FlowDirection.RightToLeft);
            InitializeComponent();
            Body.Children.Add(new TaxesSureTNB());
        }
    }
}
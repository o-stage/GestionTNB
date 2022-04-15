using System.Windows;
using System.Windows.Controls;

namespace TaxesV1
{
    public partial class MainWindow : Window
    {
        private Panel _currentPanel;
        private Button _currentButton;

        public MainWindow()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Language == "ar") SetFlowDirection(Body, FlowDirection.RightToLeft);
            _currentPanel = TaxesSureTNB.GetInstance();
            Body.Children.Insert(1, _currentPanel);
            SideMenu.OnMenuItemClicked += ((button, args) =>
            {
                Body.Children.Remove(_currentPanel);

                SideMenu.ActiveButton = button;

                if (button.Name == "Settings")
                {
                    _currentPanel = Settings.GetInstance();
                }
                else if (button.Name == "File") {
                    _currentPanel = Fichier.GetInstance();

                }
                else
                {
                    _currentPanel = TaxesSureTNB.GetInstance();
                }

                Body.Children.Insert(1, _currentPanel);
            });
        }
    }
}
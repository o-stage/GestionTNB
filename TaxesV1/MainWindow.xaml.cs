using System.Windows;
using System.Windows.Controls;

namespace TaxesV1
{
    public partial class MainWindow : Window
    {
        private Panel _currentPanel;

        public MainWindow()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Language == "ar") SetFlowDirection(Body, FlowDirection.RightToLeft);

            SetCurrentPanel(TaxesSureTNB.GetInstance());

            SideMenu.OnMenuItemClicked += (button, args) =>
            {
                SideMenu.ActiveButton = button;
                switch (button.Name)
                {
                    case "Settings":
                        SetCurrentPanel(Settings.GetInstance());
                        break;
                    case "File":
                        SetCurrentPanel(Fichier.GetInstance());
                        break;
                    default:
                        SetCurrentPanel(TaxesSureTNB.GetInstance());
                        break;
                }
            };
        }

        private void SetCurrentPanel(Panel panel)
        {
            if (_currentPanel.Parent != null)
                Body.Children.Remove(_currentPanel);
            _currentPanel = panel;
            Body.Children.Insert(1, _currentPanel);
        }
    }
}
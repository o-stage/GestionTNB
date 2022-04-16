using System.Windows;
using System.Windows.Controls;

namespace TaxesV1
{
    public partial class MainWindow : Window
    {
        private Panel _currentPanel;
        private Fichier _fichierPanel;
        private Settings _settingsPanel;
        private TaxesSureTNB _taxesSureTnbPanel;
        private UserProfile _userProfile;


        public MainWindow()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Language == "ar") SetFlowDirection(Body, FlowDirection.RightToLeft);

            SetCurrentPanel(TaxesSureTnbPanel);

            SideMenu.OnUserProfileClicked += () => { SetCurrentPanel(UserProfile); };
            SideMenu.OnMenuItemClicked += (button, args) =>
            {
                SideMenu.ActiveButton = button;
                switch (button.Name)
                {
                    case "Settings":
                        SetCurrentPanel(SettingsPanel);
                        break;
                    case "File":
                        SetCurrentPanel(FichierPanel);
                        break;
                    case "Logout":
                        new LoginWindow().Show();
                        Close();
                        break;
                    default:
                        SetCurrentPanel(TaxesSureTnbPanel);
                        break;
                }
            };
        }

        public UserProfile UserProfile
        {
            get { return _userProfile ?? (_userProfile = new UserProfile()); }
            set => _userProfile = value;
        }

        public TaxesSureTNB TaxesSureTnbPanel
        {
            get { return _taxesSureTnbPanel ?? (_taxesSureTnbPanel = new TaxesSureTNB()); }
            set => _taxesSureTnbPanel = value;
        }

        private Settings SettingsPanel
        {
            get { return _settingsPanel ?? (_settingsPanel = new Settings()); }
            set => _settingsPanel = value;
        }

        private Fichier FichierPanel
        {
            get
            {
                return _fichierPanel ?? (_fichierPanel = new Fichier());
                ;
            }
            set => _fichierPanel = value;
        }

        private void SetCurrentPanel(Panel panel)
        {
            if (_currentPanel != null)
                Body.Children.Remove(_currentPanel);
            _currentPanel = panel;
            Body.Children.Insert(1, _currentPanel);
        }
    }
}
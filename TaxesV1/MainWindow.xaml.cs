using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TaxesV1
{
    public partial class MainWindow : Window
    {
        private Panel _currentPanel;
        private Fichier _fichierPanel;
        private ManageUsers _manageUsersPanel;
        private Settings _settingsPanel;
        private TaxesSureTNB _taxesSureTnbPanel;
        private UserProfile _userProfilePanel;

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
                    case "ManageUsersMenuItem":
                        SetCurrentPanel(ManageUsersPanel);
                        break;
                    default:
                        SetCurrentPanel(TaxesSureTnbPanel);
                        break;
                }
            };
        }

        public ManageUsers ManageUsersPanel
        {
            get { return _manageUsersPanel ?? (_manageUsersPanel = new ManageUsers()); }
            set => _manageUsersPanel = value;
        }

        public UserProfile UserProfile
        {
            get { return _userProfilePanel ?? (_userProfilePanel = new UserProfile()); }
            set => _userProfilePanel = value;
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

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (Data.Entities.ChangeTracker.HasChanges())
            {
                MessageBoxResult result = MessageBox.Show(this,
                    "you have unsaved changes would you like to save them before closing?", "Confirm Closing",
                    MessageBoxButton.YesNoCancel);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Data.Entities.SaveChanges();
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }
    }
}
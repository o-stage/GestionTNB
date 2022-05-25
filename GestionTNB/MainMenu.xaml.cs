using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestionTNB
{
    public partial class MainMenu : UserControl
    {
        public delegate void OnMenuItemClickedDelegate(Button MenuItem, RoutedEventArgs e);

        public delegate void OnUserProfileClickedDelegate();

        private Button _activeButton;
        Style ActiveMenuButtonStyle;

        Style MenuButtonStyle;

        public MainMenu()
        {
            InitializeComponent();
            MenuButtonStyle = FindResource("MenuButton") as Style;
            ActiveMenuButtonStyle = FindResource("MenuButtonActive") as Style;
            _activeButton = Dashboard;


            string Name = Data.Entities.Database.SqlQuery<string>("select SUSER_NAME()").FirstOrDefault();
            int isAdmin = Data.Entities.Database.SqlQuery<int>("select IS_ROLEMEMBER ('db_owner')").FirstOrDefault();

            if (isAdmin != 1) ManageUsersMenuItem.Visibility = Visibility.Collapsed;
            UserName.Text = Name;
            var initials = Name.Split(' ');
            UserInitials.Text = initials[0][0].ToString() + initials.Last()[0];
            //Auth.Text = user.ROLE;

            foreach (var button in Menu.Children.OfType<Button>())
                button.Click += (sender, e) => OnMenuItemClicked?.Invoke((Button)sender, e);
        }

        public Button ActiveButton
        {
            get => _activeButton;
            set
            {
                _activeButton.Style = MenuButtonStyle;
                _activeButton = value;
                _activeButton.Style = ActiveMenuButtonStyle;
            }
        }

        public event OnMenuItemClickedDelegate OnMenuItemClicked;
        public event OnUserProfileClickedDelegate OnUserProfileClicked;

        private void UserProfile_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            OnUserProfileClicked?.Invoke();
        }
    }
}
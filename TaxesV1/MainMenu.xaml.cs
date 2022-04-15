using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TaxesV1
{
    public partial class MainMenu : UserControl
    {
        public delegate void OnMenuItemClickedDelegate(Button MenuItem, RoutedEventArgs e);

        public OnMenuItemClickedDelegate OnMenuItemClicked;

        Style MenuButtonStyle;
        Style ActiveMenuButtonStyle;

        private Button _activeButton;

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

        public MainMenu()
        {
            InitializeComponent();
            MenuButtonStyle = FindResource("MenuButton") as Style;
            ActiveMenuButtonStyle = FindResource("MenuButtonActive") as Style;
            _activeButton = Dashboard;
            user user = Data.Entities.users.First(user1 => user1.USER_NAME == Properties.Settings.Default.User);
            if (user != null)
            {
                UserName.Text = user.USER_NAME;
                var initials = user.USER_NAME.Split(' ');
                UserInitials.Text = initials[0][0].ToString() + initials.Last()[0];
                Auth.Text = user.ROLE;
            }

            foreach (var button in Menu.Children.OfType<Button>())
                button.Click += (sender, e) =>
                {
                    if (OnMenuItemClicked != null)
                        OnMenuItemClicked.Invoke((Button)sender, e);
                };
        }
    }
}
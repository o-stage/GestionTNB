using System.Linq;
using System.Windows.Controls;

namespace TaxesV1
{
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
            user user = Data.Entities.users.First(user1 => user1.USER_NAME == Properties.Settings.Default.User);
            if (user != null)
            {
                UserName.Text = user.USER_NAME;
                var initials = user.USER_NAME.Split(' ');
                UserInitials.Text = initials[0][0].ToString() + initials.Last()[0];
                Auth.Text = user.ROLE;
            }
        }
    }
}
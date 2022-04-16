using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TaxesV1
{
    public partial class UserProfile : DockPanel
    {
        private DatabasePrincipal principal;

        public UserProfile()
        {
            InitializeComponent();

            principal = Data.Entities.Database
                .SqlQuery<DatabasePrincipal>("select * from sys.database_principals where sid= SUSER_SID()")
                .FirstOrDefault();


            if (principal.authentication_type == 2)
                ChangePasswordPanel.IsEnabled = true;
            Details.Text = principal.authentication_type_desc;
        }

        private void ChangePassword_OnClick(object sender, RoutedEventArgs e)
        {
            if (NewPasswordTextBox.Password == ConfirmNewPasswordTextBox.Password)
            {
                try
                {
                    Data.Entities.Database.ExecuteSqlCommand(
                        $"ALTER USER [{principal.name}] WITH PASSWORD=N'{NewPasswordTextBox.Password}' OLD_PASSWORD=N'{OldPasswordTextBox.Password}'");
                    new LoginWindow().Show();
                    Window.GetWindow(this)?.Close();
                }
                catch (SqlException)
                {
                    Details.Text = "Incorrect password";
                }
            }
            else
            {
                Details.Text = "The password and confirm password fields do not match.";
            }
        }
    }
}
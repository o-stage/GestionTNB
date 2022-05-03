using System.Windows;

namespace TaxesV1
{
    public partial class NewUserWindow : Window
    {
        public NewUserWindow()
        {
            InitializeComponent();
        }

        private void NewUserButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Password == ConfirmPasswordTextBox.Password)
            {
                Data.Entities.Database.ExecuteSqlCommand(
                    $"CREATE USER [{UserNameTextBox.Text}] WITH PASSWORD = '{PasswordTextBox.Password}';");
                Data.Entities.Database.ExecuteSqlCommand(
                    $"GRANT SELECT ON Constants TO {UserNameTextBox.Text} AS dbo");
                Close();
            }
            else
            {
                ErrorLabel.Visibility = Visibility.Visible;
            }
        }
    }
}
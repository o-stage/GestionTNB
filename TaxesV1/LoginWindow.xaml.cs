using System.Data.SqlClient;
using System.Windows;

namespace TaxesV1
{
    public partial class LoginWindow
    {
        public LoginWindow()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
            UseWindowsAuthentication.IsChecked = Properties.Settings.Default.UseWindowsAuthentication;
            DataContext = this;
            int index = 0;
            switch (Properties.Settings.Default.Language)
            {
                case "en":
                    index = 0;
                    break;
                case "fr":
                    index = 1;
                    break;
                case "ar":
                    index = 2;
                    break;
            }

            if (index == 2) SetFlowDirection(Body, FlowDirection.RightToLeft);
            LanguageComboBox.SelectedIndex = index;
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            if (UseWindowsAuthentication.IsChecked != null && UseWindowsAuthentication.IsChecked.Value)
            {
                Data.Entities = new TaxesV2Entities();
                new MainWindow().Show();
                Properties.Settings.Default.UseWindowsAuthentication = true;
                Properties.Settings.Default.Save();
                Close();
                return;
            }

            try
            {
                Data.Entities = new TaxesV2Entities(UserNameTextBox.Text, PasswordTextBox.Password);
                Data.Entities.Database.Connection.Open();
                Data.Entities.Database.Connection.Close();
                new MainWindow().Show();
                Properties.Settings.Default.UseWindowsAuthentication = false;
                Properties.Settings.Default.Save();
                Close();
            }
            catch (SqlException)
            {
                ErrorLabel.Visibility = Visibility.Visible;
            }
        }

        private void UseWindowsAuthentication_OnChecked(object sender, RoutedEventArgs e)
        {
            InputPanel.IsEnabled = false;
        }

        private void UseWindowsAuthentication_OnUnchecked(object sender, RoutedEventArgs e)
        {
            InputPanel.IsEnabled = true;
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string Language = "en";
            switch (LanguageComboBox.SelectedIndex)
            {
                case 0:
                    Language = "en";
                    break;
                case 1:
                    Language = "fr";
                    break;
                case 2:
                    Language = "ar";
                    break;
            }

            if (Properties.Settings.Default.Language != Language)
            {
                Properties.Settings.Default.Language = Language;
                Properties.Settings.Default.Save();
                new LoginWindow().Show();
                Close();
            }
        }
    }
}
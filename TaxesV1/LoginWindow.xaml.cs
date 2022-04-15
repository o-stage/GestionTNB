using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Media;
using TaxesV1.Resources;

namespace TaxesV1
{
    public partial class LoginWindow
    {
        public LoginWindow()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                new System.Globalization.CultureInfo(Properties.Settings.Default.Language);

            InitializeComponent();
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
                Close();
                return;
            }

            try
            {
                Data.Entities = new TaxesV2Entities(UserNameTextBox.Text, PasswordTextBox.Password);
                new MainWindow().Show();
                Close();
            }
            catch (SqlException exception)
            {
                ErrorLabel.Visibility = Visibility.Visible;
            }
        }

        private void UseWindowsAuthentication_OnChecked(object sender, RoutedEventArgs e)
        {
            UserNameTextBox.IsEnabled = false;
            PasswordTextBox.IsEnabled = false;
        }

        private void UseWindowsAuthentication_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UserNameTextBox.IsEnabled = true;
            PasswordTextBox.IsEnabled = true;
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
using System.Data.SqlClient;
using System.Windows;
using TaxesV1.Resources;

namespace TaxesV1
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                new System.Globalization.CultureInfo(Properties.Settings.Default.Language);

            InitializeComponent();

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

            LanguageComboBox.SelectedIndex = index;
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch (LanguageComboBox.SelectedIndex)
            {
                case 0:
                    Properties.Settings.Default.Language = "en";
                    break;
                case 1:
                    Properties.Settings.Default.Language = "fr";
                    break;
                case 2:
                    Properties.Settings.Default.Language = "ar";
                    break;
            }

            Properties.Settings.Default.Save();
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            
            SqlConnection Connection =
                new SqlConnection("Data Source=.;Initial Catalog=TaxesV2;Integrated Security=True");
         Connection.Open();
            SqlCommand cmd = new SqlCommand($"SELECT * FROM users WHERE USER_NAME='{UserNameTextBox.Text}' AND PASSWORD='{PasswordTextBox.Password}'",Connection);
            if (cmd.ExecuteReader().HasRows)
            {
                Connection.Close();
                
                new TaxesSureTNB().Show();
                Close();
            }
            else
            {
                Connection.Close();
                ErrorLabel.Content = "Invalid username or password";
            }

        }
    }
}
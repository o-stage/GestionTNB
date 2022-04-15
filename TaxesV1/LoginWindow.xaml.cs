using System.Data.SqlClient;
using System.Windows;
using TaxesV1.Resources;

namespace TaxesV1
{
    public partial class MainWindow
    {
        public string CurrentPasswordHintText { get; set; }

        public MainWindow()
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
                new MainWindow().Show();
                Close();
            }
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            SqlConnection Connection =
                new SqlConnection("Data Source=.;Initial Catalog=TaxesV2;Integrated Security=True");
            Connection.Open();
            SqlCommand cmd =
                new SqlCommand(
                    $"SELECT * FROM users WHERE USER_NAME='{UserNameTextBox.Text}' AND PASSWORD='{PasswordTextBox.Password}'",
                    Connection);
            if (cmd.ExecuteReader().HasRows)
            {
                Connection.Close();
                Properties.Settings.Default.User = UserNameTextBox.Text;
                Properties.Settings.Default.Save();
                new TaxesSureTNB().Show();
                Close();
            }
            else
            {
                Connection.Close();
                ErrorLabel.Visibility = Visibility.Visible;
            }
        }
    }
}
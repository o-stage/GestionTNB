using System.Windows;
using System.Windows.Controls;

namespace TaxesV1
{
    public partial class Settings : DockPanel
    {
        public Settings()
        {
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

        private void ButtonSaveSettings_OnClick(object sender, RoutedEventArgs e)
        {
            string defaultLanguage = "en";
            switch (LanguageComboBox.SelectedIndex)
            {
                case 0:
                    defaultLanguage = "en";
                    break;
                case 1:
                    defaultLanguage = "fr";
                    break;
                case 2:
                    defaultLanguage = "ar";
                    break;
            }

            if (Properties.Settings.Default.Language != defaultLanguage)
                Properties.Settings.Default.Language = defaultLanguage;
            Properties.Settings.Default.Save();
        }
    }
}
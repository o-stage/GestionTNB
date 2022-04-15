using System.Windows.Controls;

namespace TaxesV1
{
    public partial class Settings : DockPanel
    {
        private static Settings _instance;

        public static Settings GetInstance()
        {
            return _instance ?? (_instance = new Settings());
        }

        private Settings()
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
            }
        }
    }
}
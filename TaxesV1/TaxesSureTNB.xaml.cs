using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TaxesV1
{
    public partial class TaxesSureTNB : Window
    {
        TaxesV2Entities Entities;
        private CollectionViewSource ViewSource = new CollectionViewSource();

        public TaxesSureTNB()
        {
            Entities = Data.Entities;
            InitializeComponent();
            if (Properties.Settings.Default.Language == "ar") SetFlowDirection(Body, FlowDirection.RightToLeft);
            DataGrid.ItemsSource = Entities.Dossiers.Take(30).ToList();
            // ViewSource.Source = Entities.Declarations;
            //   DataGrid.ItemsSource = Entities.Dossiers.ToList();
        }

        private void ButtonFetch_Click(object sender, RoutedEventArgs e)
        {
            Dossier dossier = Entities.Dossiers.Find(FileNumberTextBox.Text);
        }
    }
}
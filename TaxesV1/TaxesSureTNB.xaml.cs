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
        public Dossier SelectedFile { get; set; }
        public TaxesSureTNB()
        {
            Entities = Data.Entities;
            InitializeComponent();
            if (Properties.Settings.Default.Language == "ar") SetFlowDirection(Body, FlowDirection.RightToLeft);
        }

        private void ButtonFetch_Click(object sender, RoutedEventArgs e)
        {
            Dossier dossier = Entities.Dossiers.Find(FileNumberTextBox.Text);
        }

        private void ButtonCalculateTaxes_OnClick(object sender, RoutedEventArgs e)
        {
            SelectedFile = Data.Entities.Dossiers.Find(FileNumberTextBox.Text);
            DataContext = SelectedFile;
            Taxes tax = Taxes.GetTaxes(SelectedFile);
            DataGrid.ItemsSource = tax._taxes;
        }
    }
}
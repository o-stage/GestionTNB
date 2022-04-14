using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using TaxesV1.Resources;

namespace TaxesV1
{
    public partial class TaxesSureTNB : Window
    {
        TaxesV2Entities Entities;
        private CollectionViewSource ViewSource = new CollectionViewSource();
        private Taxes tax;
        public Dossier SelectedFile { get; set; }

        public TaxesSureTNB()
        {
            Entities = Data.Entities;
            InitializeComponent();
            if (Properties.Settings.Default.Language == "ar") SetFlowDirection(Body, FlowDirection.RightToLeft);
        }

        private void ButtonFetch_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() != true)
                return;
            PrintDocument doc = new PrintDocument(tax, SelectedFile)
            {
                PageHeight = dialog.PrintableAreaHeight,
                PageWidth = dialog.PrintableAreaWidth,
                PagePadding = new Thickness(50),
                ColumnGap = 0,
                ColumnWidth = dialog.PrintableAreaWidth,
                Name = "FlowDoc"
            };
            IDocumentPaginatorSource idpSource = doc;
            dialog.PrintDocument(idpSource.DocumentPaginator, "Printing.");
        }

        private void ButtonCalculateTaxes_OnClick(object sender, RoutedEventArgs e)
        {
            var cultureInfo = new CultureInfo("fr-ma");
            SelectedFile = Data.Entities.Dossiers.Find(FileNumberTextBox.Text);
            if (SelectedFile == null)
            {
                MessageBox.Show(this,Strings.FileNotFound);
                return;
            }
            tax = Taxes.GetTaxes(SelectedFile);
            DataContext = SelectedFile;
            NumberOfNonDeposedDeclarations.Text = tax.NumberOfNonDeposedDeclarations.ToString();
            TotalPrincipal.Text = tax.TotalNet.ToString("c", cultureInfo);
            TotalAmends.Text = tax.TotalAmends.ToString("c", cultureInfo);
            Total.Text = tax.Total.ToString("c", cultureInfo);
            DataGrid.ItemsSource = tax._taxes;
        }
    }
}
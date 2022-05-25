using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using GestionTNB.Resources;

namespace GestionTNB
{
    public partial class TaxesSureTNB : DockPanel
    {
        private Taxes _tax;

        public TaxesSureTNB()
        {
            InitializeComponent();
        }

        public Dossier SelectedFile { get; set; }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() != true)
                return;
            PrintDocument doc = new PrintDocument(_tax, SelectedFile)
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

        private void FindFile_OnClick(object sender, RoutedEventArgs e)
        {
            ShowFile(FileNumberTextBox.Text);
        }

        private void ShowFile(string fileNumber)
        {
            var cultureInfo = new CultureInfo("fr-ma");
            SelectedFile = Data.Entities.Dossiers.Find(fileNumber);
            if (SelectedFile == null)
            {
                MessageBox.Show(Window.GetWindow(this), Strings.FileNotFound);
                return;
            }

            _tax = Taxes.GetTaxes(SelectedFile);
            DataContext = SelectedFile;
            NumberOfNonDeposedDeclarations.Text = _tax.NumberOfNonDeposedDeclarations.ToString();
            TotalPrincipal.Text = _tax.TotalNet.ToString("c", cultureInfo);
            TotalAmends.Text = _tax.TotalAmends.ToString("c", cultureInfo);
            Total.Text = _tax.Total.ToString("c", cultureInfo);
            DataGrid.ItemsSource = _tax._taxes;
            PrintButton.IsEnabled = true;
            NewDeclarationButton.IsEnabled = true;
            EditFile.IsEnabled = true;
        }

        private void NewDeclarationButton_OnClick(object sender, RoutedEventArgs e)
        {
            NewDeclaration newDeclaration = new NewDeclaration
            {
                Owner = Window.GetWindow(this),
                NDossier = FileNumber.Text
            };
            newDeclaration.ShowDialog();
            ShowFile(FileNumber.Text);
        }

        private void ModifyFile_OnClick(object sender, RoutedEventArgs e)
        {
            EditFile editFile = new EditFile(FileNumber.Text)
            {
                Owner = Window.GetWindow(this)
            };

            editFile.ShowDialog();
        }

        private void SelectAll_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (DataGrid.ItemsSource == null) return;
            foreach (Taxes.Tax tax in DataGrid.Items)
                tax.Selected = false;
            // for some reason we need to commit edits 2 times for it to work
            DataGrid.CommitEdit();
            DataGrid.CommitEdit();
            CollectionViewSource.GetDefaultView(DataGrid.ItemsSource).Refresh();
        }

        private void SelectAll_OnChecked(object sender, RoutedEventArgs e)
        {
            if (DataGrid.ItemsSource == null) return;
            foreach (Taxes.Tax tax in DataGrid.Items)
                tax.Selected = true;
            DataGrid.CommitEdit();
            DataGrid.CommitEdit();
            CollectionViewSource.GetDefaultView(DataGrid.ItemsSource).Refresh();
        }
    }
}
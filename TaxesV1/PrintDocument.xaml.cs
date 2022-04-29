using System;
using System.Globalization;
using System.Windows.Documents;

namespace TaxesV1
{
    public partial class PrintDocument : FlowDocument
    {
        CultureInfo Culture = new CultureInfo("fr-ma");

        public PrintDocument(Taxes taxes, Dossier dossier)
        {
            Taxes = taxes;
            Dossier = dossier;
            DataContext = Dossier;
            InitializeComponent();

            foreach (var tax in Taxes._taxes)
            {
                if (!tax.Selected) continue;
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell(new Paragraph(new Run(tax.Year.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(tax.MtPrincipal.ToString("C", Culture)))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(tax.DefaultDecl.ToString("C", Culture)))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(tax.Amends.ToString("C", Culture)))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(tax.Total.ToString("C", Culture)))));
                TaxesRowRowGroup.Rows.Add(row);
            }

            TodayDate.Text = "Le: " + DateTime.Now.ToString("dd/MM/yy");
            TotalPrincipal.Text = taxes.TotalNet.ToString("c", Culture);
            TotalAmmends.Text = taxes.TotalAmends.ToString("c", Culture);
            TotalDefaultDeclaration.Text = taxes.TotalDefaultDec.ToString("c", Culture);
            Total.Text = taxes.Total.ToString("c", Culture);
            TotalWords.Text = MoneyTools.ConvertToWords(taxes.Total).ToUpper();
        }

        public Taxes Taxes { get; set; }
        public Dossier Dossier { get; set; }
    }
}
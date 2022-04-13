using System.Windows;
using System.Windows.Documents;

namespace TaxesV1
{
    public partial class PrintDocument : FlowDocument
    {
        public Taxes Taxes { get; set; }
        public Dossier Dossier { get; set; }

        public PrintDocument()
        {
            InitializeComponent();
        }
    }
}
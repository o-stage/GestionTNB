using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TaxesV1
{
    public partial class TaxesSureTNB : Window
    {
        private TaxesV2Entities Entities = new TaxesV2Entities();
        public TaxesSureTNB()
        {
            InitializeComponent();
            if(Properties.Settings.Default.Language=="ar")SetFlowDirection(Body,FlowDirection.RightToLeft);
         //   DataGrid.ItemsSource = Entities.Dossiers.ToList();
        }
        
        private void ButtonFetch_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
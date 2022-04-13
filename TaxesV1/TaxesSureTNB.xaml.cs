using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TaxesV1
{
    public partial class TaxesSureTNB : Window
    {
        private TaxesV2Entities Entities = new TaxesV2Entities();
        private CollectionViewSource ViewSource = new CollectionViewSource();
        public TaxesSureTNB()
        {
            InitializeComponent();
            if(Properties.Settings.Default.Language=="ar")SetFlowDirection(Body,FlowDirection.RightToLeft);
            DataContext = Entities.Declarations;
           // ViewSource.Source = Entities.Declarations;
            //   DataGrid.ItemsSource = Entities.Dossiers.ToList();
        }
        
        private void ButtonFetch_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
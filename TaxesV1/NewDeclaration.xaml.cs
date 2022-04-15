using System.Windows;

namespace TaxesV1
{
    public partial class NewDeclaration : Window
    {
        public NewDeclaration()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Language == "ar") SetFlowDirection(Body, FlowDirection.RightToLeft);
        }
    }
}
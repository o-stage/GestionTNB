using System;
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

        private void AddDecleration_OnClick(object sender, RoutedEventArgs e)
        {
            Declaration declaration = new Declaration
            {
                DateDeclaration = DateTime.Now,
                NDossier = FileNumberTextBox.Text,
                Payer = PayedCheckBox.IsChecked.Value,
                Anne = int.Parse(YearTextBox.Text)
            };
            Data.Entities.Declarations.Add(declaration);
        }
    }
}
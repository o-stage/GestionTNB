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
            YearTextBox.Text = DateTime.Now.Year.ToString();
        }

        public string NDossier
        {
            private get => FileNumberTextBox.Text;
            set => FileNumberTextBox.Text = value;
        }


        private void AddDeclaration_OnClick(object sender, RoutedEventArgs e)
        {
            Declaration declaration = new Declaration
            {
                NDossier = FileNumberTextBox.Text,
                DateDeclaration = DateTime.Now,
                Payer = PayedCheckBox.IsChecked.Value,
                Anne = int.Parse(YearTextBox.Text)
            };
            Data.Entities.Declarations.Add(declaration);
            Close();
        }
    }
}
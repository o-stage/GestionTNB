using System.Windows;

namespace TaxesV1
{
    public partial class EditFile : Window
    {
        private string _fileNumber;

        public EditFile(string fileNumber)
        {
            _fileNumber = fileNumber;
            InitializeComponent();
        }
    }
}
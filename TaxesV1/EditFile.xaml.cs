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
            Dossier dossier = Data.Entities.Dossiers.Find(_fileNumber);
            if (dossier != null)
            {
                DataContext = dossier;
                FileNumber.Text = _fileNumber;
                StartDatePicker.Text = dossier.DateDebut.ToString();
                DataContext = dossier;
                Terrain terrain = Data.Entities.Terrains.Find(dossier.TerrainID);
                if (terrain != null)
                {
                    
                    Etat.Text = terrain.Etat;
                    DateChangementEtat.SelectedDate = terrain.DateChangementEtat;
                }
                
            }
            
        }

        private void EditFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            Dossier dossier = Data.Entities.Dossiers.Find(_fileNumber);
            if (dossier != null)
            {
                dossier.DateDebut = StartDatePicker.SelectedDate.Value;
                Terrain terrain = Data.Entities.Terrains.Find(dossier.TerrainID);
                if (terrain != null)
                {
                    terrain.Etat = Etat.Text;
                    terrain.DateChangementEtat = DateChangementEtat.SelectedDate.Value;
                }

                
            }
            
        }
    }
}
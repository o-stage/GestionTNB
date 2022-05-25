using System.Windows;
using System.Windows.Controls;

namespace GestionTNB
{
    public partial class NewFile : DockPanel
    {
        private Dossier _newDossier;

        public NewFile()
        {
            InitializeComponent();
            NewDossier = new Dossier
            {
                Redevable = new Redevable(),
                Terrain = new Terrain()
            };
        }

        private Dossier NewDossier
        {
            get => _newDossier;
            set
            {
                _newDossier = value;
                FileGrid.DataContext = value;
            }
        }

        private void NewFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            NewDossier.Redevable = (Redevable)RedevableGrid.DataContext;
            NewDossier.Terrain = (Terrain)TerrainGrid.DataContext;
            Data.Entities.Dossiers.Add(NewDossier);
            Data.Entities.SaveChanges();
            NewDossier = new Dossier();
            RedevableGrid.DataContext = null;
            TerrainGrid.DataContext = null;
        }


        private void RedevableId_TextChanged(object sender, TextChangedEventArgs e)
        {
            var redevableId = (sender as TextBox).Text;
            NewDossier.Redevable.ID = redevableId;

            var redevable = Data.Entities.Redevables.Find(redevableId);
            if (redevable != null)
            {
                RedevableGrid.DataContext = redevable;
                RedevableGrid.IsEnabled = false;
            }
            else
            {
                RedevableGrid.DataContext = null;
                RedevableGrid.DataContext = NewDossier.Redevable;
                RedevableGrid.IsEnabled = true;
            }
        }

        private void NTF_TextChanged(object sender, TextChangedEventArgs e)
        {
            var ntf = (sender as TextBox)?.Text;
            NewDossier.Terrain.NTF = ntf;
            var terrain = Data.Entities.Terrains.Find(ntf);
            if (terrain != null)
            {
                TerrainGrid.DataContext = terrain;
                TerrainGrid.IsEnabled = false;
            }
            else
            {
                TerrainGrid.DataContext = null;
                TerrainGrid.DataContext = NewDossier.Terrain;
                TerrainGrid.IsEnabled = true;
            }
        }
    }
}
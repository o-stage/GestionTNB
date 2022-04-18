using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TaxesV1
{
    /// <summary>
    /// Logique d'interaction pour Fichier.xaml
    /// </summary>
    public partial class Fichier : DockPanel
    {
        public Fichier()
        {
            InitializeComponent();
            var ntf = Data.Entities.Terrains.Select(p => p.NTF);
            var idRedevable = Data.Entities.Redevables.Select(r => r.ID);
            ntfcombo.ItemsSource = ntf.ToList();
            
            idredevablecombo.ItemsSource = idRedevable.ToList();


        }

        void clearChamps()
        {
            NumDosstxt.Text = "";
            datedebutDatePicker.SelectedDate = DateTime.Now;
            datedossDatePicker.SelectedDate = DateTime.Now;
            idredevablecombo.SelectedIndex = 0;
            ntfcombo.SelectedIndex = 0;
        }

        private void Nouveau_Fichier_Button_Click(object sender, RoutedEventArgs e)
        {
            var doss = new Dossier();
            doss.NDossier = NumDosstxt.Text;
            doss.DateDebut = datedebutDatePicker.SelectedDate.Value;
            doss.DateDossier = datedossDatePicker.SelectedDate.Value;
            doss.RedevableId = idredevablecombo.Text;
            doss.TerrainID = ntfcombo.Text;
            Data.Entities.Dossiers.Add(doss);


            var redevabletest = Data.Entities.Redevables.Find(redavableId.Text);
            if (redevabletest == null)
            {
                Redevable redevable = new Redevable();

                redevable.ID = redavableId.Text;
                redevable.Prenom = redevableName.Text;
                redevable.Nom = redevableLastName.Text;
                redevable.Type= RedevableType.Text;
                redevable.Tel = Telephone.Text;

                Data.Entities.Redevables.Add(redevable);
            }

            var terraintest = Data.Entities.Terrains.Find(ntfcombo.SelectedItem.ToString());
            if (terraintest != null)
            {
                Terrain terrain = new Terrain();
                terrain.NTF = ntf.Text;
                terrain.Lieu = terrainlieu.Text;
                terrain.SuperficeBrute =double.Parse( terrainSuperBrute.Text);
                terrain.SuperficeTaxable = double.Parse(terrainSuperTaxable.Text);
                terrain.Etat = terrainEtat.Text ;
                terrain.Type = terrainType.Text;
                terrain.DateChangementEtat = terrainDateChangement.SelectedDate;
                Data.Entities.Terrains.Add(terrain);
            }


            Data.Entities.SaveChanges();
          
        }

        private void Rechercher_Fichier_Button_Click(object sender, RoutedEventArgs e)
        {
            var doss = Data.Entities.Dossiers.Find(NumDosstxt.Text);
            if (doss == null)
            {
                MessageBox.Show(Window.GetWindow(this),"Dossier Inatrouvable ");
                return;
            }

            datedebutDatePicker.SelectedDate = doss.DateDebut;
            datedossDatePicker.SelectedDate = doss.DateDossier;
            idredevablecombo.Text = doss.RedevableId;
            ntfcombo.Text = doss.TerrainID;
        }

        private void RedevableType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void redavableId_TextChanged(object sender, TextChangedEventArgs e)
        { 
        }
        
        private void idredevablecombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var redevable = Data.Entities.Redevables.Find(idredevablecombo.SelectedItem.ToString());
            if (redevable != null)
            {
                redavableId.Text = redevable.ID.ToString();
                redevableName.Text = redevable.Prenom.ToString();
                redevableLastName.Text = redevable.Nom.ToString();
                RedevableType.Text = redevable.Type.ToString();
                Telephone.Text = redevable.Tel.ToString();
            }
            else {

                redavableId.IsEnabled = true;
                redevableName.IsEnabled = true;
                redevableLastName.IsEnabled = true;
                RedevableType.IsEnabled = true;
                Telephone.IsEnabled = true;
            }
        }

        private void ntfcombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var terrain = Data.Entities.Terrains.Find(ntfcombo.SelectedItem.ToString());
            if (terrain != null)
            {
                ntf.Text = terrain.NTF.ToString();
                terrainlieu.Text = terrain.Lieu.ToString();
                terrainSuperBrute.Text = terrain.SuperficeBrute.ToString();
                terrainSuperTaxable.Text = terrain.SuperficeTaxable.ToString();
                terrainEtat.Text = terrain.Etat.ToString();
                terrainType.Text = terrain.Type.ToString();
                terrainDateChangement.SelectedDate = terrain.DateChangementEtat;
            }
        }
    }
}
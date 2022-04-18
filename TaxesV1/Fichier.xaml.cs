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
            Data.Entities.SaveChanges();
            clearChamps();
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
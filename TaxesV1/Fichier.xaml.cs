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
            ntfcombo.SelectedIndex = 0;
            idredevablecombo.SelectedIndex = 0;
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
                MessageBox.Show(Window.GetWindow(this), "Dossier Inatrouvable ");
                return;
            }

            DossierGrid.Visibility = Visibility.Visible;
            datedebutDatePicker.SelectedDate = doss.DateDebut;
            datedossDatePicker.SelectedDate = doss.DateDossier;
            idredevablecombo.Text = doss.RedevableId;
            ntfcombo.Text = doss.TerrainID;
        }
    }
}
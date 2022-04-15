using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaxesV1
{
    /// <summary>
    /// Logique d'interaction pour Fichier.xaml
    /// </summary>
    public partial class Fichier : DockPanel
    {
        private static  Fichier _instance;

        public static Fichier GetInstance()
        {
            if (_instance == null)
                _instance = new Fichier();


            return _instance;

        }
        private Fichier()
        {
            InitializeComponent();

            var ntf = Data.Entities.Terrains.Select(p => p.NTF);
            var idRedevable = Data.Entities.Redevables.Select(r => r.ID);
            ntfcombo.ItemsSource = ntf.ToList();
            idredevablecombo.ItemsSource = idRedevable.ToList();
            ntfcombo.SelectedIndex = 0;
            idredevablecombo.SelectedIndex = 0;
        }

        private void Nouveau_Fichier_Button_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show(Window.GetWindow(this),"aaaaaa");
            var doss = (from d in Data.Entities.Dossiers select d).FirstOrDefault();
            doss.NDossier = NumDosstxt.Text;
            doss.DateDebut = DateTime.Parse(datedebutDatePicker.SelectedDate.Value.ToString());
            doss.DateDossier = DateTime.Parse(datedossDatePicker.SelectedDate.Value.ToString());
            doss.RedevableId = idredevablecombo.Text;
            doss.TerrainID= ntfcombo.Text;
            Data.Entities.Dossiers.Add(doss);
            Data.Entities.SaveChanges();
        }
    }
}

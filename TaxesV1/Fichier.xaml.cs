using System;
using System.Collections.Generic;
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

        public static Fichier GetInstance() {
            if (_instance == null)
                _instance = new Fichier();
            return _instance;
        }

        private Fichier()
        {
            InitializeComponent();
        }
    }
}

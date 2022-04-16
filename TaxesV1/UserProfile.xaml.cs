using System.Windows.Controls;

namespace TaxesV1
{
    public partial class UserProfile : DockPanel
    {
        private static UserProfile _instance;


        private UserProfile()
        {
            InitializeComponent();
        }

        public static UserProfile GetInstance()
        {
            return _instance ?? (_instance = new UserProfile());
        }
    }
}
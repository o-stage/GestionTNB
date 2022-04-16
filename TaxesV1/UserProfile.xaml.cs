using System.Linq;
using System.Windows.Controls;

namespace TaxesV1
{
    public partial class UserProfile : DockPanel
    {
        private static UserProfile _instance;


        private UserProfile()
        {
            InitializeComponent();

            DatabasePrincipal principal = Data.Entities.Database
                .SqlQuery<DatabasePrincipal>("select * from sys.database_principals where sid= SUSER_SID()")
                .FirstOrDefault();

            if (principal.authentication_type == 2)
                ChangePasswordPanel.IsEnabled = true;
            Details.Text = principal.authentication_type_desc;
        }

        public static UserProfile GetInstance()
        {
            return _instance ?? (_instance = new UserProfile());
        }
    }
}
using System.Linq;
using System.Windows;

namespace TaxesV1
{
    public partial class ManageUser : Window
    {
        public ManageUser(string userName)
        {
            InitializeComponent();
            int isAdmin = Data.Entities.Database.SqlQuery<int>($"select IS_ROLEMEMBER ('db_owner','{userName}')")
                .FirstOrDefault();
            if (isAdmin == 1)
            {
                AdminTextBlock.Visibility = Visibility.Visible;
                IsEnabled = false;
            }

            UserName = userName;
            UserNameTextBlock.Text = UserName;
            UserPermissions permissions = new UserPermissions(userName);
            PermissionsDataGrid.ItemsSource = permissions.Tables;
        }

        private string UserName { get; set; }
    }
}
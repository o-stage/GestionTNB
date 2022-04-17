﻿using System.Linq;
using System.Windows.Controls;

namespace TaxesV1
{
    public partial class ManageUsers : DockPanel
    {
        public ManageUsers()
        {
            InitializeComponent();
            var data = Data.Entities.Database.SqlQuery<DatabasePrincipal>(
                "select * from sys.database_principals where authentication_type=2").ToList();
            Users.ItemsSource = data;
            Users.DataContext = data;
        }
    }
}
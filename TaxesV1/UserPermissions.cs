using System.Collections.Generic;
using System.Linq;

namespace TaxesV1
{
    public class UserPermissions
    {
        public UserPermissions(string userName)
        {
            UserName = userName;
            Tables = new List<Table>
            {
                new Table("Terrain", UserName),
                new Table("Declaration", UserName),
                new Table("Dossier", UserName),
                new Table("Redevable", UserName)
            };
        }

        public string UserName { get; set; }

        public List<Table> Tables { get; }

        public class Table
        {
            private Permission _changePermission;
            private Permission _deletePermission;

            private Permission _readPermission;
            private Permission _writePermission;
            public string UserName;

            internal Table(string tableName, string userName)
            {
                TableName = tableName;
                UserName = userName;
                _readPermission = new Permission("SELECT", tableName, userName);
                _writePermission = new Permission("INSERT", tableName, userName);
                _changePermission = new Permission("UPDATE", tableName, userName);
                _deletePermission = new Permission("DELETE", tableName, userName);
            }

            public string TableName { get; set; }

            public bool Read
            {
                get => _readPermission.GetValue();
                set
                {
                    if (value) _readPermission.Grant();
                    else _readPermission.Deny();
                }
            }

            public bool Write
            {
                get => _writePermission.GetValue();
                set
                {
                    if (value) _writePermission.Grant();
                    else _writePermission.Deny();
                }
            }

            public bool Change
            {
                get => _changePermission.GetValue();
                set
                {
                    if (value) _changePermission.Grant();
                    else _changePermission.Deny();
                }
            }

            public bool Delete
            {
                get => _deletePermission.GetValue();
                set
                {
                    if (value) _deletePermission.Grant();
                    else _deletePermission.Deny();
                }
            }
        }

        public class Permission
        {
            internal Permission(string permissionName, string tableName, string userName)
            {
                PermissionName = permissionName;
                TableName = tableName;
                UserName = userName;
            }

            public string PermissionName { private set; get; }

            public string TableName { private set; get; }
            public string UserName { private set; get; }

            public bool GetValue()
            {
                var x = Data.Entities.Database.SqlQuery<int>(
                    $@"declare @t table(table_qualifier varchar(50),table_owner varchar(50), table_name varchar(50),grantor varchar(50),grantee varchar(50),  privilage varchar(50), is_gantable varchar(50) )
insert @t exec sp_table_privileges @table_name = '{TableName}';
SELECT COUNT(*) from @t where grantee='{UserName}' and privilage='{PermissionName}'").First();
                return x == 1;
            }

            public void Grant()
            {
                Data.Entities.Database.ExecuteSqlCommand(
                    $"GRANT {PermissionName} ON {TableName} TO {UserName} AS dbo");
            }

            public void Deny()
            {
                Data.Entities.Database.ExecuteSqlCommand(
                    $"DENY {PermissionName} ON {TableName} TO {UserName} AS dbo");
            }

            public void Revoke()
            {
                Data.Entities.Database.ExecuteSqlCommand(
                    $"REVOKE {PermissionName} ON {TableName} TO {UserName} AS dbo");
            }
        }
    }
}
using System;

namespace GestionTNB
{
    public class DatabasePrincipal
    {
        public string name { get; set; }
        public int principal_id { get; set; }
        public char type { get; set; }
        public string type_desc { get; set; }
        public string default_schema_name { get; set; }
        public DateTime create_date { get; set; }
        public DateTime modify_date { get; set; }
        public int? owning_principal_id { get; set; }
        public byte[] sid { get; set; }
        public bool is_fixed_role { get; set; }
        public int authentication_type { get; set; }
        public string authentication_type_desc { get; set; }
        public string default_language_name { get; set; }
        public int? default_language_lcid { get; set; }
        public bool allow_encrypted_value_modifications { get; set; }
    }
}
using System.Text;

namespace MiniER.Model
{
    /// <summary>
    /// Field in table
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Table, that contains this field
        /// </summary>
        public Table Table;

        /// <summary>
        /// Use only for save and loading
        /// </summary>
        public int fieldUniqueId = 0;

        /// <summary>
        /// Field name
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Field type
        /// </summary>
        public DataType DataType { get; set; }

        /// <summary>
        /// Field size
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Is it primary key
        /// </summary>
        public bool IsPK { get; set; }

        /// <summary>
        /// Is it counter (autoincrement)
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// Can be NULL
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Default value
        /// </summary>
        public string DefaultValue { get; set; }

        public string GetFieldName(SettingsDTO settings)
        {
            if (settings.ShowDataType)
                return this.ToString();
            return this.FieldName;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(FieldName);
            sb.Append(" ");
            sb.Append(DataType);
            if (IsNullable)
                sb.Append(" NULL ");
            else
                sb.Append(" NOT NULL ");
            if (IsPK) sb.Append("PRIMARY KEY ");
            if (IsIdentity) sb.Append("IDENTITY ");
            if (!string.IsNullOrEmpty(DefaultValue))
            {
                sb.Append("DEFAULT");
                sb.Append(DefaultValue);
            }
            return sb.ToString();
        }
    }
}

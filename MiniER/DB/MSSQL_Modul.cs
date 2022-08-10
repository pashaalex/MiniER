using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;
using MiniER.Model;

namespace MiniER.DB
{
    public partial class MSSQL_Modul : UserControl, IDBModul
    {
        public MSSQL_Modul()
        {
            InitializeComponent();
        }

        public string DBType { get { return "MS SQL"; } }

        public string ConnectionString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<string, string> p in ConnDict)
                {
                    sb.Append(p.Key);
                    sb.Append("=");
                    sb.Append(p.Value);
                    sb.Append(";");
                }
                return sb.ToString();
            }
            set
            {
                textBox2.Text = value;
                foreach (string s in value.Split(';'))
                {
                    string[] ss = s.Split('=');
                    if (ss.Length > 1)
                        ConnDict.Add(ss[0], ss[1]);
                }
            }
        }

        Dictionary<string, string> ConnDict = new Dictionary<string, string>();

        protected static DataTable _SqlDataSources = null;

        protected List<string> GetDataSources()
        {
            if (_SqlDataSources == null)
                _SqlDataSources = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources();
            List<string> l = new List<string>();
            foreach (DataRow dr in _SqlDataSources.Rows)
                if (dr[1].ToString().Trim().Length > 0)
                    l.Add(dr[0].ToString() + "\\" + dr[1].ToString());
                else
                    l.Add(dr[0].ToString());
            l.Sort();
            return l;
        }

        private void DatabaseEdit_Load(object sender, EventArgs e)
        {
            processChange = true;

            if (ConnDict.ContainsKey("Data Source"))
                cb_serverName.Text = ConnDict["Data Source"];

            if (ConnDict.ContainsKey("Initial Catalog"))
                cb_database.Text = ConnDict["Initial Catalog"];


            if ((ConnDict.ContainsKey("Integrated Security")) && (ConnDict["Integrated Security"] == "True"))
            {
                tb_login.Enabled = false;
                tb_password.Enabled = false;
                rb1.Checked = true;
            }
            else
            {
                tb_login.Enabled = true;
                tb_password.Enabled = true;
                rb2.Checked = true;
                if (ConnDict.ContainsKey("User ID")) tb_login.Text = ConnDict["User ID"];
                if (ConnDict.ContainsKey("Password")) tb_password.Text = ConnDict["Password"];
            }
            
            

            processChange = false;
        }

        bool processChange = false;

        private void cb_serverName_TextChanged(object sender, EventArgs e)
        {
            if (!processChange)
            {
                ConnDict["Data Source"] = cb_serverName.Text;
                textBox2.Text = ConnectionString;
            }
        }

        private List<string> GetDatabases()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlDataAdapter sda = new SqlDataAdapter("exec sp_helpdb", conn);
                sda.Fill(dtbl);
            }
            return dtbl.Select().Select(n => n["name"].ToString()).ToList();
        }

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            if (!processChange)
            {
                if (rb1.Checked)
                {
                    // Windows
                    ConnDict.Remove("Persist Security Info");
                    ConnDict.Remove("User ID");
                    ConnDict.Remove("Password");
                    ConnDict["Integrated Security"] = "True";
                    tb_login.Enabled = false;
                    tb_password.Enabled = false;
                }
                else
                {
                    // SQL
                    ConnDict.Remove("Integrated Security");
                    tb_login.Enabled = true;
                    tb_password.Enabled = true;
                }
                textBox2.Text = ConnectionString;
            }
        }

        private void tb_login_TextChanged(object sender, EventArgs e)
        {
            ConnDict["User ID"] = tb_login.Text;
            textBox2.Text = ConnectionString;
        }

        private void tb_password_TextChanged(object sender, EventArgs e)
        {
            ConnDict["Password"] = tb_password.Text;
            textBox2.Text = ConnectionString;
        }

        private void cb_database_DropDown(object sender, EventArgs e)
        {
            if (cb_database.DataSource == null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    cb_database.DataSource = GetDatabases();
                    textBox2.Text = ConnectionString;
                }
                catch
                { }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void cb_serverName_DropDown(object sender, EventArgs e)
        {
            processChange = true;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                cb_serverName.DataSource = GetDataSources();
                if (ConnDict.ContainsKey("Data Source"))
                    cb_serverName.SelectedText = ConnDict["Data Source"];

                textBox2.Text = ConnectionString;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            processChange = false;
        }

        private void cb_database_TextChanged(object sender, EventArgs e)
        {
            if (!processChange)
                ConnDict["Initial Catalog"] = cb_database.Text;
            textBox2.Text = ConnectionString;
        }

        public Model.DataSchema GetSchema()
        {
            Model.DataSchema res = new Model.DataSchema();           

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                List<FieldInfo> identityList = GerAllIdentity(conn);

                #region Получаем таблицы
                foreach (DataRow drTable in conn.GetSchema("Tables").Select("TABLE_TYPE='BASE TABLE'", "TABLE_NAME"))
                {
                    Table tv = new Table();
                    res.TableList.Add(tv);
                    tv.TableName = drTable["TABLE_NAME"].ToString();
                    tv.TableSchema = drTable["TABLE_SCHEMA"].ToString();

                    string[] restrictions = new string[4] { null, null, tv.TableName, null };
                    List<string> columns = new List<string>();
                    List<string> PKs = GetPrimaryKeysForTable(tv.TableName, conn);

                    foreach (DataRow dr in conn.GetSchema("Columns", restrictions).Select("", "ORDINAL_POSITION"))
                    {
                        Field tf = tv.NewField();
                        tf.FieldName = dr["COLUMN_NAME"].ToString();
                        if (PKs.Contains(tf.FieldName)) tf.IsPK = true;                        
                        switch (dr["DATA_TYPE"].ToString().ToLower())
                        { 
                            case "nchar":
                            case "char":
                            case "varchar":
                            case "nvarchar":
                                tf.DataType = DataType.STRING;
                                break;
                            case "numeric":
                            case "int":
                            case "bigint":
                            case "tinyint":
                            case "smallint":
                                tf.DataType = DataType.INT;
                                break;
                            case "text":
                            case "ntext":
                                tf.DataType = DataType.TEXT;
                                break;
                            case "image":
                            case "binary":
                            case "varbinary":
                                tf.DataType = DataType.IMAGE;
                                break;
                            case "datetime":
                            case "datetime2":
                            case "smalldatetime":
                            case "date":
                                tf.DataType = DataType.DATETIME;
                                break;
                            case "bit":
                                tf.DataType = DataType.BOOL;
                                break;
                            case "float":
                            case "money":
                            case "real":
                            case "smallmoney":
                                tf.DataType = DataType.DOUBLE;
                                break;
                            case "uniqueidentifier":
                                tf.DataType = DataType.GUID;
                                break;
                            default:
                                tf.DataType = DataType.STRING; 
                                break;
                        }

                        if (identityList.Any(n => n.TableName == tv.TableName && n.SchemaName == tv.TableSchema && n.FieldName == tf.FieldName))
                            tf.IsIdentity = true;
                        if (!dr.IsNull("CHARACTER_MAXIMUM_LENGTH"))
                        {
                            int i = 0;
                            if (int.TryParse(dr["CHARACTER_MAXIMUM_LENGTH"].ToString(), out i))
                                tf.Size = i;
                        }
                        if (dr["IS_NULLABLE"].ToString() == "YES")
                            tf.IsNullable = true;
                        else
                            tf.IsNullable = false;
                        if (!dr.IsNull("COLUMN_DEFAULT")) tf.DefaultValue = dr["COLUMN_DEFAULT"].ToString();
                    }
                }
                #endregion

                // Links
                foreach (var r in GetRelations(conn))
                {
                    res.RelationList.Add(new Relation()
                    {
                        field1 = res
                        .TableList
                        .First(n => n.TableName == r.ParentTable)
                        .Fields
                        .First(n => n.FieldName == r.ParentFK),

                        field2 = res
                        .TableList
                        .First(n => n.TableName == r.ReferencedTable)
                        .Fields
                        .First(n => n.FieldName == r.ReferencedFK)
                    });
                }
            }
                
            return res;
        }

        protected List<string> GetPrimaryKeysForTable(string tableName, SqlConnection conn)
        {
            DataTable dtbl = GetDataTable(@"SELECT Constraints.CONSTRAINT_NAME, Constraints.TABLE_NAME, COLUMN_NAME, ORDINAL_POSITION
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS Constraints
JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS Keys 
 ON Constraints.CONSTRAINT_CATALOG = Keys.CONSTRAINT_CATALOG AND
  Constraints.CONSTRAINT_SCHEMA = Keys.CONSTRAINT_SCHEMA AND
  Constraints.CONSTRAINT_NAME = Keys.CONSTRAINT_NAME 
WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND
 Constraints.TABLE_NAME = '" + tableName + "'", conn);
            List<string> l = new List<string>();
            foreach (DataRow dr in dtbl.Rows)
                l.Add(dr["COLUMN_NAME"].ToString());
            return l;
        }

        protected class FieldInfo
        {
            public string TableName { get; set; }
            public string SchemaName { get; set; }
            public string FieldName { get; set; }
        }

        /// <summary>
        /// Get all PrimaryKey for all tables
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        protected List<FieldInfo> GetAllPrimaryKeys(SqlConnection conn)
        {
            DataTable dtbl = GetDataTable(@"SELECT Constraints.CONSTRAINT_NAME, Constraints.TABLE_NAME, Constraints.TABLE_SCHEMA, COLUMN_NAME, ORDINAL_POSITION
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS Constraints
JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS Keys 
ON Constraints.CONSTRAINT_CATALOG = Keys.CONSTRAINT_CATALOG AND
  Constraints.CONSTRAINT_SCHEMA = Keys.CONSTRAINT_SCHEMA AND
  Constraints.CONSTRAINT_NAME = Keys.CONSTRAINT_NAME 
WHERE CONSTRAINT_TYPE = 'PRIMARY KEY'", conn);

            List<FieldInfo> list = new List<FieldInfo>();
            foreach (DataRow dr in dtbl.Rows)
                list.Add(new FieldInfo()
                {
                    FieldName = dr["COLUMN_NAME"].ToString(),
                    SchemaName = dr["TABLE_SCHEMA"].ToString(),
                    TableName = dr["TABLE_NAME"].ToString()
                });

            return list;
        }

        /// <summary>
        /// Get ALL autoincrement field from DB
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        protected List<FieldInfo> GerAllIdentity(SqlConnection conn)
        {
            DataTable dtbl = GetDataTable(@"SELECT SCHEMA_NAME( OBJECTPROPERTY( OBJECT_ID, 'SCHEMAID' )) AS SCHEMA_NAME,
      OBJECT_NAME( OBJECT_ID ) AS TABLE_NAME,
      NAME AS COLUMN_NAME
FROM  SYS.COLUMNS
WHERE COLUMNPROPERTY(OBJECT_ID, NAME, 'IsIdentity') = 1", conn);

            List<FieldInfo> list = new List<FieldInfo>();
            foreach (DataRow dr in dtbl.Rows)
                list.Add(new FieldInfo()
                {
                    FieldName = dr["COLUMN_NAME"].ToString(),
                    SchemaName = dr["SCHEMA_NAME"].ToString(),
                    TableName = dr["TABLE_NAME"].ToString()
                });

            return list;
        }

        protected class RelationsInfo
        {
            public string ParentTable;
            public string ParentFK;
            public string ReferencedTable;
            public string ReferencedFK;
        }

        protected List<RelationsInfo> GetRelations(SqlConnection conn)
        {
            DataTable dtbl = GetDataTable(@"SELECT tablesA.name AS ParentTable, columnsA.name AS ParentPK, tablesB.name AS ReferencedTable, columnsB.name AS ReferencedFK
FROM sys.foreign_key_columns 
INNER JOIN sys.columns AS columnsA 
ON sys.foreign_key_columns.parent_object_id = columnsA.object_id
AND sys.foreign_key_columns.parent_column_id = columnsA.column_id
INNER JOIN sys.columns AS columnsB
ON sys.foreign_key_columns.referenced_object_id = columnsB.object_id
AND sys.foreign_key_columns.referenced_column_id = columnsB.column_id
INNER JOIN sys.tables AS tablesA
ON sys.foreign_key_columns.parent_object_id = tablesA.object_id
INNER JOIN sys.tables AS tablesB 
ON sys.foreign_key_columns.referenced_object_id = tablesB.object_id", conn);

            List<RelationsInfo> list = new List<RelationsInfo>();
            foreach (DataRow dr in dtbl.Rows)
                list.Add(new RelationsInfo()
                {
                    ParentFK = dr["ParentPK"].ToString(),
                    ParentTable = dr["ParentTable"].ToString(),
                    ReferencedFK = dr["ReferencedFK"].ToString(),
                    ReferencedTable = dr["ReferencedTable"].ToString()
                });

            return list;
        }

        protected DataTable GetDataTable(string sqlQuery, SqlConnection conn)
        {
            SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, conn);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            return dtbl;
        }
    }
}

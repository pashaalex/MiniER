using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// using Npgsql;
using MiniER.Model;
/*
namespace MiniER.DB
{
    public partial class Postgre_Modul : UserControl, IDBModul
    {
        public Postgre_Modul()
        {
            InitializeComponent();
        }

        public string DBType
        {
            get { return "Postgre SQL"; }
        }

        public string ConnectionString
        {
            get {
                string dbName = (cb_database.Text == "") ? "postgres" : cb_database.Text;
                return string.Format("Server={0};Port={1};Database={2};User Id={3};Password={4};",
                    tb_server.Text, tb_port.Text, dbName, tb_login.Text, tb_password.Text); 
            }
        }

        public Model.DataSchema GetSchema()
        {
            Model.DataSchema res = new Model.DataSchema();
            try
            {
                this.ParentForm.Cursor = Cursors.WaitCursor;

                foreach (DataRow dr in GetTable("SELECT table_schema,table_name FROM information_schema.tables where table_schema='public';").Rows)
                    res.TableList.Add(new Model.Table() { TableName = dr[1].ToString(), TableSchema = dr[0].ToString() });

                #region Поля
                foreach (var tb in res.TableList)
                {
                    foreach (DataRow dr in GetTable("SELECT column_name, column_default, is_nullable, data_type FROM information_schema.columns WHERE table_name = '" + tb.TableName + "'").Rows)
                    {
                        Model.Field fld = tb.NewField();                        
                        fld.FieldName = dr[0].ToString();
                        if (!dr.IsNull(1)) fld.DefaultValue = dr[1].ToString();
                        fld.IsNullable = (dr[2].ToString() == "YES");
                        switch (dr[3].ToString())
                        {
                            case "character varyng":
                            case "char":
                            case "varchar":
                            case "nvarchar":
                                fld.DataType = Model.DataType.STRING;
                                break;
                            case "bigint":
                            case "integer":
                            case "smallint":
                                fld.DataType = Model.DataType.INT;
                                break;
                            case "text":
                            case "ntext":
                                fld.DataType = Model.DataType.TEXT;
                                break;
                            case "image":
                            case "binary":
                            case "varbinary":
                                fld.DataType = Model.DataType.IMAGE;
                                break;
                            case "datetime":
                            case "date":
                                fld.DataType = Model.DataType.DATETIME;
                                break;
                            case "bit":
                                fld.DataType = Model.DataType.BOOL;
                                break;
                            case "real":
                                fld.DataType = Model.DataType.DOUBLE;
                                break;
                            case "uniqueidentifier":
                                fld.DataType = Model.DataType.GUID;
                                break;
                            default:
                                fld.DataType = Model.DataType.STRING; 
                                break;
                        }
                    }
                }
                #endregion

                // Primary Keys
                foreach (DataRow dr in GetTable(@"select tc.table_name, kc.column_name
from 
    information_schema.table_constraints tc
    join information_schema.key_column_usage kc 
        on kc.table_name = tc.table_name and kc.table_schema = tc.table_schema
where 
    tc.constraint_type = 'PRIMARY KEY'").Rows)
                {
                    Field f = res.TableList.FirstOrDefault(n => n.TableName == dr[0].ToString())?.Fields.FirstOrDefault(n => n.FieldName == dr[1].ToString());
                    if (f != null) f.IsPK = true;
                }

                
                // Relations
                foreach (DataRow dr in GetTable(@"SELECT
    tc.table_name, kcu.column_name, 
    ccu.table_name AS foreign_table_name,
    ccu.column_name AS foreign_column_name 
FROM 
    information_schema.table_constraints AS tc 
    JOIN information_schema.key_column_usage AS kcu
      ON tc.constraint_name = kcu.constraint_name
    JOIN information_schema.constraint_column_usage AS ccu
      ON ccu.constraint_name = tc.constraint_name
WHERE constraint_type = 'FOREIGN KEY'").Rows)
                {
                    Model.Relation rel = new Model.Relation();
                    rel.field1 = res.TableList.First(n => n.TableName == dr[0].ToString()).Fields.First(n => n.FieldName == dr[1].ToString());
                    rel.field2 = res.TableList.First(n => n.TableName == dr[2].ToString()).Fields.First(n => n.FieldName == dr[3].ToString());
                    res.RelationList.Add(rel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
            }
            finally
            {
                this.ParentForm.Cursor = Cursors.Default;
            }

            return res;
        }

        private void cb_database_DropDown(object sender, EventArgs e)
        {
            if (cb_database.DataSource != null) return;
            try
            {
                this.ParentForm.Cursor = Cursors.WaitCursor;
                cb_database.DataSource = GetTable("SELECT datname FROM pg_database WHERE datistemplate = false;");
                cb_database.DisplayMember = "datname";
            }
            finally
            {
                this.ParentForm.Cursor = Cursors.Default;
            }
        }

        protected DataTable GetTable(string sql)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(ConnectionString))
            {            
                DataTable dtbl = new DataTable();
                NpgsqlDataAdapter sda = new NpgsqlDataAdapter(sql, conn);
                sda.Fill(dtbl);
                return dtbl;                
            }
        }
    }
}
*/
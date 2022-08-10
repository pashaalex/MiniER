using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MiniER
{
    public partial class Reverse : Form
    {
        public Model.DataSchema Schema { get; set; }

        List<DB.IDBModul> _DBModules = null;
        DB.IDBModul curDBModul = null;

        ListBox lb_tables = null;

        public List<DB.IDBModul> DBModules
        {
            set
            {
                _DBModules = value;
                ddl_engine.DataSource = _DBModules.Select(n => n.DBType).ToArray();
            }
        }
        public Reverse()
        {
            InitializeComponent();            
        }

        private void ddl_engine_SelectedIndexChanged(object sender, EventArgs e)
        {
            pn_db.Controls.Clear();
            curDBModul = null;
            var c = _DBModules.FirstOrDefault(n => n.DBType == ddl_engine.SelectedValue.ToString());
            if (c != null)
            {
                pn_db.Controls.Add(c as UserControl);
                curDBModul = c as DB.IDBModul;
            }
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Schema = curDBModul.GetSchema();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            pn_connection.Visible = false;
            btn_Next.Visible = false;
            btn_OK.Visible = true;

            Panel p = new Panel();
            this.Controls.Add(p);
            p.Top = 2;
            p.Left = 2;
            p.Width = 400;
            p.Height = 300;

            Label lb = new Label();
            p.Controls.Add(lb);
            lb.Text = "Select table names for display:";
            lb.Dock = DockStyle.Top;

            lb_tables = new ListBox();
            p.Controls.Add(lb_tables);
            lb_tables.Dock = DockStyle.Fill;
            lb_tables.BringToFront();
            lb_tables.SelectionMode = SelectionMode.MultiExtended;

            Application.DoEvents();

            foreach (string s in Schema.TableList.Select(n => n.TableName))
                lb_tables.Items.Add(s);

            for (int i = 0; i < lb_tables.Items.Count; i++)            
                lb_tables.SetSelected(i, true);            
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {            
            for(int i = 0; i < lb_tables.Items.Count; i++)
                if (!lb_tables.SelectedIndices.Contains(i))
                {
                    var tb = Schema.TableList.First(n => n.TableName == lb_tables.Items[i].ToString());
                    Schema.RelationList.RemoveAll(n => n.field1.Table == tb);
                    Schema.RelationList.RemoveAll(n => n.field2.Table == tb);
                    Schema.TableList.Remove(tb);
                }

            // Layout tables
            int tblCnt = 0;
            int y = 10;
            int x = 10;
            int yMax = 0;
            foreach (var tv in Schema.TableList)
            {                
                tv.Top = y;
                tv.Left = x;

                SizeF sz = tv.GetSize(SettingsDTO.GetSettings());

                x += (int)sz.Width + 30;
                if (y + (int)sz.Height > yMax) yMax = y + (int)sz.Height;

                if ((++tblCnt) % 8 == 0) // 8 tables per row
                {
                    x = 10;
                    y = yMax + 10;
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}

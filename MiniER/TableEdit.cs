using System;
using System.Windows.Forms;

namespace MiniER
{
    public partial class TableEdit : Form
    {
        public TableEdit()
        {
            InitializeComponent();
        }

        public DialogResult Edit(Model.Table obj)
        {
            dg_tableFields.AutoGenerateColumns = false;
            DataType.DataSource = Enum.GetValues(typeof(Model.DataType));

            dg_tableFields.DataSource = (obj as Model.Table).FieldSource;

            tb_tableName.Enabled = true;
            tb_tableName.DataBindings.Clear();
            tb_tableName.DataBindings.Add("Text", (obj as Model.Table), "TableName");
            return this.ShowDialog();
        }

        private void TableEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            dg_tableFields.DataSource = null;
            tb_tableName.DataBindings.Clear();
            tb_tableName.Text = "";
            tb_tableName.Enabled = false;
        }

        private void dg_tableFields_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dg_tableFields.CurrentCell.ColumnIndex == 2) 
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)                
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);                
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-')) e.Handled = true;
        }
    }
}

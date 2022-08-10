using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using MiniER.Model;

namespace MiniER
{
    public partial class MainForm : Form
    {
        List<DB.IDBModul> DBModules = null;
        List<OpenSave.ILoad> Openers = null;
        List<OpenSave.ISave> Savers = null;
        
        public MainForm()
        {
            InitializeComponent();            

            DBModules = GetObjects<DB.IDBModul>();
            Openers = GetObjects<OpenSave.ILoad>();
            Savers = GetObjects<OpenSave.ISave>();

            dialog_open.Filter = string.Join("|", Openers.Select(n => n.GetExtension()).ToArray());
            dialog_open.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);

            dialog_save.Filter = string.Join("|", Savers.Select(n => n.GetExtension()).ToArray());
            dialog_save.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
        }

        protected List<T> GetObjects<T>()
        {
            Type tp = typeof(T);

            return System.Reflection.Assembly.GetAssembly(this.GetType())
                .GetTypes()
                .Where(n => tp.IsAssignableFrom(n) && !n.IsInterface)
                .Select(n => (T)Activator.CreateInstance(n))
                .ToList();            
        }

        private void editPanel1_OnEditTable(Table obj)
        {
            var t = new TableEdit();
            t.Edit(obj);
            editPanel1.Invalidate();
        }

        private void btn_addTable_Click(object sender, EventArgs e)
        {
            Table tv = editPanel1.NewTable();
            tv.Top = 10;
            tv.Left = 10;
            var t = new TableEdit();
            t.Edit(tv);            
            editPanel1.Invalidate();
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            if (dialog_open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { 
                System.IO.FileInfo fi = new System.IO.FileInfo(dialog_open.FileName);
                var o = Openers.FirstOrDefault(n => n.IsMyExtension(fi.Extension));
                if (o == null)
                {
                    MessageBox.Show("Unrecognized file format");
                    return;
                }
                editPanel1.Schema = o.Open(fi.FullName);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (dialog_save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(dialog_save.FileName);
                var o = Savers.FirstOrDefault(n => n.IsMyExtension(fi.Extension));
                if (o == null)
                {
                    MessageBox.Show("Unrecognized file format");
                    return;
                }                
                o.Save(editPanel1.Schema, fi.FullName);
            }
        }

        private void btn_reverse_Click(object sender, EventArgs e)
        {
            Reverse r = new Reverse();
            r.DBModules = this.DBModules;
            if (r.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                editPanel1.Schema = r.Schema;
            }
        }

        private void btn_settings_Click(object sender, EventArgs e)
        {
            var f = new SettingsForm();
            f.Settings = SettingsDTO.GetSettings();
            if (f.ShowDialog() == DialogResult.OK)
            {
                SettingsDTO.SetSettings(f.Settings);
                editPanel1.Invalidate();
            }
        }
    }
}

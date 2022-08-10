using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using MiniER.Model;

namespace MiniER.Controls
{
    public partial class EditPanel : UserControl
    {
        public EditPanel()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            Schema = new DataSchema();            
            InitializeComponent();
        }

        SettingsDTO Settings { get { return SettingsDTO.GetSettings(); } }

        Relation selectedRelation;
        Table selectedTable;
        Table movingTable = null;
        Field movingField = null;
        int _x = 0;
        int _y = 0;

        public delegate void SelectObject(IDrawable obj);
        public event SelectObject OnSelectObject;

        public delegate void EditTable(Table obj);
        public event EditTable OnEditTable;

        protected DataSchema _schema = null;

        public DataSchema Schema {
            get { return _schema; }
            set
            {
                _schema = value;                
                Invalidate();
                ReCalcSize();
            }
        }

        /// <summary>
        /// Create new table
        /// </summary>
        /// <returns></returns>
        public Table NewTable()
        {
            Table tv = new Table();
            Schema.TableList.Add(tv);
            tv.TableName = "Table" + Schema.TableList.Count;
            return tv;
        }

        protected void ReCalcSize()
        {
            if (Schema == null) return;
            if (hScrollBar1 == null) return; // Call from visual studio designer

            int xMax = 0;
            int yMax = 0;
            foreach (Table tv in Schema.TableList)
            {
                if (tv.Left + tv.GetSize(Settings).Width > xMax) xMax = (int)(tv.Left + tv.GetSize(Settings).Width);
                if (tv.Top + tv.GetSize(Settings).Height > yMax) yMax = (int)(tv.Top + tv.GetSize(Settings).Height);
            }
            xMax += 10;
            yMax += 10;

            if (xMax > this.Width)
            {
                hScrollBar1.Visible = true;
                hScrollBar1.LargeChange = this.Width;
                hScrollBar1.Minimum = 0;
                hScrollBar1.Maximum = xMax;
            }
            else
            {
                hScrollBar1.Value = 0;
                hScrollBar1.Visible = false;
            }

            if (yMax > this.Height)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.LargeChange = this.Height;
                vScrollBar1.Minimum = 0;
                vScrollBar1.Maximum = yMax;
            }
            else
            {
                vScrollBar1.Value = 0;
                vScrollBar1.Visible = false;
            }
        }

        private void EditPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectedTable != null)
            {
                selectedTable.IsSelected = false;
                selectedTable = null;
                Invalidate();
            }

            if (selectedRelation != null)
            {
                selectedRelation.IsSelected = false;
                selectedRelation = null;
                Invalidate();
            }

            if (OnSelectObject != null) OnSelectObject(null);

            _x = e.X + hScrollBar1.Value;
            _y = e.Y + vScrollBar1.Value;

            foreach (Relation r in Schema.RelationList)
                if (r == r.MouseGet(_x, _y, Settings))
                {
                    selectedRelation = r;
                    r.IsSelected = true;
                    if (OnSelectObject != null) OnSelectObject(r);
                    Invalidate();
                    return;
                }

            movingTable = null;
            for (int i =  Schema.TableList.Count - 1; i >= 0; i--)
            {
                Table t = Schema.TableList[i];
                object o = t.MouseGet(_x, _y, Settings);
                movingTable = (o as Table);
                movingField = (o as Field);
                if (o != null) break;
            }

            if (movingTable != null)
            {
                selectedTable = movingTable;
                selectedTable.IsSelected = true;
                if (OnSelectObject != null) OnSelectObject(selectedTable);
                Invalidate();
            }
            _x = e.X;
            _y = e.Y;
        }

        private void MoveStop()
        {
            _x -= -hScrollBar1.Value;
            _y -= vScrollBar1.Value;

            if (movingField != null)
            {
                Field tf = null;
                foreach (Table t in Schema.TableList)
                {
                    tf = t.MouseGet(_x, _y, Settings) as Field;
                    if (tf != null) break;
                }
                if (tf != null)
                {
                    if (tf == movingField)
                    {
                        // try to connect with same field
                        // MessageBox.Show("Impossible to link field with themself");
                        // IGNORE
                    }
                    else
                    {
                        // Check the link already exists
                        if (Schema.RelationList.Any(n => (n.field1 == tf) && (n.field2 == movingField)) ||
                            Schema.RelationList.Any(n => (n.field2 == tf) && (n.field1 == movingField)))
                        {
                            // MessageBox.Show("Link already exists");
                            // IGNORE
                        }
                        else
                        {
                            // Create link
                            Relation tr = new Relation();
                            tr.field1 = tf;
                            tr.field2 = movingField;
                            Schema.RelationList.Add(tr);
                        }
                    }
                }
            }
            movingField = null;
            movingTable = null;
            ReCalcSize();
            Invalidate();
        }

        private void EditPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (movingTable != null)
            {
                movingTable.Left -= (_x - e.X);
                movingTable.Top -= (_y - e.Y);
                _x = e.X;
                _y = e.Y;
                Invalidate();
            }

            if (movingField != null)
            {
                _x = e.X;
                _y = e.Y;
                Invalidate();
            }
        }

        private void EditPanel_MouseLeave(object sender, EventArgs e)
        {
            MoveStop();
        }

        private void EditPanel_MouseUp(object sender, MouseEventArgs e)
        {
            _x = e.X;
            _y = e.Y;
            MoveStop();
        }

        private void EditPanel_Paint(object sender, PaintEventArgs e)
        {
            if (Schema == null) return;

            try
            {
                Graphics g = e.Graphics;
                g.TranslateTransform(-hScrollBar1.Value, -vScrollBar1.Value);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Draw links
                foreach (Relation r in Schema.RelationList)
                    r.Draw(g, Settings);

                // Draw tables
                foreach (Table t in Schema.TableList)
                    t.Draw(g, Settings);

                g.TranslateTransform(hScrollBar1.Value, vScrollBar1.Value);
                if (movingField != null)
                {
                    // Moving table field
                    int w = (int)movingField.Table.GetSize(Settings).Width;
                    Rectangle r = new Rectangle(_x - w / 2, _y - (int)Table.rowHeight / 2, w, (int)Table.rowHeight);
                    g.FillRectangle(Brushes.White, r);
                    g.DrawRectangle(Pens.Black, r);
                    g.DrawString(movingField.FieldName, Table.font, Brushes.Black, r);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EditPanel_KeyDown(object sender, KeyEventArgs e)
        {
            #region Remove links
            if ((e.KeyCode == Keys.Delete) && (selectedRelation != null))
            {
                if (MessageBox.Show("Do you really want to remove link?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Schema.RelationList.Remove(selectedRelation);
                    selectedRelation = null;
                    Invalidate();
                }
            }
            #endregion

            #region Remove table
            if ((e.KeyCode == Keys.Delete) && (selectedTable != null))
            {
                if (MessageBox.Show("Do you really want to delete Table?", "Confiramtion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Remove links
                    Schema.RelationList.RemoveAll(n => selectedTable.FieldSource.Contains(n.field1) || selectedTable.FieldSource.Contains(n.field2));

                    // Remove table
                    Schema.TableList.Remove(selectedTable);
                    selectedTable = null;
                    Invalidate();
                }
            }
            #endregion
        }

        private void EditPanel_Resize(object sender, EventArgs e)
        {
            ReCalcSize();
        }

        private void ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void EditPanel_DoubleClick(object sender, EventArgs e)
        {            
            for (int i = Schema.TableList.Count - 1; i >= 0; i--)
            {
                var t = (Schema.TableList[i].MouseGet(_x, _y, Settings) as Table);
                if ((t != null) && (OnEditTable != null))
                {
                    OnEditTable(t);
                    return;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;

namespace MiniER.Model
{
    public class Table : IDrawable
    {
        public int Left { get; set; }
        public int Top { get; set; }

        public string TableName { get; set; }
        public string TableSchema { get; set; }

        public Table()
        {
            Fields = new List<Field>();
            FieldSource = new FieldBindingSource(this, Fields); 
        }

        /// <summary>
        /// Create new field in table
        /// </summary>
        /// <returns></returns>
        public Field NewField()
        {
            return (Field)FieldSource.AddNew();
        }

        /// <summary>
        /// Binding for fields
        /// </summary>
        public FieldBindingSource FieldSource { get; set; }

        public List<Field> Fields { get; set; }
        
        public bool IsSelected { get; set; }

        /// <summary>
        /// Row height (calculates automaticaly after call GetSize)
        /// </summary>
        public static float rowHeight = 15;
        
        static Image fakeImage = new Bitmap(1, 1);

        public static Font font = new Font("Courier New", 10);
        public static Font fontBold = new Font("Courier New", 10, FontStyle.Bold);

        protected string GetTableName(SettingsDTO settings)
        {
            if (settings.ShowSchema)
                return TableSchema + "." + TableName;
            return TableName;
        }

        public SizeF GetSize(SettingsDTO settings)
        {
            float xMax = 0;
            using (Graphics g = Graphics.FromImage(fakeImage))
            {
                SizeF s = g.MeasureString(GetTableName(settings), font);
                if (s.Width > xMax) xMax = s.Width;
                foreach (string str in Fields.Select(n => n.GetFieldName(settings)).ToArray())
                {
                    s = g.MeasureString(str, font);
                    if (s.Width > xMax) xMax = s.Width;
                }
            }
            return new SizeF(xMax, rowHeight * (Fields.Count + 1));
        }

        public void Draw(Graphics g, SettingsDTO settings)
        {
            SizeF size = GetSize(settings);
            g.FillRectangle(Brushes.White, Left, Top, size.Width, size.Height);
            g.FillRectangle(Brushes.Blue, Left, Top, size.Width, rowHeight);
            if (IsSelected)
            {
                Pen p = new Pen(Brushes.Black, 3);
                g.DrawRectangle(p, Left, Top, size.Width, size.Height);
            }
            else
                g.DrawRectangle(Pens.Black, Left, Top, size.Width, size.Height);

            g.DrawString(GetTableName(settings), font, Brushes.White, Left, Top);

            for (int i = 0; i < Fields.Count; i++)
            {
                g.DrawRectangle(Pens.Black, Left, Top + (i + 1) * rowHeight, size.Width, rowHeight);
                if (Fields[i].IsPK)
                    g.DrawString(Fields[i].GetFieldName(settings), fontBold, Brushes.Black, Left, Top + (i + 1) * rowHeight);
                else
                    g.DrawString(Fields[i].GetFieldName(settings), font, Brushes.Black, Left, Top + (i + 1) * rowHeight);
            }
        }

        public object MouseGet(int x, int y, SettingsDTO settings)
        {
            SizeF size = GetSize(settings);
            RectangleF r = new RectangleF(Left, Top, size.Width, size.Height);
            if (r.Contains(x, y))
            {
                if (y - Top < rowHeight) return this;
                int i = (int)Math.Floor((y - Top) / rowHeight) - 1;
                return Fields[i];
            }
            return null;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(TableSchema))
                return TableSchema + "." + TableName;
            return TableName;
        }
    }
}

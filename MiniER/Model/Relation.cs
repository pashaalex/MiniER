using System;
using System.Collections.Generic;

using System.Drawing;

namespace MiniER.Model
{
    public class Relation : IDrawable
    {
        public Field field1 {get; set;}
        public Field field2 {get; set;}
        
        public bool IsSelected = false;

        float x1, y1, x2, y2;
        bool IsLeft1 = true; 
        bool IsLeft2 = true;

        public void Draw(Graphics g, SettingsDTO settings)
        {
            SizeF s1 = field1.Table.GetSize(settings);
            SizeF s2 = field2.Table.GetSize(settings);

            x1 = field1.Table.Left;
            y1 = field1.Table.Top + (field1.Table.FieldSource.IndexOf(field1) + 1.5F) * Table.rowHeight;

            x2 = field2.Table.Left;
            y2 = field2.Table.Top + (field2.Table.FieldSource.IndexOf(field2) + 1.5F) * Table.rowHeight;

            if (x1 + s1.Width < x2)
            {
                x1 += s1.Width;
                IsLeft1 = false;
            }
            else
                IsLeft1 = true;

            if (x2 + s2.Width < x1)
            {
                x2 += s2.Width;
                IsLeft2 = false;
            }
            else
                IsLeft2 = true;

            if ((!field1.IsPK) && (field2.IsPK)) g.DrawEllipse(Pens.Black, x1 - 5, y1 - 5, 10, 10);
            if ((field1.IsPK) && (!field2.IsPK)) g.DrawEllipse(Pens.Black, x2 - 5, y2 - 5, 10, 10);

            List<PointF> points = new List<PointF>();
            points.Add(new PointF(x1, y1));
            if (IsLeft1)
                points.Add(new PointF(x1 - 10, y1));
            else
                points.Add(new PointF(x1 + 10, y1));

            if (IsLeft2)
                points.Add(new PointF(x2 - 10, y2));
            else
                points.Add(new PointF(x2 + 10, y2));
            points.Add(new PointF(x2, y2));

            Pen p = Pens.Black;
            if (IsSelected) p = new Pen(Brushes.Black, 3);
            g.DrawLines(p, points.ToArray());
        }

        public object MouseGet(int x, int y, SettingsDTO settings)
        {
            double mx1 = IsLeft1 ? x1 - 10 : x1 + 10;
            double my1 = y1;
            double mx2 = IsLeft2 ? x2 - 10 : x2 + 10;
            double my2 = y2;

            double a = Math.Sqrt((x - mx1) * (x - mx1) + (y - my1) * (y - my1));
            double b = Math.Sqrt((x - mx2) * (x - mx2) + (y - my2) * (y - my2));
            double c = Math.Sqrt((mx1 - mx2) * (mx1 - mx2) + (my1 - my2) * (my1 - my2));
            if ((a < c) && (b < c))
            {
                double p = (a + b + c) / 2.0;
                double h = 2 * Math.Sqrt(p * (p - 2) * (p - b) * (p - c)) / c;
                if (h < 3) return this;
            }

            if (Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1)) < 10) return this;
            if (Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2)) < 10) return this;

            return null;
        }
    }
}

using System;

using MiniER.Model;
using System.Drawing;

namespace MiniER.OpenSave
{
    public class SaveEMF : ISave
    {
        public string GetExtension()
        {
            return "Vector *.emf|*.emf";
        }

        public bool IsMyExtension(string extension)
        {
            return string.Compare(extension, ".emf") == 0;
        }

        public void Save(Model.DataSchema schema, string fileName)
        {
            System.Drawing.Imaging.Metafile curMetafile = null;

            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            IntPtr hdc = g.GetHdc();
            try
            {
                curMetafile = new System.Drawing.Imaging.Metafile(fileName, hdc, System.Drawing.Imaging.EmfType.EmfOnly);
            }
            catch
            {
                g.ReleaseHdc(hdc);
                g.Dispose();
                throw;
            }

            Graphics gr = Graphics.FromImage(curMetafile);
            // Draw links
            foreach (Relation r in schema.RelationList)
                r.Draw(gr, SettingsDTO.GetSettings());

            // Draw tables
            foreach (Table t in schema.TableList)
                t.Draw(gr, SettingsDTO.GetSettings());

            g.ReleaseHdc(hdc);
            gr.Dispose();
            g.Dispose();
            bmp.Dispose();
        }
    }
}

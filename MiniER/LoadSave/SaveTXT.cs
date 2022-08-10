using System.Linq;

using System.IO;
using MiniER.Model;

namespace MiniER.OpenSave
{
    class SaveTXT : ISave
    {
        public string GetExtension()
        {
            return "TXT-Files|*.txt";
        }

        public bool IsMyExtension(string extension)
        {
            return string.Compare(extension, ".txt") == 0;
        }

        public void Save(Model.DataSchema schema, string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
                foreach (Table tv in schema.TableList)
                {
                    sw.WriteLine("Table: " + tv.TableName);
                    foreach (Field tf in tv.Fields)
                    {
                        sw.Write("    " + tf.FieldName + " " + tf.DataType.ToString() + (tf.IsPK ? "[KEY]" : ""));
                        if (!tf.IsPK)
                        {
                            var r1 = schema.RelationList.FirstOrDefault(n => n.field1 == tf);
                            var r2 = schema.RelationList.FirstOrDefault(n => n.field2 == tf);
                            if (r1 != null) sw.Write(" relate with " + r1.field2.Table.TableName + "." + r1.field2.FieldName);
                            if (r2 != null) sw.Write(" relate with " + r2.field1.Table.TableName + "." + r2.field1.FieldName);
                        }
                        sw.WriteLine();
                    }
                }
        }
    }
}

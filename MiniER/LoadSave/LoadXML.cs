using System;
using System.Collections.Generic;

using MiniER.Model;
using System.Xml;

namespace MiniER.OpenSave
{
    public class LoadXML: ILoad
    {
        public string GetExtension()
        {
            return "XML-Files|*.xml";
        }

        public bool IsMyExtension(string extension)
        {
            return string.Compare(extension, ".xml") == 0;
        }

        public Model.DataSchema Open(string fileName)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(fileName);
            Model.DataSchema res = new Model.DataSchema();

            Dictionary<int, Field> fields = new Dictionary<int, Field>();
            foreach (XmlNode tblNode in xml.SelectNodes("//table"))
            {
                Table tv = new Table();
                res.TableList.Add(tv);
                tv.TableName = tblNode.Attributes["tableName"].Value;
                if (tblNode.Attributes["tableSchema"] != null)
                    tv.TableSchema = tblNode.Attributes["tableSchema"].Value;
                tv.Top = int.Parse(tblNode.Attributes["top"].Value);
                tv.Left = int.Parse(tblNode.Attributes["left"].Value);
                foreach (XmlNode fldNode in tblNode.SelectNodes("field"))
                {
                    Field tf = tv.NewField();
                    tf.fieldUniqueId = int.Parse(fldNode.Attributes["fieldId"].Value);
                    fields.Add(tf.fieldUniqueId, tf);
                    tf.FieldName = fldNode.Attributes["name"].Value;
                    DataType o = DataType.STRING;
                    if (Enum.TryParse<DataType>(fldNode.Attributes["type"].Value, out o))
                        tf.DataType = o;

                    if ((fldNode.Attributes["PK"] != null) 
                        && (fldNode.Attributes["PK"].Value == "True")) tf.IsPK = true;
                }
            }

            foreach (XmlNode relNode in xml.SelectNodes("//relation"))
            {
                Relation tr = new Relation();
                tr.field1 = fields[int.Parse(relNode.Attributes["field1Id"].Value)];
                tr.field2 = fields[int.Parse(relNode.Attributes["field2Id"].Value)];
                res.RelationList.Add(tr);
            }
            return res;
        }
    }
}

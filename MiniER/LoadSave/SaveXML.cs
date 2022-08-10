using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using MiniER.Model;

namespace MiniER.OpenSave
{
    public class SaveXML : ISave
    {
        public string GetExtension()
        {
            return "XML-Files|*.xml";
        }

        public bool IsMyExtension(string extension)
        {
            return string.Compare(extension, ".xml") == 0;
        }

        public void Save(Model.DataSchema schema, string fileName)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><root/>");
            int Id = 1;
            XmlNode tables = xml.CreateElement("tables");
            xml.DocumentElement.AppendChild(tables);

            foreach (Table tv in schema.TableList)
            {
                XmlElement table = xml.CreateElement("table");
                tables.AppendChild(table);
                table.SetAttribute("tableName", tv.TableName);
                table.SetAttribute("tableSchema", tv.TableSchema);
                table.SetAttribute("top", tv.Top.ToString());
                table.SetAttribute("left", tv.Left.ToString());
                foreach (Field tf in tv.Fields)
                {
                    tf.fieldUniqueId = Id++;
                    XmlElement field = xml.CreateElement("field");
                    table.AppendChild(field);
                    field.SetAttribute("name", tf.FieldName);
                    field.SetAttribute("fieldId", tf.fieldUniqueId.ToString());
                    field.SetAttribute("type", tf.DataType.ToString());
                    if (tf.IsPK) field.SetAttribute("PK", "True");
                }
            }

            XmlNode relationsNode = xml.CreateElement("relations");
            xml.DocumentElement.AppendChild(relationsNode);

            foreach (Relation tr in schema.RelationList)
            {
                XmlElement relationNode = xml.CreateElement("relation");
                relationsNode.AppendChild(relationNode);
                relationNode.SetAttribute("field1Id", tr.field1.fieldUniqueId.ToString());
                relationNode.SetAttribute("field2Id", tr.field2.fieldUniqueId.ToString());
            }

            xml.Save(fileName);
        }
    }
}

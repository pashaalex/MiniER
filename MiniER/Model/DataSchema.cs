using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniER.Model
{
    public class DataSchema
    {
        public List<Table> TableList = new List<Table>();
        public List<Relation> RelationList = new List<Relation>();
    }
}

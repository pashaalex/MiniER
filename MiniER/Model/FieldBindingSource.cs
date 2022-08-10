using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniER.Model
{
    public class FieldBindingSource : System.Windows.Forms.BindingSource
    {
        private Table tv;

        public override object AddNew()
        {
            object o = base.AddNew();
            Field tf = (Field)o;
            tf.Table = tv;
            return tf;
        }

        public override void Remove(object value)
        {
            base.Remove(value);
        }

        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
        }

        public FieldBindingSource(Table _tv, List<Field> collection)
        {
            tv = _tv;
            this.DataSource = collection;
        }
    }
}

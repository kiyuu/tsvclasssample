using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ConsoleApp8.Model
{
    public class Item : DBModelBase
    {
        public override string TableName => "ItemMaster";

        public override string WorkTableName => "W_ItemMaster";

        protected override ReadOnlyCollection<Column> CreateColumns()
        {
            var result = new List<Column>();

            result.Add(new Column("ItemCode", typeof(string), result.Count + TsvStartCulmnIndex, 8, true, true, true));
            result.Add(new Column("ItemName", typeof(string), result.Count + TsvStartCulmnIndex, 8, true, true, true));

            return result.AsReadOnly();
        }
    }
}

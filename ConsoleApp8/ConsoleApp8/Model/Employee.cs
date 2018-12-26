using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ConsoleApp8.Model
{
    public class Employee : DBModelBase
    {
        public override string TableName => "EmployeeMaster";

        public override string WorkTableName => "W_EmployeeMaster";

        protected override ReadOnlyCollection<Column> CreateColumns()
        {
            var result = new List<Column>();
            result.Add(new Column("EmproyeeCode", typeof(string), result.Count + this.TsvStartCulmnIndex, 8, true, true, true));
            result.Add(new Column("EmproyeeName", typeof(string), result.Count + this.TsvStartCulmnIndex, 8, true, true, true));
            result.Add(new Column<string>("Password", result.Count + this.TsvStartCulmnIndex, 8, true, true, true));

            return result.AsReadOnly();
        }

    }
}

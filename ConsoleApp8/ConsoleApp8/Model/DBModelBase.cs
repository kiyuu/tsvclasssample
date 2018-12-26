using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ConsoleApp8.Model
{
    public abstract class DBModelBase
    {
        public abstract string TableName { get; }
        public abstract string WorkTableName { get; }

        public ReadOnlyCollection<Column> Columns { get; }

        public virtual int TsvStartCulmnIndex => 2;

        protected abstract ReadOnlyCollection<Column> CreateColumns();

        public DBModelBase()
        {
            this.Columns = this.CreateColumns();
        }
    }
}

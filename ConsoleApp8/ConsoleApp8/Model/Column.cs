using System;

namespace ConsoleApp8
{
    public class Column
    {
        public Column(string columnName,Type type,int tsvIndex,int length,bool isLengthCheck,bool isSelectColumn,bool isWhereColumn)
        {
            this.ColumnName = columnName;
            this.ColumnType = type;
            this.TSVIndex = tsvIndex;
            this.Length = length;
            this.IsLengthCheck = isLengthCheck;
            this.IsSelectColumn = IsSelectColumn;
            this.IsWhereColumn = IsWhereColumn;
        }

        public string ColumnName { get; private set; }

        public Type ColumnType { get; private set; }

        public int TSVIndex { get; private set; }

        public int Length { get; private set; }

        public bool IsLengthCheck { get; private set; }

        public bool IsTypeCheck { get; private set; }

        public bool IsSelectColumn { get; private set; }

        public bool IsWhereColumn { get; private set; }

        public virtual bool TryParse(string value)
        {
            return true;
        }

    }
}

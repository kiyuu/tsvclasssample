using ConsoleApp8.Utility;
using System;

namespace ConsoleApp8
{
    public class Column<T> : Column
    {
        public Column(string columnName, int tsvIndex, int length, bool isLengthCheck, bool isSelectColumn, bool isWhereColumn)
            : base(columnName, typeof(T), tsvIndex, length, isLengthCheck, isSelectColumn, isWhereColumn)
        {
        }

        public override bool TryParse(string value)
        {
            return ConvertUtility.TryParse<T>(value, out T result);
        }
    }
}

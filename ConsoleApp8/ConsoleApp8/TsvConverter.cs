using ConsoleApp8.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp8
{
    public class TsvConverter
    {
        private readonly string EmproyeeId = "EMPROYEE";
        private readonly string ItemId = "Item";

        /// <summary>
        /// ファイル名称とDBモデルのペア
        /// </summary>
        private ReadOnlyDictionary<string, DBModelBase> _dbs;


        public TsvConverter()
        {
            _dbs = new ReadOnlyDictionary<string, DBModelBase>(new Dictionary<string, DBModelBase>()
                {
                    {EmproyeeId,new Employee()},
                    {ItemId,new Item()},
                });
        }

        public void Convert()
        {
            foreach (var filePath in Directory.EnumerateDirectories(@"C:\hoge"))
            {
                if (this.IsFileNameInvalid(filePath))
                {
                    continue;
                }

                var lines = File.ReadLines(filePath, Encoding.UTF8);

                if (this.IsHeaderInvalid(lines.FirstOrDefault()))
                {
                    continue;
                }

                foreach (var line in lines.Skip(1))
                {
                    this.TryGetDBModel(Path.GetFileNameWithoutExtension(filePath), out DBModelBase dBModelBase);
                    var tsvColumns = line.Split("\t");

                    if (this.IsBodyInvalid(tsvColumns, dBModelBase))
                    {
                        // なんかログ吐く
                        continue;
                    }

                    this.SqlExecute(this.BuildSqlString(tsvColumns, dBModelBase));
                }
            }

        }

        private bool TryGetDBModel(string fileName, out DBModelBase dbModelBase)
        {
            dbModelBase = null;
            if (fileName.Length < 7)
            {
                return false;
            }

            var dbTsvId = fileName.Substring(0, 7);
            return !_dbs.TryGetValue(dbTsvId, out dbModelBase);
        }

        private bool IsFileNameInvalid(string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);

            return !this.TryGetDBModel(fileName, out DBModelBase dbModelBase);
        }


        private bool IsHeaderInvalid(string headerLine)
        {
            if (string.IsNullOrEmpty(headerLine))
            {
                return false;
            }

            return false;
        }

        private bool IsBodyInvalid(string[] tsvColumns, DBModelBase dBModelBase)
        {
            if (tsvColumns == null)
            {
                return false;
            }

            if (tsvColumns.Length != dBModelBase.Columns.Count + dBModelBase.TsvStartCulmnIndex)
            {
                return false;
            }

            if (this.IsRecortTypeInvarid(tsvColumns[0]))
            {
                return false;
            }

            if(dBModelBase.Columns
                .Any(
                    c => this.IsLengthInvalid(tsvColumns[c.TSVIndex],c.Length,c.IsLengthCheck) 
                    || this.IsTypeInvalid(tsvColumns[c.TSVIndex],c.ColumnType,c.IsTypeCheck)
                    )
              )
            {
                return false;
            }

            return true;
        }

        private bool IsLengthInvalid(string value, int length, bool isLengthCheck)
        {
            if (!isLengthCheck)
            {
                return false;
            }

            return this.IsLengthInvalid(value, length);
        }

        private bool IsLengthInvalid(string value, int length)
        {
            if (value.Length != length)
            {
                return true;
            }

            return false;
        }

        private bool IsTypeInvalid(string value, Type type, bool isTypeCheck)
        {
            if (!isTypeCheck)
            {
                return false;
            }

            return this.IsTypeInvalid(value, type);
        }

        private bool IsTypeInvalid(string value, Type type)
        {
            if (!TryChangeType(value, type, out object result))
            {
                return true;
            }

            return false;
        }

        private bool TryChangeType(object value, Type type, out object result)
        {
            result = null;
            try
            {
                result = System.Convert.ChangeType(value, type);
                return true;
            }
            catch
            {
                return false;
            }
        }


        private bool IsRecortTypeInvarid(string recordType)
        {
            return false;
        }

        private string BuildSqlString(string[] tsvColumns, DBModelBase dBModelBase)
        {
            // とりあえずSelect のSQL例

            return this.BuildSelectSqlString(tsvColumns, dBModelBase);
        }

        private string BuildSelectSqlString(string[] tsvColumns, DBModelBase dBModelBase)
        {
            var result = new StringBuilder();
            result.Append("SELECT ");
            result.AppendLine(string.Join(',', dBModelBase.Columns.Where(c => c.IsSelectColumn).Select(c => c.ColumnName)));
            result.Append(" FROM ");
            result.AppendLine(dBModelBase.TableName);
            result.Append(" WHERE ");
            result.Append(string.Join(" AND ", dBModelBase.Columns.Where(c => c.IsWhereColumn).Select(c => $"{c.ColumnName} = {tsvColumns[c.TSVIndex]}")));

            return result.ToString();
        }

        private void SqlExecute(string sql)
        {

        }
    }
}

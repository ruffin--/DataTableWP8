// =========================== LICENSE ===============================
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
// ======================== EO LICENSE ===============================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.rufwork.polyfills.data
{
    public class DictionaryPlusColumn : Dictionary<string, DataColumn>
    {
        public DataColumn this[int intIndex]
        {
            get
            {
                return this.ElementAt(intIndex).Value;
            }
            set
            {
                this[this.ElementAt(intIndex).Key] = value;
            }
        }

        public void Add(DataColumn colToAdd)
        {
            this.Add(colToAdd.ColumnName, colToAdd);
        }
    }

    public class DataTable
    {
        public bool CaseSensitive = false;  // not actually live/used.
        public List<DataRow> Rows = new List<DataRow>();
        private DictionaryPlusColumn _columns = new DictionaryPlusColumn();

        public string TableName = string.Empty;
        public DataView DefaultView = null;

        public List<DataColumn> Columns
        {
            get
            {
                List<DataColumn> cols = new List<DataColumn>();
                foreach (KeyValuePair<string, DataColumn> kvp in _columns)
                {
                    cols.Add(kvp.Value);
                }
                return cols;
            }
        }

        public DataTable()
        {
        }

        public DataTable(string strTableName)
        {
            this.TableName = strTableName;
        }

        public DataRow NewRow()
        {
            DataRow newRow = new DataRow();

            foreach (KeyValuePair<string, DataColumn> kvpStringDataCol in _columns)
            {
                newRow.Add(kvpStringDataCol.Value, null);
            }

            return newRow;
        }
    }

    public class DataRow : Dictionary<DataColumn, object>, IComparable
    {
        private int _findIndexByColName(string strColName)
        {
            int intColFound = -1;
            for (int i = 0; i < this.Count(); i++)
            {
                if (strColName.Equals(this.ElementAt(i).Key.ColumnName, StringComparison.CurrentCultureIgnoreCase))
                {
                    intColFound = i;
                    break;
                }
            }
            return intColFound;
        }

        public object this[int intIndex]
        {
            get
            {
                return this.ElementAt(intIndex);
            }
            set
            {
                DataColumn dc = this.ElementAt(intIndex).Key;
                this[dc] = value;
            }
        }

        public object this[string strColName]
        {
            get
            {
                int intCol = _findIndexByColName(strColName);
                if (intCol < 0)
                    throw new Exception("Column " + strColName + " does not exist in this row.");
                return this[intCol];
            }

            set
            {
                int intCol = _findIndexByColName(strColName);
                if (intCol < 0)
                    throw new Exception("Column " + strColName + " does not exist in this row.");
                this[intCol] = value;

            }
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }

    public class DataView
    {
        public DataTable baseTable = null;
        public DataTable sortedTable = null;

        private string _strSort;
        public string Sort
        {
            get
            {
                return _strSort;
            }
            set
            {
                _strSort = value;
                // sort the baseTable.
                Console.WriteLine("TODO: Sort the baseTable and store in sortedTable.");
            }
        }

        public DataView(DataTable baseTable)
        {
            this.baseTable = baseTable;
        }
    }

    public class DataColumn
    {
        private System.Type _dataType = null;
        public System.Type DataType
        {
            get
            {
                return _dataType;
            }
            set
            {
                if (null == value)
                {
                    throw new Exception("A DataColumn's DataType cannot be null.");
                }
                _dataType = value;
            }
        }
        public string ColumnName = string.Empty;

        public DataColumn(string strName)
        {
            this.ColumnName = strName;
        }
    }
}

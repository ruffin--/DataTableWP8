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
        public List<DataRow> Rows = new List<DataRow>();
        public DictionaryPlusColumn Columns = new DictionaryPlusColumn();

        public string TableName = string.Empty;

        public DataView DefaultView = null;

        public DataTable()
        {
        }

        public DataRow NewRow()
        {
            DataRow newRow = new DataRow();

            foreach (KeyValuePair<string, DataColumn> kvpStringDataCol in this.Columns)
            {
                newRow.Add(kvpStringDataCol.Value, null);
            }

            return newRow;
        }
    }

    public class DataRow : Dictionary<DataColumn, object>
    {
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

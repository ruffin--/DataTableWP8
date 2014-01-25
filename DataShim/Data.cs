// =========================== LICENSE ===============================
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
// ======================== EO LICENSE ===============================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.rufwork.polyfills.data
{
    public sealed class DictionaryBackedSet<T> : IEnumerable<T>
    {
        private readonly Dictionary<object, T> dict = new Dictionary<object, T>();

        public IEnumerator<T> GetEnumerator()
        {
            return dict.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Add(T item)
        {
            if (Contains(item))
            {
                return false;
            }
            // MEGATODO: Needs to be changed to take the DataColumn's name when T is a Datacolumn.
            // Waiting until I extend properly, howeer.  Hacky for testing now.
            string strRandomKey = Guid.NewGuid().ToString();
            Console.WriteLine("Random key: " + strRandomKey);
            dict.Add(strRandomKey, item);
            return true;
        }

        public bool Contains(T item)
        {
            return dict.ContainsValue(item);
        }

        public int Count
        {
            get
            {
                return dict.Count;
            }
        }

        public T this[int intIndex]
        {
            get
            {
                return dict.ElementAt(intIndex).Value;
            }
        }

        //========================================================================
        // Totally hacktasticly cheating here on down for now.  Never do this.
        // TODO: I should at least protect the accepted typesin the constructor
        // and rename this type to something less generic.
        //========================================================================
        public bool Contains(string strName)
        {
            bool foundName = false;
            if (typeof(T).ToString().Contains("DataColumn"))
            {
                foreach (KeyValuePair<object, T> kvp in dict)
                {
                    if (strName.Equals((DataColumn)(object)kvp.Value.ToString()))
                    {
                        foundName = true;
                        break;
                    }
                }
            }
            return foundName;
        }

        public T this[string strName]
        {
            get
            {
                T t2ret = default(T);
                bool foundCol = false;
                if (typeof(T).ToString().Contains("DataColumn"))
                {
                    foreach (T col in this)
                    {
                        DataColumn tempCol = (DataColumn)(object)col;
                        if (tempCol.ColumnName == strName)
                        {
                            t2ret = col;
                            foundCol = true;
                            break;
                        }
                    }
                }
                if (!foundCol)
                    throw new Exception("Column " + strName + " was not found in this collection.");
                return t2ret;
            }
            //set
            //{
            //}
        }
        //========================================================================
        //========================================================================
    }

    public class DataTable
    {
        public bool CaseSensitive = false;  // not actually live/used.
        public List<DataRow> Rows = new List<DataRow>();
        public DictionaryBackedSet<DataColumn> Columns = new DictionaryBackedSet<DataColumn>();

        public string TableName = string.Empty;
        public DataView DefaultView = null;

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

            foreach (DataColumn col in Columns)
            {
                newRow.Add(col, null);
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

        public DataTable ToTable()
        {
            return this.sortedTable;
        }
    }

    public class DataColumn
    {
        public int MaxLength = -1;  // not used. for duck typing only.

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

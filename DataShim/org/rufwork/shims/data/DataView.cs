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
using org.rufwork.collections;
using System.IO;

namespace org.rufwork.shims.data
{
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
                this.sortedTable = _sortTable();
            }
        }

        private DataTable _sortTable()
        {
            int intTempIndex = -1;
            string[] astrSort = _strSort.Split(' ');

            if (astrSort.Length != 2)
            {
                throw new Exception("Sort clause in unsupported format: " + _strSort);
            }
            for (int i = 0; i < this.baseTable.Columns.Count; i++)
            {
                if (this.baseTable.Columns[i].ColumnName.Equals(astrSort[0], StringComparison.CurrentCultureIgnoreCase))
                {
                    intTempIndex = i;
                    break;
                }
            }
            if (-1 == intTempIndex)
            {
                throw new Exception("Column name not found: " + astrSort[0]);
            }

            DataTable dtReturn = _dupeTableNoData();

            switch (this.baseTable.Columns[intTempIndex].DataType.ToString())
            {
                case "System.Int32":
                case "System.String":
                case "System.Decimal":
                case "System.DateTime":
                    Dictionary<int, IComparable> dictRowNumThenIntValue = new Dictionary<int, IComparable>();
                    for (int i=0; i < this.baseTable.Rows.Count; i++)
                    {
                        dictRowNumThenIntValue.Add(i, (IComparable)this.baseTable.Rows[i][intTempIndex]);
                    }

                    if (astrSort[1].Equals("DESC", StringComparison.CurrentCultureIgnoreCase))
                        foreach (KeyValuePair<int, IComparable> rowNumThenValue in dictRowNumThenIntValue.OrderByDescending(entry => entry.Value))
                            dtReturn.Rows.Add(_copyRow(dtReturn.NewRow(), dtReturn.Columns, rowNumThenValue.Key));
                    else if (astrSort[1].Equals("ASC", StringComparison.CurrentCultureIgnoreCase))
                        foreach (KeyValuePair<int, IComparable> rowNumThenValue in dictRowNumThenIntValue.OrderBy(entry => entry.Value))
                            dtReturn.Rows.Add(_copyRow(dtReturn.NewRow(), dtReturn.Columns, rowNumThenValue.Key));
                    break;

                default:
                    throw new NotImplementedException(
                        string.Format(
                            "Uncaptured data type ({0}) for datatable sort: {1}",
                            this.baseTable.Columns[intTempIndex].DataType.ToString(),
                             dtReturn.Columns[intTempIndex].ColumnName
                        )
                    );
            }

            return dtReturn;
        }

        private DataRow _copyRow(DataRow newRow, IEnumerable<DataColumn> newTableColumns, int intBaseTableRowToCopy)
        {
            foreach (DataColumn dc in newTableColumns)
            {
                newRow[dc] = this.baseTable.Rows[intBaseTableRowToCopy][dc.ColumnName];
            }
            return newRow;
        }

        private DataTable _dupeTableNoData()
        {
            DataTable dtOut = new DataTable();

            foreach (DataColumn col in this.baseTable.Columns)
            {
                DataColumn colNew = new DataColumn(col.ColumnName);
                colNew.DataType = col.DataType;
                dtOut.Columns.Add(colNew);
            }
            return dtOut;
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
}
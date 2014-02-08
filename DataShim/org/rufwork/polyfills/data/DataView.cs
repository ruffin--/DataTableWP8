﻿// =========================== LICENSE ===============================
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
using System.Runtime.Serialization.Formatters.Binary;

namespace org.rufwork.polyfills.data
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
                // sort the sortedTable.
                Console.WriteLine("TODO: Sort the sortedTable: " + _strSort);
            }
        }

        private DataTable _sortTable()
        {
            Console.WriteLine("Get the column from name");
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

            switch (Type.GetTypeCode(this.baseTable.Columns[intTempIndex].DataType))
            {
                case TypeCode.Int32:
                case TypeCode.String:
                case TypeCode.Decimal:
                case TypeCode.DateTime:
                    Dictionary<int, IComparable> dictRowNumThenIntValue = new Dictionary<int, IComparable>();
                    for (int i=0; i < this.baseTable.Rows.Count; i++)
                    {
                        dictRowNumThenIntValue.Add(i, (IComparable)this.baseTable.Rows[i][intTempIndex]);
                    }

                    if (astrSort[1].Equals("DESC"))
                    {
                        foreach (KeyValuePair<int, IComparable> rowNumThenValue in dictRowNumThenIntValue.OrderByDescending(entry => entry.Value))
                        {
                            DataRow newRow = dtReturn.NewRow();
                            foreach (DataColumn dc in dtReturn.Columns)
                            {
                                newRow[dc] = this.baseTable.Rows[rowNumThenValue.Key][dc.ColumnName];
                            }
                            dtReturn.Rows.Add(newRow);
                        }
                    }
                    else
                    {
                        foreach (KeyValuePair<int, IComparable> rowNumThenValue in dictRowNumThenIntValue.OrderBy(entry => entry.Value))
                        {
                            DataRow newRow = dtReturn.NewRow();
                            foreach (DataColumn dc in dtReturn.Columns)
                            {
                                newRow[dc] = this.baseTable.Rows[rowNumThenValue.Key][dc.ColumnName];
                            }
                            dtReturn.Rows.Add(newRow);
                        }
                    }
                    break;

                default:
                    throw new NotImplementedException("Uncaptured data type for datatable sort: " + dtReturn.Columns[intTempIndex].ColumnName);
            }

            return dtReturn;
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

            //foreach (DataRow row in this.baseTable.Rows)
            //{
            //    DataRow rowNew = dtOut.NewRow();
            //    for (int i = 0; i < this.baseTable.Columns.Count; i++)
            //    {
            //        rowNew[i] = row[i];
            //    }
            //    dtOut.Rows.Add(rowNew);
            //}

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
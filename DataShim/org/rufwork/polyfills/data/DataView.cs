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
}
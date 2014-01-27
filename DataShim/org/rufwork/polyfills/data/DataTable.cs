using System;
using System.Collections.Generic;
using org.rufwork.collections;

namespace org.rufwork.polyfills.data
{
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
}


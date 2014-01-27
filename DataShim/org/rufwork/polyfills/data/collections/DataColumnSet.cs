using System;
using System.Collections.Generic;
using org.rufwork.collections;
using org.rufwork.polyfills.data;

namespace DataShim
{
	public class DataColumnSet : DictionaryBackedSet<DataColumn>
	{
		//========================================================================
		// Totally hacktasticly cheating here on down for now.  Never do this.
		// TODO: I should at least protect the accepted typesin the constructor
		// and rename this type to something less generic.
		//========================================================================
		public bool Contains(string strName)
		{
			bool foundName = false;
			foreach (KeyValuePair<object, DataColumn> kvp in dict)
			{
				if (strName.Equals((DataColumn)(object)kvp.Value.ToString()))
				{
					foundName = true;
					break;
				}
			}
			return foundName;
		}

		public DataColumn this [string strName] {
			get {
				DataColumn t2ret = null;
				bool foundCol = false;

				foreach (DataColumn col in this) {
					DataColumn tempCol = (DataColumn)(object)col;
					if (tempCol.ColumnName == strName) {
						t2ret = col;
						foundCol = true;
						break;
					}
				}
				if (!foundCol)
					throw new Exception ("Column " + strName + " was not found in this collection.");
				return t2ret;
			}
			//set
			//{
			//}
		}
	}
}


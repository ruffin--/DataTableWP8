//The MIT License (MIT)

//Copyright (c) 2014 Ruffin Bailey, except where explicitly noted.

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.

using org.rufwork.polyfills.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataShim
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.SetWindowSize(120, 30);
            }
            catch (Exception)
            {
            }

            // Below code is on a number of places on the net.
            // Consider it MIT licensed here at best.  The few
            // mistakes that seem cut and pasted have been fixed,
            // but it's pretty much otherwise the same stuff everyone
            // else has.
            DataTable table = new DataTable();

            //Create 7 columns for this DataTable
            DataColumn col1 = new DataColumn("ID");
            DataColumn col2 = new DataColumn("Name");
            DataColumn col3 = new DataColumn("Checked");
            DataColumn col4 = new DataColumn("Description");
            DataColumn col5 = new DataColumn("Price");
            DataColumn col6 = new DataColumn("Brand");
            DataColumn col7 = new DataColumn("Remarks");

            //Define DataType of the Columns
            col1.DataType = System.Type.GetType("System.Int32");
            col2.DataType = System.Type.GetType("System.String");
            col3.DataType = System.Type.GetType("System.Boolean");
            col4.DataType = System.Type.GetType("System.String");
            col5.DataType = System.Type.GetType("System.Double");
            col6.DataType = System.Type.GetType("System.String");
            col7.DataType = System.Type.GetType("System.String");

            //Add All These Columns into DataTable table
            table.Columns.Add(col1);
            table.Columns.Add(col2);
            table.Columns.Add(col3);
            table.Columns.Add(col4);
            table.Columns.Add(col5);
            table.Columns.Add(col6);
            table.Columns.Add(col7);

            //Create a Row in the DataTable table
            DataRow row = table.NewRow();

            //Fill All Columns with Data
            row[col1] = 1100;
            row[col2] = "Computer Set";
            row[col3] = true;
            row[col4] = "New computer set";
            row[col5] = 32000.00;  // okay, I changed this to make sure indexes worked.
            row[col6] = "NEW BRAND-1100";
            row[col7] = "Purchased on July 30,2008";

            //Add the Row into DataTable
            table.Rows.Add(row);

            // I'm back from the borrowed code, adding a second line.
            //Create a Row in the DataTable table
            row = table.NewRow();
            row[col1] = 131;
            row[col2] = "Another name";
            row[col3] = false;
            row[col4] = "Better than a computer set.";
            row[col5] = 200.99;
            row[col6] = "ACME";
            row[col7] = "Different remarks.";
            table.Rows.Add(row);

            Console.WriteLine(col1.GetType().ToString());
            Console.WriteLine(table.Columns[0].GetType().ToString());
            Console.WriteLine(table.Rows[0][table.Columns[0]]);
            Console.WriteLine(Program.dataTableToString(table));

            //table.DefaultView.Sort = "Description DESC";
            table.DefaultView.Sort = "ID ASC";
            table = table.DefaultView.ToTable();

            Console.WriteLine(Program.dataTableToString(table));

            Console.WriteLine("Return to end.");
            Console.ReadLine();
        }

        // This is mine.  Have at it, MIT license.
        public static string dataTableToString(DataTable dtIn)
        {
            string strReturn = "";
            int[] aintColLength = new int[dtIn.Columns.Count];

            for (int i = 0; i < dtIn.Columns.Count; i++)
            {
                DataColumn dc = dtIn.Columns[i];
                aintColLength[i] = dc.ColumnName.Length;    // start with a minimum display length of the column name.

                // Figure out how many characters might be in this column
                // for the current data.  I'm going to ignore performance for now.

                foreach (DataRow dr in dtIn.Rows)
                {
                    if (dr[dc].ToString().Length > aintColLength[i])
                    {
                        aintColLength[i] = dr[dc].ToString().Length;
                    }
                }

                if (dc.ColumnName.Length > aintColLength[i])
                {
                    strReturn += dc.ColumnName.Substring(0, aintColLength[i]) + " | ";
                }
                else
                {
                    strReturn += dc.ColumnName.PadRight(aintColLength[i]) + " | ";
                }
            }
            strReturn += System.Environment.NewLine;

            foreach (DataRow dr in dtIn.Rows)
            {
                // TODO: Is foreach Column order guaranteed?
                for (int i = 0; i < dtIn.Columns.Count; i++)
                {
                    strReturn += dr[dtIn.Columns[i]].ToString().PadLeft(aintColLength[i]) + " # ";
                }
                strReturn += System.Environment.NewLine;
            }

            return strReturn;
        }


    }
}

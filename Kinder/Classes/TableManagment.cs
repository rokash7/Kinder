using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kinder.Classes
{
    public static class TableManagment
    {
        //generic method
        public static void FillTable<Tdata>(ref DataGrid table, List<Tdata> data)
        {
            table.Items.Clear();

            foreach (Tdata element in data)
            {
                table.Items.Add(element);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Kinder.Classes;

namespace Kinder
{
    /// <summary>
    /// Interaction logic for ItemsListing.xaml
    /// </summary>
    public partial class ItemsListing : Window
    {
        public ItemsListing()
        {
            InitializeComponent();
        }

        private List<Item> ItemsList = new();

        private void ReadDataFromFile()
        {
            StreamReader file = new("./Data_Files/Items.txt");

            string line;
            while((line = file.ReadLine()) != null)
            {
                ItemsList.Add(ParseData(line));
            }

            file.Close();
        }

        private Item ParseData(string line)
        {
            Item result = new();

            string[] data = line.Split(';');

            result.ID = int.Parse(data[0]);
            result.dateOfPurchase = DateTime.Parse(data[1]);
            result.Condition = (ICondition.options) Enum.Parse(typeof(ICondition.options), data[2]);
            result.Cathegory = (ICathegory.options) Enum.Parse(typeof(ICathegory.options), data[3]);
            result.userID = int.Parse(data[4]);

            string[] dimsParsed = data[5].Split(',');
            int l= int.Parse(dimsParsed[0]) , w= int.Parse(dimsParsed[1]), h= int.Parse(dimsParsed[2]);

            result.size = new Dimensions(l, h, w);
            result.karmaPrice = int.Parse(data[6]);

            return result;
        }
    }
}

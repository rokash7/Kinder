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
    /// Interaction logic for UsersLikedItems.xaml
    /// </summary>
    public partial class UsersLikedItems : Window
    {
        private string FileLocation_liked = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items_liked.txt");
        private string FileLocation_items = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items.txt");

        private List<LikedItemsClass> LikedItems = new();
        private List<Item> Items = new();

        public UsersLikedItems()
        {
            InitializeComponent();
            GetItems();
            LoadData();
        }

        private void GetItems()
        {
            Item Temp = new();
            List<LikedItemsClass> TempList = new();
            
            //loading list of liked Items
            using (StreamReader reader = new StreamReader(FileLocation_liked))
            {
                while (!reader.EndOfStream)
                {
                    int[] tempArr = Temp.ParsedLiked(reader.ReadLine());
                    List<int> tempList = new();

                    for (int i = 1; i < tempArr.Length; i++)
                    {
                        tempList.Add(tempArr[i]);
                    }

                    TempList.Add(new LikedItemsClass(tempArr[0], tempList));
                    LikedItems.Add(new LikedItemsClass(tempArr[0], tempList));
                }
            }

            //loading list of all Items
            List<Item> AllItemList = new();

            using (StreamReader reader = new StreamReader(FileLocation_items))
            {
                while (!reader.EndOfStream)
                {
                    Temp = Temp.ParseData(reader.ReadLine());
                    AllItemList.Add(Temp);
                }
            }

            //filter other users, only current one stays
            var FilteredLikedList = TempList.Where
                                        (p => p.UserID == User.CurrentUserID);

            //gathering data
            var Joined = FilteredLikedList.First().ItemsIDs.GroupJoin(AllItemList, //inner sequence
                id => id, //outter key
                ite => ite.ID,  //inner key
                (id, ite) => new { id, ite });

            /*
             * Items that current user liked:
             * 
             * ID1:
             *      Item1
             *      
             * ID2:
             *      Item2
             *      
             * ....
             */

            foreach(var joined in Joined)
            {
                Items.Add(joined.ite.First());
            }
        }

        private void LoadData()
        {
            foreach(Item item in Items)
            {
                ItemsTable.Items.Add(item);
            }
        }

        private void UnlikeButton_Click(object sender, RoutedEventArgs e)
        {
            //saving item:
            Item classObj = ItemsTable.SelectedItem as Item;

            //deleting item off items list
            Items.Remove(classObj);
            LoadData();

            //deleting item off table
            ItemsTable.Items.Clear();
            LoadData();

            //deleting item off liked items list
            using (StreamWriter writer = new StreamWriter(System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\output.txt")))
            {
                foreach (LikedItemsClass liked in LikedItems)
                {
                    writer.WriteLine(liked.ToString());
                }
            }

            LikedItemsClass tempObj = LikedItems.First(p => p.UserID == User.CurrentUserID);
            LikedItems[LikedItems.IndexOf(tempObj)].ItemsIDs.Remove(classObj.ID);

            //rewriting list to file
            using(StreamWriter writer = new StreamWriter(FileLocation_liked))
            {
               foreach(LikedItemsClass liked in LikedItems)
                {
                    writer.WriteLine(liked.ToString());
                }
            }
        }
    }
}

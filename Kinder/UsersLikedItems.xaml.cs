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
        
        private List<Item> ItemsList = new();
        private List<LikedItemsClass> LikedItems = new();
        private List<Item> Items = new();
        private List<Data> GivenItems = new();
        private static int deletedItemID;

        public UsersLikedItems()
        {
            InitializeComponent();
            ReadDataFromFile();
            GetItems();
            LoadData();
            ReadGiven();
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
            //mazj
            var filteredLikedList = TempList.Where
                                        (p => p.UserID == User.CurrentUserID);

            //gathering data
            //mazoji
            var Joined = filteredLikedList.First().ItemsIDs.GroupJoin(AllItemList, //inner sequence
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

            foreach (var joined in Joined)
            {
                Items.Add(joined.ite.First());
            }
        }

        private void LoadData()
        {
            foreach (Item item in Items)
            {
                ItemsTable.Items.Add(item);
            }
        }
        private void ReadGiven()
        {
            using (StreamReader r = new(System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Claimed.txt")))
            {
                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();

                    Data temp = new();

                    string[] tempStr = line.Split(';');
                    int[] tempInt = { int.Parse(tempStr[0]), int.Parse(tempStr[1]) };

                    temp.ItemID = tempInt[0];
                    temp.ID = tempInt[1];

                    GivenItems.Add(temp);
                }
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
            using (StreamWriter writer = new StreamWriter(FileLocation_liked))
            {
                foreach (LikedItemsClass liked in LikedItems)
                {
                    writer.WriteLine(liked.ToString());
                }
            }
        }
        private void ReadDataFromFile()
        {
            ItemsList.Clear();

            StreamReader file = new(FileLocation_items);
            Item temp = new();

            string line;
            while ((line = file.ReadLine()) != null)
            {
                ItemsList.Add(temp.ParseData(line));
            }

            file.Close();
        }

        private void ReWriteFile(int itemID = -1)
        {
            //rewriting items file:
            using (StreamWriter file = new(FileLocation_items))
            {
                foreach (Item item in ItemsList)
                {
                    file.WriteLine(item.ToString());
                }
            }

            //rewrite liked items file:
            if (itemID != -1)
            {
                Item Temp = new();
                List<LikedItemsClass> TempList = new();

                //read current liked
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
                    }
                }

                //delete the absent ones
                for (int i = 0; i < TempList.Count; i++)
                {
                    TempList[i].ItemsIDs.Remove(itemID);
                }

                //upload updated liked list to file
                using (StreamWriter writer = new StreamWriter(FileLocation_liked))
                {
                    foreach (LikedItemsClass liked in TempList)
                    {
                        writer.WriteLine(liked.ToString());
                    }
                }

                using (StreamWriter w = new(System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Claimed.txt")))
                {
                    foreach (var item in GivenItems)
                    {
                        if(item.ItemID != itemID)
                        {
                            w.WriteLine(item.ItemID.ToString() + ';' + item.ID.ToString());
                        }
                    }
                }
            }
        }

        private void ReleaseKarmaPoints(Item item)
        {
            User tempUser = User.GetUserByID(item.UserID);

            tempUser.KarmaPoints += item.KarmaPrice;

            FileManager.ChangeUserField(tempUser);
        }

        private void ClaimButton_Click(object sender, RoutedEventArgs e)
        {
            Item classObj = ItemsTable.SelectedItem as Item;
            Data temp = new Data(classObj.ID, User.CurrentUserID);

            if (GivenItems.Contains(temp))
            {
                Items.Remove(classObj);
                ItemsTable.Items.Clear();
                LoadData();

                ItemsList.Remove(classObj);
                GivenItems.Remove(temp);
                ReWriteFile(classObj.ID);

                ReleaseKarmaPoints(classObj);
            }
            else
            {
                MessageBox.Show("This item is not given to you");
            }
        }

        private class Data : User, IEquatable<Data>
        { 
            public int ItemID { get; set; }

            public Data(int ItemID, int UserID)
            {
                this.ID = UserID;
                this.ItemID = ItemID;
            }

            public Data()
            {

            }

            public bool Equals(Data other)
            {
                if (other == null)
                {
                    return false;
                }

                return (this.ID.Equals(other.ID) && this.ItemID.Equals(other.ItemID));
            }
        }
    }
}

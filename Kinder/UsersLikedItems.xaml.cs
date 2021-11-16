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
        private string fileLocation_liked = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items_liked.txt");
        private string fileLocation_items = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items.txt");

        private List<Item> itemsList = new();
        private List<LikedItemsClass> likedItems = new();
        private List<Item> items = new();
        private List<Data> givenItems = new();

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
            Item temp = new();
            List<LikedItemsClass> tempList = new();

            //loading list of liked Items
            using (StreamReader reader = new StreamReader(fileLocation_liked))
            {
                while (!reader.EndOfStream)
                {
                    int[] tempArr = temp.ParsedLiked(reader.ReadLine());
                    List<int> newTempList = new();

                    for (int i = 1; i < tempArr.Length; i++)
                    {
                        newTempList.Add(tempArr[i]);
                    }

                    tempList.Add(new LikedItemsClass(tempArr[0], newTempList));
                    likedItems.Add(new LikedItemsClass(tempArr[0], newTempList));
                }
            }

            //loading list of all Items
            List<Item> allItemList = new();

            using (StreamReader reader = new StreamReader(fileLocation_items))
            {
                while (!reader.EndOfStream)
                {
                    temp = temp.ParseData(reader.ReadLine());
                    allItemList.Add(temp);
                }
            }

            //filter other users, only current one stays
            //mazj
            var filteredLikedList = tempList.Where
                                        (p => p.UserID == User.CurrentUserID);

            //gathering data
            //mazoji
            var Joined = filteredLikedList.First().ItemsIDs.GroupJoin(allItemList, //inner sequence
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
                items.Add(joined.ite.First());
            }
        }

        private void LoadData()
        {
            TableManagment.FillTable<Item>(ref itemsTable, items);
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

                    givenItems.Add(temp);
                }
            }
        }

        private void UnlikeButton_Click(object sender, RoutedEventArgs e)
        {
            //saving item:
            Item classObj = itemsTable.SelectedItem as Item;

            //deleting item off items list
            items.Remove(classObj);
            LoadData();

            //deleting item off table
            itemsTable.Items.Clear();
            LoadData();

            //deleting item off liked items list
            using (StreamWriter writer = new StreamWriter(System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\output.txt")))
            {
                foreach (LikedItemsClass liked in likedItems)
                {
                    writer.WriteLine(liked.ToString());
                }
            }

            LikedItemsClass tempObj = likedItems.First(p => p.UserID == User.CurrentUserID);
            likedItems[likedItems.IndexOf(tempObj)].ItemsIDs.Remove(classObj.ID);

            //rewriting list to file
            using (StreamWriter writer = new StreamWriter(fileLocation_liked))
            {
                foreach (LikedItemsClass liked in likedItems)
                {
                    writer.WriteLine(liked.ToString());
                }
            }
        }
        private void ReadDataFromFile()
        {
            itemsList.Clear();

            StreamReader file = new(fileLocation_items);
            Item temp = new();

            string line;
            while ((line = file.ReadLine()) != null)
            {
                itemsList.Add(temp.ParseData(line));
            }

            file.Close();
        }

        private void ReWriteFile(int itemID = -1)
        {
            //rewriting items file:
            using (StreamWriter file = new(fileLocation_items))
            {
                foreach (Item item in itemsList)
                {
                    file.WriteLine(item.ToString());
                }
            }

            //rewrite liked items file:
            if (itemID != -1)
            {
                Item temp = new();
                List<LikedItemsClass> TempList = new();

                //read current liked
                using (StreamReader reader = new StreamReader(fileLocation_liked))
                {
                    while (!reader.EndOfStream)
                    {
                        int[] tempArr = temp.ParsedLiked(reader.ReadLine());
                        List<int> newTempList = new();

                        for (int i = 1; i < tempArr.Length; i++)
                        {
                            newTempList.Add(tempArr[i]);
                        }

                        TempList.Add(new LikedItemsClass(tempArr[0], newTempList));
                    }
                }

                //delete the absent ones
                for (int i = 0; i < TempList.Count; i++)
                {
                    TempList[i].ItemsIDs.Remove(itemID);
                }

                //upload updated liked list to file
                using (StreamWriter writer = new StreamWriter(fileLocation_liked))
                {
                    foreach (LikedItemsClass liked in TempList)
                    {
                        writer.WriteLine(liked.ToString());
                    }
                }

                using (StreamWriter w = new(System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Claimed.txt")))
                {
                    foreach (var item in givenItems)
                    {
                        if (item.ItemID != itemID)
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
            Item classObj = itemsTable.SelectedItem as Item;
            Data temp = new Data(classObj.ID, User.CurrentUserID);

            if (givenItems.Contains(temp))
            {
                items.Remove(classObj);
                itemsTable.Items.Clear();
                LoadData();

                itemsList.Remove(classObj);
                givenItems.Remove(temp);
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

            public Data(int itemID, int userID)
            {
                this.ID = userID;
                this.ItemID = itemID;
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

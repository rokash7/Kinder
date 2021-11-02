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
    /// Interaction logic for LikedItems.xaml
    /// </summary>


    public partial class LikedItems : Window
    {
        //files locations:
        private string FileLocation_items = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items.txt");
        private string FileLocation_liked = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items_liked.txt");

        //collection lists:
        private List<User> UserList = new();
        private List<Item> AllItemList = new();
        private List<LikedItemsClass> LikedItemList = new();
        private List<DataStore<int, string, string, string, string>> Data = new();

        public LikedItems()
        {
            InitializeComponent();
            LoadData();
            ProcessingLists();
            UploadData();
        }

        private void LoadData()
        {
            Item Temp = new();

            //loading list of all items
            using (StreamReader reader = new StreamReader(FileLocation_items))
            {
                while (!reader.EndOfStream)
                {
                    Temp = Temp.ParseData(reader.ReadLine());
                    AllItemList.Add(Temp);
                }
            }

            //loading list of liked items
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

                    LikedItemList.Add(new LikedItemsClass(tempArr[0], tempList));
                }
            }

            //loading list of users
            FileManager Temp2 = new();

            for(int i = 0; i < User.getUserCount(); i++)
            {
                //indexed property use:
                UserList.Add(Temp2[i]);
            }
        }

        private void ProcessingLists()
        {
            //generating list of Item class for liked items
            List<Item> LikedItemListSelected = new();

            File.WriteAllText(System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Save.txt"), String.Empty);

            //iterating collections:
            foreach (LikedItemsClass liked in LikedItemList)
            {
                foreach(int id in liked.ItemsIDs)
                {
                    foreach(Item item in AllItemList)
                    {
                        if (item.ID == id)
                        {
                            Item Temp = item;
                            Temp.LikedBy = liked.UserID;

                            using (StreamWriter w = File.AppendText(System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Save.txt")))
                            {
                                w.WriteLine(CustomToString(Temp));
                            }
                        }
                    }
                }
            }

            using (StreamReader r = new(System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Save.txt")))
            {
                while (!r.EndOfStream)
                {
                    LikedItemListSelected.Add(CustomParser(r.ReadLine()));
                }
            }
            
            //filtering list, so that we see items only uploaded by current user
            var FilteredLikedList = LikedItemListSelected.Where
                                        (p => p.UserID == User.CurrentUserID);

            //gathering data in linq GroupJoin method
            var Joined = FilteredLikedList.GroupJoin(UserList, //inner sequence
                ite => ite.LikedBy, //outter key
                usr => usr.ID,  //inner key
                (ite, usr) => new { ite, usr });

            /*
             * Users that liked current user's items:
             * 
             * Item1:
             *      user1, user2, ....
             *      
             * Item2:
             *      user3, user 4, ...
             *      
             * ....
             */

            //storing data
            foreach(var item in Joined)
            {
                foreach(var user in item.usr)
                {
                    DataStore<int, string, string, string, string> Temp = new();

                    Temp.ItemID = item.ite.ID;
                    Temp.ItemName = item.ite.Name;
                    Temp.ItemDesc = item.ite.Description;
                    Temp.Username = user.Username;
                    Temp.Email = user.Email;

                    Data.Add(Temp);
                }
            }            
        }
        private void UploadData()
        {
            foreach(var item in Data)
            {
                ItemsTable.Items.Add(item);
            }
        }

        //generic class:
        private class DataStore<TItemID, TItemName, TItemDesc, TUsername, TEmail>
        {
            public TItemID ItemID { get; set; }
            public TItemName ItemName { get; set; }
            public TItemDesc ItemDesc { get; set; }
            public TUsername Username { get; set; }
            public TEmail Email { get; set; }
        }

        private Item CustomParser(string line)
        {
            Item result = new();

            string[] data = line.Split(';');

            result.ID = int.Parse(data[0]);

            result.DateOfPurchase = DateTime.Parse(data[1]);
            result.DateStr = data[1];

            result.Condition = (ConditionEnum)Enum.Parse(typeof(ConditionEnum), data[2]);
            result.Cathegory = (CathegoryEnum)Enum.Parse(typeof(CathegoryEnum), data[3]);
            result.UserID = int.Parse(data[4]);

            string[] dimsParsed = data[5].Split(',');

            //named argument usage:
            result.Size = new Dimensions(Length: int.Parse(dimsParsed[0]), Height: int.Parse(dimsParsed[1]), Width: int.Parse(dimsParsed[2]));
            result.SizeStr = result.Size.ToString();

            result.KarmaPrice = int.Parse(data[6]);

            result.SetName(data[7]);
            result.SetDescription(data[8]);

            result.LikedBy = int.Parse(data[9]);

            return result;
        }

        private String CustomToString(Item ItemObj)
        {
            return ItemObj.ToString() + ';' + ItemObj.LikedBy.ToString();
        }
    }
}

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
        private string fileLocation_items = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items.txt");
        private string fileLocation_liked = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items_liked.txt");

        //fileManager
        FileManager fileManager = new();

        //collection lists:
        private Lazy<List<User>> userList;
        private Lazy<List<LikedItemsClass>> likedItemList;

        private List<Item> allItemList = new();
        private List<DataStore> data = new();
        private List<DataStore> givenItems = new();

        public LikedItems()
        {
            userList = new Lazy<List<User>>(delegate
            {
                List<User> temp = FileManager.GetUsers();

                /*for (int i = 0; i < User.GetUserCount(); i++)
                {
                    //indexed property use:
                    temp.Add(fileManager[i]);
                }*/
                return temp;
            }, true);

            likedItemList = new Lazy<List<LikedItemsClass>>(delegate
            {
                return PopulateLikedItemList();
            }, true);

            InitializeComponent();
            LoadData();
            ProcessingLists();
            ReadGiven();
            UploadData();
        }

        private List<LikedItemsClass> PopulateLikedItemList()
        {
            List<LikedItemsClass> result = new();
            Item temp = new();

            //loading list of liked items
            using (StreamReader reader = new StreamReader(fileLocation_liked))
            {
                while (!reader.EndOfStream)
                {
                    int[] tempArr = fileManager.GetAllLikedItems(new ParsingOperations(), reader.ReadLine());
                    List<int> tempList = new();

                    for (int i = 1; i < tempArr.Length; i++)
                    {
                        tempList.Add(tempArr[i]);
                    }

                    result.Add(new LikedItemsClass(tempArr[0], tempList));
                }
            }

            return result;
        }

        private void LoadData()
        {
            Item temp = new();

            //loading list of all items
            using (StreamReader reader = new StreamReader(fileLocation_items))
            {
                while (!reader.EndOfStream)
                {
                    temp = temp.ParseData(reader.ReadLine());
                    allItemList.Add(temp);
                }
            }
        }

        private void ProcessingLists()
        {
            //generating list of Item class for liked items
            List<Item> likedItemListSelected = new();

            File.WriteAllText(System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Save.txt"), String.Empty);

            //iterating collections:
            foreach (LikedItemsClass liked in likedItemList.Value)
            {
                foreach(int id in liked.ItemsIDs)
                {
                    foreach(Item item in allItemList)
                    {
                        if (item.ID == id)
                        {
                            Item temp = item;
                            temp.LikedBy = liked.UserID;

                            using (StreamWriter w = File.AppendText(System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Save.txt")))
                            {
                                w.WriteLine(CustomToString(temp));
                            }
                        }
                    }
                }
            }

            using (StreamReader r = new(System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Save.txt")))
            {
                while (!r.EndOfStream)
                {
                    likedItemListSelected.Add(CustomParser(r.ReadLine()));
                }
            }
            
            //filtering list, so that we see items only uploaded by current user
            var filteredLikedList = likedItemListSelected.Where
                                        (p => p.UserID == User.CurrentUserID);

            //gathering data in linq GroupJoin method
            var Joined = filteredLikedList.GroupJoin(userList.Value, //inner sequence
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
                    DataStore temp = new();

                    temp.UserID = user.ID;
                    temp.ItemID = item.ite.ID;
                    temp.ItemName = item.ite.Name;
                    temp.ItemDesc = item.ite.Description;
                    temp.Username = user.Username;
                    temp.Email = user.Email;
                    temp.Line = DataLine(item.ite.ID, user.ID);

                    data.Add(temp);
                }
            }
        }

        private void ReadGiven()
        {
            using (StreamReader r = new(System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Claimed.txt")))
            {
                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();

                    DataStore temp = new();

                    string[] tempStr = line.Split(';');
                    int[] tempInt = { int.Parse(tempStr[0]), int.Parse(tempStr[1]) };

                    temp.ItemID = tempInt[0];
                    temp.UserID = tempInt[1];

                    givenItems.Add(temp);
                }
            }

            foreach (var i in givenItems)
            {
                data = data.Where(p => !(p.UserID == i.UserID && p.ItemID == i.ItemID)).ToList();
            }
        }

        private void UploadData()
        {
            TableManagment.FillTable<DataStore>(ref itemsTable, data);
        }

        private static string DataLine(int itemID, int userID)
        {
            return itemID.ToString() + ';' + userID.ToString();
        }

        private void GiveButton_Click(object sender, RoutedEventArgs e)
        {   
            int selected = itemsTable.SelectedIndex;

            using (StreamWriter w = File.AppendText(System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Claimed.txt")))
            {
                w.WriteLine(data[selected].Line);
            }

            data.RemoveAt(selected);
            
            UploadData();
        }

        public class DataStore
        {
            public int ItemID { get; set; }
            public string ItemName { get; set; }
            public string ItemDesc { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Line { get; set; }
            public int UserID { get; set; }
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
            result.Size = new Dimensions(length: int.Parse(dimsParsed[0]), height: int.Parse(dimsParsed[1]), width: int.Parse(dimsParsed[2]));
            result.SizeStr = result.Size.ToString();

            result.KarmaPrice = int.Parse(data[6]);

            result.SetName(data[7]);
            result.SetDescription(data[8]);

            result.LikedBy = int.Parse(data[9]);

            return result;
        }

        private String CustomToString(Item itemObj)
        {
            return itemObj.ToString() + ';' + itemObj.LikedBy.ToString();
        }
    }
}

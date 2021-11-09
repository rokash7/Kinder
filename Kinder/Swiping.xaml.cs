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
    /// Interaction logic for Swiping.xaml
    /// </summary>
    public partial class Swiping : Window
    {
        private List<Item> itemList = new();
        private List<LikedItemsClass> likedItems = new();
        private string fileLocation_items = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items.txt");
        private string fileLocation_liked = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items_liked.txt");

        public Swiping()
        {
            InitializeComponent();
            LoadItems();
            RenderItem();
        }

        private void LoadItems()
        {
            //loading all items:
            StreamReader fileAllItems = new(fileLocation_items);
            Item temp = new();

            string line;
            while ((line = fileAllItems.ReadLine()) != null)
            {
                //extention method usage
                itemList.Add(temp.ParseData(line));
            }

            fileAllItems.Close();

            //linq query to filter
            itemList = itemList.Where(p => p.UserID != User.CurrentUserID).ToList();

            //loading already liked items
            StreamReader fileLikedItems = new(fileLocation_liked);

            while ((line = fileLikedItems.ReadLine()) != null)
            {
                int[] tempArr = temp.ParsedLiked(line);
                List<int> tempList = new();

                for (int i = 1; i < tempArr.Length; i++)
                {
                    tempList.Add(tempArr[i]);
                }

                likedItems.Add(new LikedItemsClass(tempArr[0], tempList));
            }

            fileLikedItems.Close();

            //filtering already liked items
            List<LikedItemsClass> toDelete = new();
            toDelete = likedItems.Where(p => p.UserID == User.CurrentUserID).ToList();

            foreach (LikedItemsClass likedItem in toDelete)
            {
                itemList = itemList.Where(p => likedItem.ItemsIDs.Contains(p.ID) == false).ToList();
            }
        }

        private bool ItemsListIsEmpty()
        {
            if (itemList.First().ID == -1)
            {
                return true;
            }

            return false;
        }

        private void RenderItem()
        {
            if (itemList.Count == 0)
            {
                itemList.Add(new Item(-1));
                TextBox_String.Text = "no items left";
            }
            else
            {
                MessageBox.Show("Count of items loaded:" + itemList.Count.ToString());
                TextBox_String.Text = itemList.First().ToString().Replace(';', '\n');
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsListIsEmpty())
            {
                MessageBox.Show("No more left");
            }
            else
            {
                itemList.Add(itemList.First());
                itemList.RemoveAt(0);

                RenderItem(); 
            }
        }

        private void LikeItemButton_Click(object sender, RoutedEventArgs e)
        {
            //save current item
            if (ItemsListIsEmpty())
            {
                MessageBox.Show("No more left");
            }
            else
            {
                int viewedItemID = itemList.First().ID;
                LikedItemsClass temp = new();

                //update current liked item
                foreach (LikedItemsClass liked in likedItems)
                {
                    if (liked.UserID == User.CurrentUserID)
                    {
                        liked.ItemsIDs.Add(viewedItemID);
                        temp = liked;
                    }
                }

                likedItems = likedItems.Where(p => p.UserID != User.CurrentUserID).ToList();
                likedItems.Add(temp);

                //rewrite current liked items list to file
                StreamWriter file = new(fileLocation_liked);

                foreach (LikedItemsClass like in likedItems)
                {
                    file.WriteLine(like.ToString());
                }

                file.Close();

                //rendering sequence
                itemList.RemoveAt(0);
                RenderItem();
            }
        }

        private void AccountPageButton_Click(object sender, RoutedEventArgs e)
        {
            var accountPageWindow = new AccountPage();
            accountPageWindow.ShowDialog();
        }

        private void LeaderboardsPageButton_Click(object sender, RoutedEventArgs e)
        {
            var leaderboardPageWindow = new LeaderboardWindow();
            leaderboardPageWindow.ShowDialog();
        }

        private void CommunicationPageButton_Click(object sender, RoutedEventArgs e)
        {
            var communicationPageWindow = new ChatWindow();
            communicationPageWindow.ShowDialog();
        }

        private void SettingsPageButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsPageWindow = new Settings();
            settingsPageWindow.ShowDialog();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            var logInPage = new MainWindow();
            logInPage.Show();
            this.Close();
        }

        private void ItemsPageButton_Click(object sender, RoutedEventArgs e)
        {
            ItemsListing itemsListing = new();
            itemsListing.Show();
        }

        private void LikedItemsPageButton_Click(object sender, RoutedEventArgs e)
        {
            LikedItems likedItems = new();
            likedItems.Show();
        }
        
        private void UsersLikedItemsPageButton_Click(object sender, RoutedEventArgs e)
        {
            UsersLikedItems usersLikedItems = new();
            usersLikedItems.Show();
        }
    }

    internal class LikedItemsClass
    {
        public LikedItemsClass()
        {

        }
        public LikedItemsClass(int userID, List<int> itemsIDs)
        {
            UserID = userID;
            ItemsIDs = itemsIDs;
        }

        public int UserID { get; set; }

        public List<int> ItemsIDs { get; set; }

        public override string ToString()
        {
            string result = "";

            result += UserID;

            foreach (int i in ItemsIDs)
            {
                result += ';' + i.ToString();
            }

            return result;
        }
    }
}

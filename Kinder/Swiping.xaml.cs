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

        FileManager fileManager = new();

        public Swiping()
        {
            InitializeComponent();
            LoadItems();
            RenderItem();
            
            //anonymous methods as event handlers for page openings
            AccountPageButton.Click += delegate (object sender, RoutedEventArgs e)
            {
                AccountPage accountPageWindow = new AccountPage();
                accountPageWindow.ShowDialog();
            };

            LeaderboardsPageButton.Click += delegate (object sender, RoutedEventArgs e)
            {
                LeaderboardWindow leaderboardPageWindow = new LeaderboardWindow();
                leaderboardPageWindow.ShowDialog();
            };

            CommunicationPageButton.Click += delegate (object sender, RoutedEventArgs e)
            {
                ChatWindow communicationPageWindow = new ChatWindow();
                communicationPageWindow.ShowDialog();
            };

            SettingsPageButton.Click += delegate (object sender, RoutedEventArgs e)
            {
                Settings settingsPageWindow = new Settings();
                settingsPageWindow.ShowDialog();
            };

            LikedItemsPageButton.Click += delegate (object sender, RoutedEventArgs e)
            {
                LikedItems likedItems = new();
                likedItems.Show();
            };

            UsersLikedItemsPageButton.Click += delegate (object sender, RoutedEventArgs e)
            {
                UsersLikedItems usersLikedItems = new();
                usersLikedItems.Show();
            };

            ItemsPageButton.Click += delegate (object sender, RoutedEventArgs e)
            {
                ItemsListing itemsListing = new();
                itemsListing.Show();
            };

            LogOutButton.Click += delegate (object sender, RoutedEventArgs e)
            {
                var logInPage = new MainWindow();
                logInPage.Show();
                this.Close();
            };
        }

        private void LoadItems()
        {
            //loading all items:
            StreamReader fileAllItems = new(fileLocation_items);
            Item temp = new();
            string line;

            itemList = fileManager.GetAllItems(new ParsingOperations());

            fileAllItems.Close();

            //linq query to filter
            itemList = itemList.Where(p => p.UserID != User.CurrentUserID).ToList();

            //loading already liked items
            StreamReader fileLikedItems = new(fileLocation_liked);

            while ((line = fileLikedItems.ReadLine()) != null)
            {
                int[] tempArr = fileManager.GetAllLikedItems(new ParsingOperations(), line);
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

        private void RenderItem()
        {
            if (itemList.Count == 0)
            {
                itemList.Add(new Item(-1));
                TextBox_String.Text = "no items left";

                AgeTextLabel.Visibility = Visibility.Hidden;
                AgeLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Count of items loaded:" + itemList.Count.ToString());
                TextBox_String.Text = itemList.First().ToString().Replace(';', '\n');

                AgeTextLabel.Visibility = Visibility.Visible;
                AgeLabel.Visibility = Visibility.Visible;

                //lambda expression
                Action<DateTime> ShowAge = (date) => AgeLabel.Content = 
                    (((DateTime.Now.Subtract(date)).TotalDays)/365.0).ToString("0.00")
                    + " years";

                ShowAge(itemList.First().DateOfPurchase);
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

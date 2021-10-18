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
        private List<Item> ItemList = new();
        private List<LikedItemsClass> LikedItems = new();
        private string FileLocation_items = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items.txt");
        private string FileLocation_liked = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items_liked.txt");

        public Swiping()
        {
            InitializeComponent();
            LoadItems();
            RenderItem();
        }

        private void LoadItems()
        {
            //loading all items:
            StreamReader FileAllItems = new(FileLocation_items);
            Item Temp = new();

            string Line;
            while((Line = FileAllItems.ReadLine()) != null)
            {
                //extention method usage
                ItemList.Add(Temp.ParseData(Line));
            }

            FileAllItems.Close();

            //linq query to filter
            ItemList = ItemList.Where(p => p.UserID != User.CurrentUserID).ToList();

            //loading already liked items
            StreamReader FileLikedItems = new(FileLocation_liked);

            while((Line = FileLikedItems.ReadLine()) != null)
            {
                int[] tempArr = Temp.ParsedLiked(Line);
                List<int> tempList = new();
 
                for (int i = 1; i < tempArr.Length; i++)
                {
                    tempList.Add(tempArr[i]);
                }

                LikedItems.Add(new LikedItemsClass(tempArr[0], tempList));
            }

            FileLikedItems.Close();

            //filtering already liked items
            List<LikedItemsClass> ToDelete = new();
            ToDelete = LikedItems.Where(p => p.UserID == User.CurrentUserID).ToList();
            
            foreach(LikedItemsClass likedItem in ToDelete)
            {
                ItemList = ItemList.Where(p => likedItem.ItemsIDs.Contains(p.ID)==false).ToList();
            }
        }

        private void RenderItem()
        {
            MessageBox.Show("count of items loaded:"+ItemList.Count.ToString());
            TextBox_String.Text = ItemList.First().ToString().Replace(';', '\n');
            
        } 

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            ItemList.RemoveAt(0);
            RenderItem();
        }

        private void LikeItemButton_Click(object sender, RoutedEventArgs e)
        {
            //save current item
            int ViewedItemID = ItemList.First().ID;
            LikedItemsClass Temp = new();

            //update current liked item
            foreach (LikedItemsClass liked in LikedItems)
            {
                if (liked.UserID == User.CurrentUserID)
                {
                    liked.ItemsIDs.Add(ViewedItemID);
                    Temp = liked;
                }
            }

            LikedItems = LikedItems.Where(p => p.UserID != User.CurrentUserID).ToList();
            LikedItems.Add(Temp);

            //rewrite current liked items list to file
            StreamWriter file = new(FileLocation_liked);

            foreach(LikedItemsClass like in LikedItems)
            {
                file.WriteLine(like.ToString());
            }

            file.Close();

            //rendering sequence
            ItemList.RemoveAt(0);
            RenderItem();
        }

        private void AccountPageButton_Click(object sender, RoutedEventArgs e)
        {
            AccountPage AccountPageWindow = new AccountPage();
            AccountPageWindow.ShowDialog();
        }

        private void LeaderboardsPageButton_Click(object sender, RoutedEventArgs e)
        {
            LeaderboardWindow LeaderboardPageWindow = new LeaderboardWindow();
            LeaderboardPageWindow.ShowDialog();
        }

        private void CommunicationPageButton_Click(object sender, RoutedEventArgs e)
        {
            ChatWindow CommunicationPageWindow = new ChatWindow();
            CommunicationPageWindow.ShowDialog();
        }

        private void SettingsPageButton_Click(object sender, RoutedEventArgs e)
        {
            Settings SettingsPageWindow = new Settings();
            SettingsPageWindow.ShowDialog();
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
            string Result = "";

            Result += UserID;

            foreach(int i in ItemsIDs)
            {
                Result += ';' + i.ToString();
            }

            return Result;
        }
    }
}

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
            StreamReader file1 = new(FileLocation_items);
            Item temp = new();

            string line;
            while((line = file1.ReadLine()) != null)
            {
                //extention method usage
                ItemList.Add(temp.ParseData(line));
            }

            file1.Close();

            //linq query to filter
            ItemList = ItemList.Where(p => p.UserID != User.CurrentUserID).ToList();

            //loading already liked items
            StreamReader file2 = new(FileLocation_liked);

            while((line = file2.ReadLine()) != null)
            {
                int[] tempArr = temp.ParsedLiked(line);
                List<int> tempList = new();
 
                for (int i = 1; i < tempArr.Length; i++)
                {
                    tempList.Add(i);
                }
                MessageBox.Show(tempArr[2].ToString());

                LikedItems.Add(new LikedItemsClass(tempArr[0], tempList));
            }

            file2.Close();

            //TODO: bug here, filtartion from already liked doesn't work
            //trying to implement groupJoin (linq method)
            var joinedGroup = ItemList.GroupJoin(   //outter data
                LikedItems,     //inner data
                ite => ite.UserID,  //outter key selector
                like => like.UserID,    //inner key selector
                (ite, like) => new { ite, like }  //result selector
                );

            //filtering already liked items
            foreach (var item in joinedGroup)
            {
                foreach(var likedItem in item.like)
                {
                    if (likedItem.ItemsIDs.Contains(item.ite.ID))
                    {
                        ItemList.RemoveAt(ItemList.IndexOf(item.ite));
                    }
                }
            }
        }

        private void RenderItem()
        {
            TextBox_String.Text = ItemList.First().ToString().Replace(';', '\n');
            ItemList.RemoveAt(0);

            MessageBox.Show(ItemList.Count.ToString());
        } 

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            RenderItem();
        }

        private void LikeItemButton_Click(object sender, RoutedEventArgs e)
        {

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
    }

    internal class LikedItemsClass
    {
        public LikedItemsClass(int userID, List<int> itemsIDs)
        {
            UserID = userID;
            ItemsIDs = itemsIDs;
        }

        public int UserID { get; set; }

        public List<int> ItemsIDs { get; set; }
    }
}

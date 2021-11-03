using Kinder.Classes;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kinder
{
    public partial class LeaderboardWindow : Window
    {
        //testing data
        private List<UserLeaderboard> userList = new();
        
        private void UpdateUserList()
        {
            if (userList != null)
            {
                userList.Clear();
            }
            foreach(User user in FileManager.GetUsers())
            {
                userList.Add(new UserLeaderboard(user.Username, user.KarmaPoints, user.RegDate));
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            leaderboard.Items.Clear();

            //LINQ query to filter
            List<UserLeaderboard> newUserList = userList.Where(x => x.Username.Contains(MainTextBox.Text)).ToList();

            ShowData(newUserList);
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            leaderboard.Items.Clear();
            ShowData(userList);
        }

        private void ShowData(List<UserLeaderboard> tempUserList)
        {
            //linq  to sort data
            tempUserList.Sort((x, y) => y.KarmaPoints.CompareTo(x.KarmaPoints));

            int i = 0;
            foreach (UserLeaderboard a in tempUserList)
            {
                a.Place = ++i;
                leaderboard.Items.Add(a);
            }
        }

        public LeaderboardWindow()
        {
            InitializeComponent();
            UpdateUserList();
            ShowData(userList);
        }

        private class UserLeaderboard : User
        {
            public int Place { get; set; }
            public string RegistrationDateString { get; set; }

            public UserLeaderboard(string username, int karmaPoints, string registrationDate)
            {
                Username = username;
                KarmaPoints = karmaPoints;
                RegistrationDateString = registrationDate;
            }
        }
    }
}

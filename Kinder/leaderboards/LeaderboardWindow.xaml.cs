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
        private readonly List<UserLeaderboard> UserList = new()
        {
            new UserLeaderboard("username1", 100, new DateTime(2020, 12, 16)),
            new UserLeaderboard("username2", 105, new DateTime(2021, 06, 16)),
            new UserLeaderboard("username3", 245, new DateTime(2021, 08, 16)),
            new UserLeaderboard("username4", 737, new DateTime(2021, 11, 16)),
            new UserLeaderboard("username5", 727, new DateTime(2021, 12, 16)),
            new UserLeaderboard("username6", 1787, new DateTime(2021, 12, 16)),
            new UserLeaderboard("username7", 15, new DateTime(2021, 12, 16)),
            new UserLeaderboard("username8", 105, new DateTime(2021, 12, 16)),
            new UserLeaderboard("username9", 1781, new DateTime(2021, 12, 16)),
            new UserLeaderboard("username10", 332, new DateTime(2021, 12, 16))
        };

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            leaderboard.Items.Clear();

            //LINQ was used here
            List<UserLeaderboard> NewUserList = UserList.Where(x => x.Username.Contains(MainTextBox.Text)).ToList();

            ShowData(NewUserList);
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            leaderboard.Items.Clear();
            ShowData(UserList);
        }

        private void ShowData(List<UserLeaderboard> tempUserList)
        {
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

            ShowData(UserList);
        }

        private class UserLeaderboard : User
        {
            public int Place { get; set; }
            public string RegistrationDateString { get; set; }

            public UserLeaderboard(string username, int karmaPoints, DateTime registrationDate)
            {
                Username = username;
                KarmaPoints = karmaPoints;
                RegistrationDateString = GetDateStringFormat(registrationDate);
            }

            private string GetDateStringFormat(DateTime Date)
            {
                return Date.ToString("yyyy-MM-dd");
            }

        }
    }
}

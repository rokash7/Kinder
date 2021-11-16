using Kinder.Classes;
using System;
using System.Collections.Generic;
using System.Data;
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
        private List<UserLeaderboard> userList = new();

        private void UpdateUserList()
        {
            if (userList != null)
            {
                userList.Clear();
            }

            foreach (User user in FileManager.GetUsers())
            {
                userList.Add(new UserLeaderboard(user.Username, user.KarmaPoints, user.RegDate));
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            leaderboard.Items.Clear();

            List<UserLeaderboard> newUserList = userList.Where(x => x.Username.Contains(MainTextBox.Text)).ToList();

            ShowData(newUserList);
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateUserList();
            ShowData(userList);
            UpdateLatestUpdateTime();
        }

        private void UpdateLatestUpdateTime()
        {
            updatedTime.Text = "Last updated at: " + DateTime.Now;
        }

        private void ShowData(List<UserLeaderboard> tempUserList)
        {
            //sort data
            tempUserList.Sort((x, y) => y.KarmaPoints.CompareTo(x.KarmaPoints));

            int i = 0;
            foreach (UserLeaderboard a in tempUserList)
            {
                a.Place = ++i;
            }

            TableManagment.FillTable<UserLeaderboard>(ref leaderboard, tempUserList);
        }

        public LeaderboardWindow()
        {
            InitializeComponent();
            UpdateUserList();
            ShowData(userList);
            UpdateLatestUpdateTime();
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

        private void Leaderboard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UserLeaderboard currUser = null;
            try
            {
                int selIndex = leaderboard.SelectedIndex + 1;
                foreach (UserLeaderboard user in userList)
                {
                    if (user.Place == selIndex)
                    {
                        currUser = user;
                    }
                }
                if (currUser != null)
                {
                    var profilePage = new Profile(currUser.Username);
                    profilePage.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Something went wrong :/");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
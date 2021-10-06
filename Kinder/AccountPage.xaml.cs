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
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Window
    {
        public AccountPage()
        {
            InitializeComponent();
        }

        private void Settings_Click(object sender, RoutedEventArgs e) ///To do
        {
            var settingsWindow = new Settings();
            settingsWindow.ShowDialog();
        }

        private void Give_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Giving away");
        }

        private void Request_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Give");
        }

        private void Leaderboard_Click(object sender, RoutedEventArgs e)
        {
            var leaderboardWindow = new LeaderboardWindow();
            leaderboardWindow.ShowDialog();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var logInPage = new MainWindow();
            logInPage.Show();
            this.Close();
        }
    }
}

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
using System.Windows.Shapes;

namespace Kinder
{
    /// <summary>
    /// Interaction logic for Swiping.xaml
    /// </summary>
    public partial class Swiping : Window
    {
        public Swiping()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DonateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AccountPageButton_Click(object sender, RoutedEventArgs e)
        {
            AccountPage AccountPageWindow = new AccountPage();
            AccountPageWindow.Show();
        }

        private void LeaderboardsPageButton_Click(object sender, RoutedEventArgs e)
        {
            LeaderboardWindow LeaderboardPageWindow = new LeaderboardWindow();
            LeaderboardPageWindow.Show();
        }

        private void CommunicationPageButton_Click(object sender, RoutedEventArgs e)
        {
            ChatWindow CommunicationPageWindow = new ChatWindow();
            CommunicationPageWindow.Show();
        }

        private void SettingsPageButton_Click(object sender, RoutedEventArgs e)
        {
            Settings SettingsPageWindow = new Settings();
            SettingsPageWindow.Show();
        }
    }
}

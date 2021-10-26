using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            var RegPage = new RegistrationWindow();
            this.Close();
            RegPage.Show();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (User.CheckLogin(Username.Text, Password.Password))
            {
                var MainPage = new Swiping();
                MainPage.Show();
                this.Close();
            }
            else
            {
                Username.BorderBrush = Brushes.Red;
                Password.BorderBrush = Brushes.Red;
                MessageBox.Show("Incorrect password or username, try again");
            }
        }

        private void ButtonMessageTest_Click(object sender, RoutedEventArgs e)          ///Testing button
        {
            LikedItems a = new();
            a.Show();
            //LeaderboardWindow page = new LeaderboardWindow();
            //page.Show();
            //ChatWindow chatWindow = new ChatWindow();
            //chatWindow.Show();
            //Swiping page = new Swiping();
            //page.Show();                              ///Testing directories
            //string path = System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString();
            //MessageBox.Show(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString(), "Data_files"));
        }
    }
}

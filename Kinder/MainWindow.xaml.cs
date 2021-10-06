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
            var regPage = new RegistrationWindow();
            regPage.ShowDialog();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (User.checkLogin(Username.Text, Password.Password))
            {
                var mainPage = new AccountPage();
                mainPage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect password or username, try again");
            }
        }

        private void ButtonMessageTest_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

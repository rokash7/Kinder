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
using Kinder.Database;

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
            this.Close();
            regPage.Show();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (User.CheckLogin(Username.Text, Password.Password))
            {
                var mainPage = new Swiping();
                mainPage.Show();
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
            using UsersContext Context = new UsersContext();    // Adding test data for DB
            Context.Users.Add(new Users()
            {
                Username = "admin",
                Password = "admin",
                Email = "admin@gmail.com",
                PhoneNumber = "+admin",
                Name = "admin",
                Surname = "admin",
                KarmaPoints = 420,
                RegDate = "2020-12-16"
            });
            Context.Users.Add(new Users()
            {
                Username = "dude1",
                Password = "password1",
                Email = "dude1@gmail.com",
                PhoneNumber = "+12345",
                Name = "dudee",
                Surname = "dud",
                KarmaPoints = 0,
                RegDate = "2020-12-16"
            });
            Context.Users.Add(new Users()
            {
                Username = "dude2",
                Password = "password2",
                Email = "dude2@gmail.com",
                PhoneNumber = "+54321",
                Name = "dud",
                Surname = "dudee",
                KarmaPoints = 10,
                RegDate = "2020-12-16"
            });
            Context.Items.Add(new Items()
            {
                DateOfPurchase = "2020-01-05",
                Condition = ConditionEnum.Fair,
                Category = CathegoryEnum.Education,
                UserID = 1,
                Size = "1,2,1",
                KarmaPrice = 157,
                Name = "Unnamed Item",
                Description = "Undescribed Item",
            });
            Context.Items.Add(new Items()
            {
                DateOfPurchase = "2020-01-05",
                Condition = ConditionEnum.Near_mint,
                Category = CathegoryEnum.Furniture,
                UserID = 2,
                Size = "10,5,1",
                KarmaPrice = 126,
                Name = "Djungelskog",
                Description = "A rather large bear plushie",
            });
            Context.SaveChanges();
            //LikedItems a = new();
            //a.Show();
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

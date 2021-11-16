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
using Grpc.Net.Client;
//using GrpcProvider;
using Kinder.Classes;
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
            InitializeComponent();                               //abC1#321A
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            var regPage = new RegistrationWindow();
            regPage.ShowDialog();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegValidation.CheckTextBoxInput(username.Text);
                User.CheckUsername(username.Text);
                errorUsername.Visibility = Visibility.Hidden;
                username.BorderBrush = Brushes.Green;
                User.CheckPassword(password.Password);
                var newPage = new Swiping();
                newPage.Show();
                this.Close();
            }
            catch (UserDoesNotExistsException ex)
            {
                username.BorderBrush = Brushes.Red;
                errorUsername.Text = ex.Message;
                errorUsername.Visibility = Visibility.Visible;
            }
            catch (IncorrectPasswordException ex)
            {
                password.BorderBrush = Brushes.Red;
                errorPassword.Text = ex.Message;
                errorPassword.Visibility = Visibility.Visible;
            }
            catch (EmptyFieldException ex)
            {
                errorUsername.Text = ex.Message;
                errorUsername.Visibility = Visibility.Visible;
                username.BorderBrush = Brushes.Red;
            }
        }

        private void ButtonMessageTest_Click(object sender, RoutedEventArgs e)          ///Testing button
        {
            /*using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });*/
            /*using var serviceProvider = new GrpcServiceProvider();
            var client = serviceProvider.GetGreeterClient();
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });*/
            //MessageBox.Show("Greeting: " + reply.Message);
            /*using UsersContext context = new UsersContext();    // Adding test data for DB
            context.Users.Add(new Users()
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
            context.Users.Add(new Users()
            {
                Username = "dude1",
                Password = "password1",
                Email = "dude1@gmail.com",
                PhoneNumber = "+12345",
                Name = "dudee",
                Surname = "dud",
                KarmaPoints = 0,
                RegDate = "2020-12-16",
                UserItems = new List<Items>()
                {
                    new Items()
                    {
                        DateOfPurchase = "2020-01-05",
                        Condition = ConditionEnum.Fair,
                        Category = CathegoryEnum.Education,
                        Size = "1,2,1",
                        KarmaPrice = 157,
                        Name = "Unnamed Item",
                        Description = "Undescribed Item",
                    },
                    new Items()
                    {
                        DateOfPurchase = "2021-11-09",
                        Condition = ConditionEnum.Mint,
                        Category = CathegoryEnum.Technology,
                        Size = "15,20,2",
                        KarmaPrice = 157,
                        Name = "Brandname Laptop",
                        Description = "Brand new laptop from brandname.",
                    }
                }
            });
            context.Users.Add(new Users()
            {
                Username = "dude2",
                Password = "password2",
                Email = "dude2@gmail.com",
                PhoneNumber = "+54321",
                Name = "dud",
                Surname = "dudee",
                KarmaPoints = 10,
                RegDate = "2020-12-16",
                UserItems = new List<Items>()
                {
                    new Items()
                    {
                        DateOfPurchase = "2020-01-05",
                        Condition = ConditionEnum.Near_mint,
                        Category = CathegoryEnum.Furniture,
                        Size = "10,5,1",
                        KarmaPrice = 126,
                        Name = "Djungelskog",
                        Description = "A rather large bear plushie.",
                    }
                }
            });
            context.SaveChanges();*/
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

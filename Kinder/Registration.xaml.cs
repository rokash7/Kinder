using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Kinder.Classes;

namespace Kinder
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)               ///To add normal validation for email, phonenumber, username...
        {
            if (!(string.IsNullOrWhiteSpace(Email.Text) || string.IsNullOrWhiteSpace(PhoneNumber.Text)))
            {
                if (User.CheckIfUserAlreadyExists(Email.Text, PhoneNumber.Text))
                {
                    if (!string.IsNullOrWhiteSpace(Username.Text))
                    {
                        if (User.CheckIfUsernameIsTaken(Username.Text))
                        {
                            FileManager.addNewUser(Username.Text, Password.Password, Email.Text, PhoneNumber.Text, Name.Text, Surname.Text, User.getUserCount());
                            MessageBox.Show("User created successfully! Now  try to log in");
                            MainWindow loginPage = new MainWindow();
                            this.Close();
                            loginPage.Show();
                        }
                        else
                        {
                            MessageBox.Show("This username is taken!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username");
                    }   
                }
                else
                {
                    MessageBox.Show("User with same email or phone number already exists!\nIf You Already have an accoutn try logging in!");
                }
            }
            else
            {
                MessageBox.Show("Invalid email or phone number");
            }
            
        }
    }
}

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

        private void SignUpButton_Click(object sender, RoutedEventArgs e)               ///NEED ADVICE! TO DO - Maybe put all those blocks somewhere else, or try to do a multifunctial one 
        {
            int count = 0;
            if(RegValidation.CheckTextBoxInput(Username.Text))
            {
                ErrorUsername.Text = "Enter Username!";
                ErrorUsername.Visibility = Visibility.Visible;
                Username.BorderBrush = Brushes.Red;
            } else if(RegValidation.CheckIfFieldIsValid(Username.Text))
            {
                ErrorUsername.Text = "Username is not valid!";
                ErrorUsername.Visibility = Visibility.Visible;
                Username.BorderBrush = Brushes.Red;
            } else if (User.CheckIfUsernameIsTaken(Username.Text))
            {
                ErrorUsername.Text = "Username is already taken!";
                ErrorUsername.Visibility = Visibility.Visible;
                Username.BorderBrush = Brushes.Red;
            } else
            {
                ErrorUsername.Visibility = Visibility.Hidden;
                Username.BorderBrush = Brushes.Green;
                count++;
            }

            if (RegValidation.CheckTextBoxInput(Name.Text) || RegValidation.CheckTextBoxInput(Surname.Text))    ///Name/Surname
            {
                ErrorNameSurname.Text = "Enter Name and Surname!!";
                ErrorNameSurname.Visibility = Visibility.Visible;
                Name.BorderBrush = Brushes.Red;
                Surname.BorderBrush = Brushes.Red;
            } else if (RegValidation.CheckIfFieldIsValid(Name.Text) || RegValidation.CheckIfFieldIsValid(Surname.Text))
            {
                ErrorNameSurname.Text = "Name or Surname is not valid!";
                ErrorNameSurname.Visibility = Visibility.Visible;
                Name.BorderBrush = Brushes.Red;
                Surname.BorderBrush = Brushes.Red;
            } else
            {
                ErrorNameSurname.Visibility = Visibility.Hidden;
                Name.BorderBrush = Brushes.Green;
                Surname.BorderBrush = Brushes.Green;
                count++;
            }

            if (RegValidation.CheckTextBoxInput(Email.Text))            ///Email
            {
                ErrorEmail.Text = "Enter Email!";
                ErrorEmail.Visibility = Visibility.Visible;
                Email.BorderBrush = Brushes.Red;
            } else if(User.CheckIfUserAlreadyExists(Email.Text))
            {
                ErrorEmail.Text = "User already exists!\nTry to log in!";
                ErrorEmail.Visibility = Visibility.Visible;
                Email.BorderBrush = Brushes.Red;
            } else if (RegValidation.CheckIfEmailValid(Email.Text))
            {
                ErrorEmail.Text = "Name or surname is not valid!";
                ErrorEmail.Visibility = Visibility.Visible;
                Email.BorderBrush = Brushes.Red;
            } else
            {
                ErrorEmail.Visibility = Visibility.Hidden;
                Email.BorderBrush = Brushes.Green;
                count++;
            }

            if (RegValidation.CheckTextBoxInput(PhoneNumber.Text))          /// TO DO WITH ALL PHONE NUMBS. CURRENTLY ONLY LTU WORKS...
            {
                ErrorPhoneNumber.Text = "Enter PhoneNumber!";
                ErrorPhoneNumber.Visibility = Visibility.Visible;
                PhoneNumber.BorderBrush = Brushes.Red;
            } else if (User.CheckIfUserAlreadyExists(PhoneNumber.Text))
            {
                ErrorPhoneNumber.Text = "User already exists!\nTry to log in!";
                ErrorPhoneNumber.Visibility = Visibility.Visible;
                PhoneNumber.BorderBrush = Brushes.Red;
            } else if (RegValidation.CheckIfPhoneNumberValid(PhoneNumber.Text))     
            {
                ErrorPhoneNumber.Text = "PhoneNumber is not valid!";
                ErrorPhoneNumber.Visibility = Visibility.Visible;
                PhoneNumber.BorderBrush = Brushes.Red;
            }
            else
            {
                ErrorPhoneNumber.Visibility = Visibility.Hidden;
                PhoneNumber.BorderBrush = Brushes.Green;
                count++;
            }

            if (RegValidation.CheckTextBoxInput(Password.Password))         ///Maybe to do idk, NEED ADVICE!
            {
                ErrorPassword.Text = "Enter Password!";
                ErrorPassword.Visibility = Visibility.Visible;
                Password.BorderBrush = Brushes.Red;
            } else if (RegValidation.CheckIfFieldIsValid(Password.Password))
            {
                ErrorPassword.Text = "Password is not valid!";
                ErrorPassword.Visibility = Visibility.Visible;
                Password.BorderBrush = Brushes.Red;
            } else
            {
                ErrorPassword.Visibility = Visibility.Hidden;
                Password.BorderBrush = Brushes.Green;
                count++;
            }

            if(count == 5)                          ////Simple thing that says if all boxes are filled correctly
            {
                FileManager.addNewUser(Username.Text, Password.Password, Email.Text, PhoneNumber.Text, Name.Text, Surname.Text, User.getUserCount());
                MessageBox.Show("User created successfully! Now  try to log in");
                MainWindow loginPage = new MainWindow();
                this.Close();
                loginPage.Show();
            }
        }
    }
}

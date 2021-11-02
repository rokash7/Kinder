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

        private void SignUpButton_Click(object sender, RoutedEventArgs e)               ///Still need work, but basic functionality exists
        {
            int count = 0;

            try                                                     ///Username try/catch block
            {
                RegValidation.CheckTextBoxInput(Username.Text);
                RegValidation.CheckIfFieldIsValid(Username.Text);
                User.CheckIfUsernameIsTaken(Username.Text);
                ErrorUsername.Visibility = Visibility.Hidden;
                Username.BorderBrush = Brushes.Green;
                count++;
            }
            catch (EmptyFieldException ex)
            {
                ErrorUsername.Text = ex.Message;
                ErrorUsername.Visibility = Visibility.Visible;
                Username.BorderBrush = Brushes.Red;
            }
            catch (WhiteSpaceDetectedException ex)
            {
                ErrorUsername.Text = ex.Message;
                ErrorUsername.Visibility = Visibility.Visible;
                Username.BorderBrush = Brushes.Red;
            }
            catch (UsernameIsTakenException ex)
            {
                ErrorUsername.Text = ex.Message;
                ErrorUsername.Visibility = Visibility.Visible;
                Username.BorderBrush = Brushes.Red;
            }

            try                                                     ///Name/surname try/catch block
            {
                RegValidation.CheckTextBoxInput(Name.Text);
                RegValidation.CheckIfFieldIsValid(Name.Text);
                RegValidation.CheckTextBoxInput(Surname.Text);
                RegValidation.CheckIfFieldIsValid(Surname.Text);
                ErrorNameSurname.Visibility = Visibility.Hidden;
                Name.BorderBrush = Brushes.Green;
                Surname.BorderBrush = Brushes.Green;
                count++;
            }
            catch (EmptyFieldException ex)
            {
                ErrorNameSurname.Text = ex.Message;
                ErrorNameSurname.Visibility = Visibility.Visible;
                Name.BorderBrush = Brushes.Red;
                Surname.BorderBrush = Brushes.Red;
            }
            catch (WhiteSpaceDetectedException ex)
            {
                ErrorNameSurname.Text = ex.Message;
                ErrorNameSurname.Visibility = Visibility.Visible;
                Name.BorderBrush = Brushes.Red;
                Surname.BorderBrush = Brushes.Red;
            }

            try                                                     ///Email try/catch block
            {
                RegValidation.CheckTextBoxInput(Email.Text);
                RegValidation.CheckIfEmailValid(Email.Text);
                User.CheckIfUserAlreadyExists(Email.Text);
                ErrorEmail.Visibility = Visibility.Hidden;
                Email.BorderBrush = Brushes.Green;
                count++;
            }
            catch (EmptyFieldException ex)
            {
                ErrorEmail.Text = ex.Message;
                ErrorEmail.Visibility = Visibility.Visible;
                Email.BorderBrush = Brushes.Red;
            }
            catch (InvalidEmailException ex)
            {
                ErrorEmail.Text = ex.Message;
                ErrorEmail.Visibility = Visibility.Visible;
                Email.BorderBrush = Brushes.Red;
            }
            catch (UserAlreadyExistsException ex)
            {
                ErrorEmail.Text = ex.Message;
                ErrorEmail.Visibility = Visibility.Visible;
                Email.BorderBrush = Brushes.Red;
            }

            try                                                     ///Phonenumber try/catch block
            {
                RegValidation.CheckTextBoxInput(PhoneNumber.Text);
                RegValidation.CheckIfPhoneNumberValid(PhoneNumber.Text);
                User.CheckIfUserAlreadyExists(PhoneNumber.Text);
                ErrorPhoneNumber.Visibility = Visibility.Hidden;
                PhoneNumber.BorderBrush = Brushes.Green;
                count++;
            }
            catch (EmptyFieldException ex)
            {
                ErrorPhoneNumber.Text = ex.Message;
                ErrorPhoneNumber.Visibility = Visibility.Visible;
                PhoneNumber.BorderBrush = Brushes.Red;
            }
            catch (InvalidPhonenumberException ex)
            {
                ErrorPhoneNumber.Text = ex.Message;
                ErrorPhoneNumber.Visibility = Visibility.Visible;
                PhoneNumber.BorderBrush = Brushes.Red;
            }
            catch (UserAlreadyExistsException ex)
            {
                ErrorPhoneNumber.Text = ex.Message;
                ErrorPhoneNumber.Visibility = Visibility.Visible;
                PhoneNumber.BorderBrush = Brushes.Red;
            }

            if (PasswordShow.Text != Password.Password && PasswordShow.Visibility == Visibility.Visible)
            {
                Password.Password = PasswordShow.Text;
            }

            try                                                     ///Password try/catch block
            {
                RegValidation.CheckTextBoxInput(Password.Password);
                RegValidation.CheckIfFieldIsValid(Password.Password);
                RegValidation.CheckIfPasswordValid(Password.Password);
                ErrorPassword.Visibility = Visibility.Hidden;
                Password.BorderBrush = Brushes.Green;
                PasswordShow.BorderBrush = Brushes.Green;
                count++;
            }
            catch (EmptyFieldException ex)
            {
                ErrorPassword.Text = ex.Message;
                ErrorPassword.Visibility = Visibility.Visible;
                Password.BorderBrush = Brushes.Red;
                PasswordShow.BorderBrush = Brushes.Red;
            }
            catch (WhiteSpaceDetectedException ex)
            {
                ErrorPassword.Text = ex.Message;
                ErrorPassword.Visibility = Visibility.Visible;
                Password.BorderBrush = Brushes.Red;
                PasswordShow.BorderBrush = Brushes.Red;
            }
            catch (InvalidPasswordException ex)
            {
                ErrorPassword.Text = ex.Message;
                ErrorPassword.Visibility = Visibility.Visible;
                Password.BorderBrush = Brushes.Red;
                PasswordShow.BorderBrush = Brushes.Red;
            }

            if (count == 5)                          ////Simple thing that says if all boxes are filled correctly
            {
                FileManager.AddUserIDToLiked(User.getUserCount());
                FileManager.AddNewUser(Username.Text, Password.Password, Email.Text, PhoneNumber.Text, Name.Text, Surname.Text, User.getUserCount(), DateTime.Now.ToString("yyyy-MM-dd"));
                MessageBox.Show("User created successfully! Now  try to log in");
                MainWindow LoginPage = new MainWindow();
                this.Close();
                LoginPage.Show();
            }
        }

        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordShow.Visibility == Visibility.Hidden)
            {
                PasswordShow.Visibility = Visibility.Visible;
                Password.Visibility = Visibility.Hidden;
                PasswordShow.Text = Password.Password;
            }
            else
            {
                PasswordShow.Visibility = Visibility.Hidden;
                Password.Visibility = Visibility.Visible;
                Password.Password = PasswordShow.Text;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

            errorUsername.Visibility = Visibility.Hidden;
            username.BorderBrush = Brushes.Green;
            try                                                     ///Username try/catch block
            {
                RegValidation.CheckTextBoxInput(username.Text);
                RegValidation.CheckIfFieldIsValid(username.Text);
                User.CheckIfUsernameIsTaken(username.Text);
                count++;
            }
            catch (EmptyFieldException ex)
            {
                errorUsername.Text = ex.Message;
                errorUsername.Visibility = Visibility.Visible;
                username.BorderBrush = Brushes.Red;
            }
            catch (WhiteSpaceDetectedException ex)
            {
                errorUsername.Text = ex.Message;
                errorUsername.Visibility = Visibility.Visible;
                username.BorderBrush = Brushes.Red;
            }
            catch (UsernameIsTakenException ex)
            {
                errorUsername.Text = ex.Message;
                errorUsername.Visibility = Visibility.Visible;
                username.BorderBrush = Brushes.Red;
            }

            errorNameSurname.Visibility = Visibility.Hidden;
            name.BorderBrush = Brushes.Green;
            surname.BorderBrush = Brushes.Green;
            try                                                     ///Name/surname try/catch block
            {
                RegValidation.CheckTextBoxInput(name.Text);
                RegValidation.CheckIfFieldIsValid(name.Text);
                RegValidation.CheckTextBoxInput(surname.Text);
                RegValidation.CheckIfFieldIsValid(surname.Text);
                count++;
            }
            catch (EmptyFieldException ex)
            {
                errorNameSurname.Text = ex.Message;
                errorNameSurname.Visibility = Visibility.Visible;
                name.BorderBrush = Brushes.Red;
                surname.BorderBrush = Brushes.Red;
            }
            catch (WhiteSpaceDetectedException ex)
            {
                errorNameSurname.Text = ex.Message;
                errorNameSurname.Visibility = Visibility.Visible;
                name.BorderBrush = Brushes.Red;
                surname.BorderBrush = Brushes.Red;
            }

            errorEmail.Visibility = Visibility.Hidden;
            email.BorderBrush = Brushes.Green;
            try                                                     ///Email try/catch block
            {
                RegValidation.CheckTextBoxInput(email.Text);
                RegValidation.CheckIfEmailValid(email.Text);
                User.CheckIfUserAlreadyExists(email.Text);
                count++;
            }
            catch (EmptyFieldException ex)
            {
                errorEmail.Text = ex.Message;
                errorEmail.Visibility = Visibility.Visible;
                email.BorderBrush = Brushes.Red;
            }
            catch (InvalidEmailException ex)
            {
                errorEmail.Text = ex.Message;
                errorEmail.Visibility = Visibility.Visible;
                email.BorderBrush = Brushes.Red;
            }
            catch (UserAlreadyExistsException ex)
            {
                errorEmail.Text = ex.Message;
                errorEmail.Visibility = Visibility.Visible;
                email.BorderBrush = Brushes.Red;
            }

            errorPhoneNumber.Visibility = Visibility.Hidden;
            phoneNumber.BorderBrush = Brushes.Green;
            try                                                     ///Phonenumber try/catch block
            {
                RegValidation.CheckTextBoxInput(phoneNumber.Text);
                RegValidation.CheckIfPhoneNumberValid(phoneNumber.Text);
                User.CheckIfUserAlreadyExists(phoneNumber.Text);
                count++;
            }
            catch (EmptyFieldException ex)
            {
                errorPhoneNumber.Text = ex.Message;
                errorPhoneNumber.Visibility = Visibility.Visible;
                phoneNumber.BorderBrush = Brushes.Red;
            }
            catch (InvalidPhonenumberException ex)
            {
                errorPhoneNumber.Text = ex.Message;
                errorPhoneNumber.Visibility = Visibility.Visible;
                phoneNumber.BorderBrush = Brushes.Red;
            }
            catch (UserAlreadyExistsException ex)
            {
                errorPhoneNumber.Text = ex.Message;
                errorPhoneNumber.Visibility = Visibility.Visible;
                phoneNumber.BorderBrush = Brushes.Red;
            }

            if (passwordShow.Text != password.Password && passwordShow.Visibility == Visibility.Visible)
            {
                password.Password = passwordShow.Text;
            }

            errorPassword.Visibility = Visibility.Hidden;
            password.BorderBrush = Brushes.Green;
            passwordShow.BorderBrush = Brushes.Green;
            try                                                     ///Password try/catch block
            {
                RegValidation.CheckTextBoxInput(password.Password);
                RegValidation.CheckIfFieldIsValid(password.Password);
                RegValidation.CheckIfPasswordValid(password.Password);
                count++;
            }
            catch (EmptyFieldException ex)
            {
                errorPassword.Text = ex.Message;
                errorPassword.Visibility = Visibility.Visible;
                password.BorderBrush = Brushes.Red;
                passwordShow.BorderBrush = Brushes.Red;
            }
            catch (WhiteSpaceDetectedException ex)
            {
                errorPassword.Text = ex.Message;
                errorPassword.Visibility = Visibility.Visible;
                password.BorderBrush = Brushes.Red;
                passwordShow.BorderBrush = Brushes.Red;
            }
            catch (InvalidPasswordException ex)
            {
                errorPassword.Text = ex.Message;
                errorPassword.Visibility = Visibility.Visible;
                password.BorderBrush = Brushes.Red;
                passwordShow.BorderBrush = Brushes.Red;
            }

            if (count == 5)                          ////Simple thing that says if all boxes are filled correctly
            {
                FileManager.AddUserIDToLiked(User.GetUserCount());
                FileManager.AddNewUser(username.Text, User.HashPassword(password.Password), email.Text, phoneNumber.Text, name.Text, surname.Text, User.GetUserCount(), DateTime.Now.ToString("yyyy-MM-dd"));
                MessageBox.Show("User created successfully! Now  try to log in");
                var loginPage = new MainWindow();
                this.Close();
                loginPage.Show();
            }
        }

        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (passwordShow.Visibility == Visibility.Hidden)
            {
                passwordShow.Visibility = Visibility.Visible;
                password.Visibility = Visibility.Hidden;
                passwordShow.Text = password.Password;
            }
            else
            {
                passwordShow.Visibility = Visibility.Hidden;
                password.Visibility = Visibility.Visible;
                password.Password = passwordShow.Text;
            }
        }
    }
}

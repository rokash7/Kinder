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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void ChangeEmail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegValidation.CheckIfEmailValid(newEmail.Text);
                User.ChangeUserEmail(newEmail.Text);
                MessageBox.Show("Email changed to " + newEmail.Text + ".");
            }
            catch (InvalidEmailException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangePhoneNumber_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegValidation.CheckIfPhoneNumberValid(newPhoneNumber.Text);
                User.ChangeUserPhoneNumber(newPhoneNumber.Text);
                MessageBox.Show("Phone Number changed to " + newPhoneNumber.Text + ".");
            }
            catch (InvalidPhonenumberException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (newVisiblePassword.Text != newPassword.Password && newVisiblePassword.Visibility == Visibility.Visible)
                {
                    newPassword.Password = newVisiblePassword.Text;
                }
                RegValidation.CheckTextBoxInput(newPassword.Password);
                RegValidation.CheckIfFieldIsValid(newPassword.Password);
                RegValidation.CheckIfPasswordValid(newPassword.Password);
                User.CheckPassword(CurrentPassword.Password);

                User.ChangeUserPassword(User.HashPassword(newPassword.Password));
                MessageBox.Show("Password changed");
            }
            catch (EmptyFieldException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (WhiteSpaceDetectedException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (InvalidPasswordException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (IncorrectPasswordException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (newVisiblePassword.Visibility == Visibility.Hidden)
            {
                newVisiblePassword.Visibility = Visibility.Visible;
                newPassword.Visibility = Visibility.Hidden;
                newVisiblePassword.Text = newPassword.Password;
            }
            else
            {
                newVisiblePassword.Visibility = Visibility.Hidden;
                newPassword.Visibility = Visibility.Visible;
                newPassword.Password = newVisiblePassword.Text;
            }
        }
    }
}

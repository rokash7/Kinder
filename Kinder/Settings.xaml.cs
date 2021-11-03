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
                RegValidation.CheckIfEmailValid(NewEmail.Text);
                User.ChangeUserEmail(NewEmail.Text);
                MessageBox.Show("Email changed to " + NewEmail.Text + ".");
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
                RegValidation.CheckIfPhoneNumberValid(NewPhoneNumber.Text);
                User.ChangeUserPhoneNumber(NewPhoneNumber.Text);
                MessageBox.Show("Phone Number changed to " + NewPhoneNumber.Text + ".");
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
                RegValidation.CheckTextBoxInput(NewPassword.Password);
                RegValidation.CheckIfFieldIsValid(NewPassword.Password);
                RegValidation.CheckIfPasswordValid(NewPassword.Password);
                User.CheckPassword(CurrentPassword.Password);
                
                User.ChangeUserPassword(NewPassword.Password);
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
    }
}

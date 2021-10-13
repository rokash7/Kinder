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
            if (!string.IsNullOrWhiteSpace(NewEmail.Text))
            {
                User.ChangeUserEmail(NewEmail.Text);
                MessageBox.Show("Email changed to " + NewEmail.Text + ".");
            }
            else
            {
                MessageBox.Show("Invalid Email.");
            }
        }

        private void ChangePhoneNumber_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewPhoneNumber.Text))
            {
                User.ChangeUserPhoneNumber(NewPhoneNumber.Text);
                MessageBox.Show("Phone Number changed to " + NewPhoneNumber.Text + ".");
            }
            else
            {
                MessageBox.Show("Invalid Phone Number.");
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewPassword.Password))
            {
                if (User.CheckPassword(CurrentPassword.Password))
                {
                    User.ChangeUserPassword(NewPassword.Password);
                    MessageBox.Show("Password changed.");
                }
                else
                {
                    MessageBox.Show("Incorrect password!");
                }
            }
            else
            {
                MessageBox.Show("Invalid Password.");
            }
        }
    }
}

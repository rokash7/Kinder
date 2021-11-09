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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        public Profile(string username)
        {
            InitializeComponent();
            DataContext = User.GetUserByUsername(username);
        }

        private void ProfileBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ProfileItems_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Currently this feature does not work");

        }
    }
}

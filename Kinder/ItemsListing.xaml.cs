using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ItemsListing.xaml
    /// </summary>
    public partial class ItemsListing : Window
    {
        public ItemsListing()
        {
            InitializeComponent();
            ReadDataFromFile();
            DisplayData();
        }

        private List<Item> ItemsList = new();
        private int CurrentUserID = User.CurrentUserID;
        private string FileLocation = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items.txt");

        //reading file
        private void ReadDataFromFile()
        {
            ItemsList.Clear();

            StreamReader file = new(FileLocation);
            Item temp = new();

            string line;
            while ((line = file.ReadLine()) != null)
            {
                
                ItemsList.Add(temp.ParseData(line));
            }

            file.Close();
        }

        private void DisplayData()
        {
            ItemsList.Sort();
            ItemsTable.Items.Clear();

            foreach(Item item in ItemsList.Where(p => p.UserID == CurrentUserID))
            {
                ItemsTable.Items.Add(item);
            }
        }
        
        private void ReWriteFile()
        {
            StreamWriter file = new(FileLocation);

            foreach(Item item in ItemsList)
            {
                file.WriteLine(item.ToString());
            }
            file.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ItemsList.Sort();
            Item NewItem = new();

            int nextID = ItemsList[0].ID + 1; //list is sorted by IDs, so it's the first element has highest ID

            NewItem.ID = nextID;

            NewItem.DateOfPurchase = DateTime.Parse(DateTextBox.Text);
            NewItem.DateStr = DateTextBox.Text;

            String condStr= ConditionComboBox.SelectedItem.ToString();
            Enum.TryParse(condStr, out ConditionEnum condition);
            NewItem.Condition = condition;

            String catStr = CathegoryComboBox.SelectedItem.ToString();
            Enum.TryParse(catStr, out CathegoryEnum cathegory);
            NewItem.Cathegory = cathegory;

            NewItem.UserID = CurrentUserID;

            //narrowing convertion:
            long length_long, width_long, height_long;

            length_long = long.Parse(DimsTextBoxL.Text);
            width_long = long.Parse(DimsTextBoxW.Text);
            height_long = long.Parse(DimsTextBoxH.Text);

            int length_int, width_int, height_int;

            checked
            {
                length_int = (int)length_long;
                width_int = (int)width_long;
                height_int = (int)height_long;
            }

            //named argument:
            NewItem.Size = new Dimensions(Length: length_int, Height: height_int, Width: width_int);
            NewItem.SizeStr = NewItem.Size.ToString();
            
            //widening convertion:
            NewItem.KarmaPrice = byte.Parse(PointsTextBox.Text);

            /* such convertion allows us to keep points distribution in check. 
             * Users can't get more that 255 points per item.
             * with int they could get way way more */

            //optional arguments implementation:
            if(NameTextBox.Text.Length > 0)
            {
                NewItem.SetName(NameTextBox.Text);
            }
            else
            {
                NewItem.SetName();
            }

            if (DescTextBox.Text.Length > 0)
            {
                NewItem.SetDescription(DescTextBox.Text);
            }
            else
            {
                NewItem.SetDescription();
            }

            StreamWriter write = File.AppendText(FileLocation);
            write.WriteLine(NewItem.ToString());
            write.Close();

            ReadDataFromFile();
            DisplayData();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Item classObj = ItemsTable.SelectedItem as Item;

            ItemsList.RemoveAt(ItemsList.IndexOf(classObj));

            ReWriteFile();
            DisplayData();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Item classObj = ItemsTable.SelectedItem as Item;
            ItemsList.RemoveAt(ItemsList.IndexOf(classObj));

            ReWriteFile();

            if (DateTextBox.Text.Length > 0)
            {
                classObj.DateOfPurchase = DateTime.Parse(DateTextBox.Text);
                classObj.DateStr = DateTextBox.Text;
            }

            if(ConditionComboBox.SelectedItem != null)
            { 
                String condStr = ConditionComboBox.SelectedItem.ToString(); 
                Enum.TryParse(condStr, out ConditionEnum condition);
                classObj.Condition = condition; 
            }
            

            if(CathegoryComboBox.SelectedItem != null)
            {
                String catStr = CathegoryComboBox.SelectedItem.ToString();
                Enum.TryParse(catStr, out CathegoryEnum cathegory);
                classObj.Cathegory = cathegory;
            }

            if(DimsTextBoxL.Text.Length > 0)
            {
                classObj.size.Length = int.Parse(DimsTextBoxL.Text);
                classObj.SizeStr = classObj.Size.ToString();
            }

            if (DimsTextBoxH.Text.Length > 0)
            {
                classObj.size.Height = int.Parse(DimsTextBoxH.Text);
                classObj.SizeStr = classObj.Size.ToString();
            }

            if (DimsTextBoxW.Text.Length > 0)
            {
                classObj.size.Width = int.Parse(DimsTextBoxW.Text);
                classObj.SizeStr = classObj.Size.ToString();
            }

            if (PointsTextBox.Text.Length > 0)
            {
                classObj.KarmaPrice = byte.Parse(PointsTextBox.Text);
            }

            if (NameTextBox.Text.Length > 0)
            {
                classObj.Name = NameTextBox.Text;
            }

            if (DescTextBox.Text.Length > 0)
            {
                classObj.Description = DescTextBox.Text;
            }

            StreamWriter write = File.AppendText(FileLocation);
            write.WriteLine(classObj.ToString());
            write.Close();

            ReadDataFromFile();
            DisplayData();
        }
    }
}

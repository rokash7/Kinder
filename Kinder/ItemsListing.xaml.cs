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
        private int CurrentUserID = 1; //TODO: current user session
        private string FileLocation = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items.txt");

        private void ReadDataFromFile()
        {
            ItemsList.Clear();

            StreamReader file = new(FileLocation);

            string line;
            while ((line = file.ReadLine()) != null)
            {
                ItemsList.Add(ParseData(line));
            }

            file.Close();
        }

        private void DisplayData()
        {
            ItemsList.Sort();
            ItemsTable.Items.Clear();

            foreach(Item item in ItemsList.Where(p => p.userID == CurrentUserID))
            {
                ItemsTable.Items.Add(item);
            }
        }

        private Item ParseData(string line)
        {
            Item result = new();

            string[] data = line.Split(';');

            result.ID = int.Parse(data[0]);

            result.dateOfPurchase = DateTime.Parse(data[1]);
            result.dateStr = data[1];

            result.Condition = (ConditionEnum) Enum.Parse(typeof(ConditionEnum), data[2]);
            result.Cathegory = (CathegoryEnum) Enum.Parse(typeof(CathegoryEnum), data[3]);
            result.userID = int.Parse(data[4]);

            string[] dimsParsed = data[5].Split(',');
            int l= int.Parse(dimsParsed[0]) , w= int.Parse(dimsParsed[1]), h= int.Parse(dimsParsed[2]);
            result.size = new Dimensions(l, h, w);
            result.SizeStr = result.size.ToString();

            result.karmaPrice = int.Parse(data[6]);

            return result;
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
            NewItem.dateStr = DateTextBox.Text;

            String condStr= ConditionComboBox.SelectedItem.ToString();
            Enum.TryParse(condStr, out ConditionEnum condition);
            NewItem.Condition = condition;

            String catStr = CathegoryComboBox.SelectedItem.ToString();
            Enum.TryParse(catStr, out CathegoryEnum cathegory);
            NewItem.Cathegory = cathegory;

            NewItem.userID = CurrentUserID;

            String[] dims = DimsTextBox.Text.Split(',');
            NewItem.Size = new Dimensions(int.Parse(dims[0]), int.Parse(dims[1]), int.Parse(dims[2]));
            NewItem.SizeStr = NewItem.Size.ToString();
            
            NewItem.KarmaPrice = int.Parse(PointsTextBox.Text);

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
                classObj.dateStr = DateTextBox.Text;
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

            if(DimsTextBox.Text.Length > 0)
            {
                String[] dims = DimsTextBox.Text.Split(',');
                classObj.Size = new Dimensions(int.Parse(dims[0]), int.Parse(dims[1]), int.Parse(dims[2]));
                classObj.SizeStr = classObj.Size.ToString();
            }

            if (PointsTextBox.Text.Length > 0)
            {
                classObj.KarmaPrice = int.Parse(PointsTextBox.Text);
            }

            StreamWriter write = File.AppendText(FileLocation);
            write.WriteLine(classObj.ToString());
            write.Close();

            ReadDataFromFile();
            DisplayData();

            //TODO: bug with dimensions, they are getting mixed up somehow
        }
    }
}

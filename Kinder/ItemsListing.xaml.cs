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
using Kinder.Database;

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

        private List<Item> itemsList = new();
        private int currentUserID = User.CurrentUserID;
        private string fileLocation = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items.txt");
        private string fileLocation_liked = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data_files\\Items_liked.txt");

        //Dependency injection C1
        FileManager fileManager = new();

        private void ReadDataFromFile()
        {
            //Dependency injection C2
            itemsList.Clear();
            itemsList = fileManager.GetAllItems(new ParsingOperations());
        }

        private void DisplayData()
        {
            itemsList.Sort();
            itemsTable.Items.Clear();

            foreach (Item item in itemsList.Where(p => p.UserID == currentUserID))
            {
                itemsTable.Items.Add(item);
            }
        }

        private void ReWriteFile(int itemID = -1)
        {
            //rewriting items file:
            using (StreamWriter file = new(fileLocation))
            {
                foreach (Item item in itemsList)
                {
                    file.WriteLine(item.ToString());
                }
            }

            //rewrite liked items file:
            if (itemID != -1)
            {
                Item temp = new();
                List<LikedItemsClass> tempList = new();

                //read current liked
                using (StreamReader reader = new StreamReader(fileLocation_liked))
                {
                    while (!reader.EndOfStream)
                    {
                        int[] tempArr = fileManager.GetAllLikedItems(new ParsingOperations(), reader.ReadLine());
                        List<int> newTempList = new();

                        for (int i = 1; i < tempArr.Length; i++)
                        {
                            newTempList.Add(tempArr[i]);
                        }

                        tempList.Add(new LikedItemsClass(tempArr[0], newTempList));
                    }
                }

                //delete the absent ones
                for (int i = 0; i < tempList.Count; i++)
                {
                    tempList[i].ItemsIDs.Remove(itemID);
                }

                //upload updated liked list to file
                using (StreamWriter writer = new StreamWriter(fileLocation_liked))
                {
                    foreach (LikedItemsClass liked in tempList)
                    {
                        writer.WriteLine(liked.ToString());
                    }
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //Icomparable:
            itemsList.Sort();
            Item newItem = new();

            int nextID = itemsList[0].ID + 1; //list is sorted by IDs, so it's the first element has highest ID

            newItem.ID = nextID;

            newItem.DateOfPurchase = DateTime.Parse(dateTextBox.Text);
            newItem.DateStr = dateTextBox.Text;

            String condStr = conditionComboBox.SelectedItem.ToString();
            Enum.TryParse(condStr, out ConditionEnum condition);
            newItem.Condition = condition;

            String catStr = cathegoryComboBox.SelectedItem.ToString();
            Enum.TryParse(catStr, out CathegoryEnum cathegory);
            newItem.Cathegory = cathegory;

            newItem.UserID = currentUserID;

            //narrowing convertion:
            long length_long, width_long, height_long;

            length_long = long.Parse(dimsTextBoxL.Text);
            width_long = long.Parse(dimsTextBoxW.Text);
            height_long = long.Parse(dimsTextBoxH.Text);

            int length_int, width_int, height_int;

            checked
            {
                length_int = (int)length_long;
                width_int = (int)width_long;
                height_int = (int)height_long;
            }

            //named argument:
            newItem.Size = new Dimensions(length: length_int, height: height_int, width: width_int);
            newItem.SizeStr = newItem.Size.ToString();
            
            //widening convertion:
            newItem.KarmaPrice = byte.Parse(pointsTextBox.Text);

            /* such convertion allows us to keep points distribution in check. 
             * Users can't get more that 255 points per item.
             * with int they could get way way more */

            //optional arguments implementation:
            if(nameTextBox.Text.Length > 0)
            {
                newItem.SetName(nameTextBox.Text);
            }
            else
            {
                newItem.SetName();
            }

            if (descTextBox.Text.Length > 0)
            {
                newItem.SetDescription(descTextBox.Text);
            }
            else
            {
                newItem.SetDescription();
            }

            StreamWriter write = File.AppendText(fileLocation);
            write.WriteLine(newItem.ToString());
            write.Close();

            /*using UsersContext context = new UsersContext();
            var CurrentUser = context
                .Users
                .Where(u => u.ID == User.CurrentUserID);
            new Items();
            context.Items.Add(new Items()
            {
                DateOfPurchase = newItem.DateStr,
                Condition = newItem.Condition,
                Category = newItem.Cathegory,
                Size = newItem.SizeStr,
                KarmaPrice = newItem.KarmaPrice,
                Name = newItem.Name,
                Description = newItem.Description,
            });
            context.SaveChanges();*/

            ReadDataFromFile();
            DisplayData();
        }
                
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //saving item
            Item classObj = itemsTable.SelectedItem as Item;

            itemsList.RemoveAt(itemsList.IndexOf(classObj));

            ReWriteFile(classObj.ID);
            DisplayData();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Item classObj = itemsTable.SelectedItem as Item;
            itemsList.RemoveAt(itemsList.IndexOf(classObj));

            ReWriteFile();

            if (dateTextBox.Text.Length > 0)
            {
                classObj.DateOfPurchase = DateTime.Parse(dateTextBox.Text);
                classObj.DateStr = dateTextBox.Text;
            }

            if(conditionComboBox.SelectedItem != null)
            { 
                String condStr = conditionComboBox.SelectedItem.ToString(); 
                Enum.TryParse(condStr, out ConditionEnum condition);
                classObj.Condition = condition; 
            }
            
            if(cathegoryComboBox.SelectedItem != null)
            {
                String catStr = cathegoryComboBox.SelectedItem.ToString();
                Enum.TryParse(catStr, out CathegoryEnum cathegory);
                classObj.Cathegory = cathegory;
            }

            if(dimsTextBoxL.Text.Length > 0)
            {
                string[] str = classObj.SizeStr.Split(',');
                str[0] = dimsTextBoxL.Text;
                classObj.SizeStr = str[0] + ',' + str[1] + ',' + str[2]; 
            }

            if (dimsTextBoxH.Text.Length > 0)
            {
                string[] str = classObj.SizeStr.Split(',');
                str[1] = dimsTextBoxH.Text;
                classObj.SizeStr = str[0] + ',' + str[1] + ',' + str[2];
            }

            if (dimsTextBoxW.Text.Length > 0)
            {
                string[] str = classObj.SizeStr.Split(',');
                str[2] = dimsTextBoxW.Text;
                classObj.SizeStr = str[0] + ',' + str[1] + ',' + str[2];
            }

            if (pointsTextBox.Text.Length > 0)
            {
                classObj.KarmaPrice = byte.Parse(pointsTextBox.Text);
            }

            if (nameTextBox.Text.Length > 0)
            {
                classObj.Name = nameTextBox.Text;
            }

            if (descTextBox.Text.Length > 0)
            {
                classObj.Description = descTextBox.Text;
            }

            StreamWriter write = File.AppendText(fileLocation);
            write.WriteLine(classObj.ToString(classObj.SizeStr));
            write.Close();

            ReadDataFromFile();
            DisplayData();
        }
    }
}

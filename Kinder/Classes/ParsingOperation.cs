using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinder.Classes
{
    //Dependency injection A
    public interface IGetItems
    {
        List<Item> GetItems();
    }

    public interface IParsedLiked
    {
        int[] ParsedLiked(string line);
    }

    public class ParsingOperations : IGetItems, IParsedLiked
    {
        static string path = System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString();
        static string fileLocation_items = System.IO.Path.Combine(path, "Data_files\\Items.txt");
        static string fileLocation = System.IO.Path.Combine(path, "Data_files\\Users.txt");

        public List<Item> GetItems()
        {
            List<Item> temp = new();

            using (StreamReader file = new(fileLocation_items))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    temp.Add(ParseItemDataLine(line));
                }
            }

            return temp;
        }

        private Item ParseItemDataLine(string line)
        {
            Item result = new();

            string[] data = line.Split(';');

            result.ID = int.Parse(data[0]);

            result.DateOfPurchase = DateTime.Parse(data[1]);
            result.DateStr = data[1];

            result.Condition = (ConditionEnum)Enum.Parse(typeof(ConditionEnum), data[2]);
            result.Cathegory = (CathegoryEnum)Enum.Parse(typeof(CathegoryEnum), data[3]);
            result.UserID = int.Parse(data[4]);

            string[] dimsParsed = data[5].Split(',');

            //named argument usage:
            result.Size = new Dimensions(length: int.Parse(dimsParsed[0]), height: int.Parse(dimsParsed[1]), width: int.Parse(dimsParsed[2]));
            result.SizeStr = result.Size.ToString();

            result.KarmaPrice = int.Parse(data[6]);

            result.SetName(data[7]);
            result.SetDescription(data[8]);

            return result;
        }

        public int[] ParsedLiked(string line)
        {
            string[] parts = line.Split(';');
            int[] result = new int[parts.Length];

            int i = 0;
            foreach (string part in parts)
            {
                result[i] = int.Parse(part);
                i++;
            }

            return result;
        }
    }
}
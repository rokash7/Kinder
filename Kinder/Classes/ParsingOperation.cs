using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinder.Classes
{
    static class ParsingOperation
    {
        public static Item ParseData(this Item item, string line)
        {
            Item result = new();

            string[] data = line.Split(';');

            result.ID = int.Parse(data[0]);

            result.dateOfPurchase = DateTime.Parse(data[1]);
            result.DateStr = data[1];

            result.Condition = (ConditionEnum)Enum.Parse(typeof(ConditionEnum), data[2]);
            result.Cathegory = (CathegoryEnum)Enum.Parse(typeof(CathegoryEnum), data[3]);
            result.UserID = int.Parse(data[4]);

            string[] dimsParsed = data[5].Split(',');

            //named argument usage:
            result.size = new Dimensions(Length: int.Parse(dimsParsed[0]), Height: int.Parse(dimsParsed[1]), Width: int.Parse(dimsParsed[2]));
            result.SizeStr = result.size.ToString();

            result.karmaPrice = int.Parse(data[6]);

            result.SetName(data[7]);
            result.SetDescription(data[8]);

            return result;
        }

        public static int[] ParsedLiked(this Item item, string line)
        {
            string[] parts = line.Split(';');
            int[] result = new int[parts.Length];

            int i = 0;
            foreach(string part in parts)
            {
                result[i] = int.Parse(part);
                i++;
            }

            return result;
        }
    }
}

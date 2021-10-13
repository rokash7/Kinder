using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinder.Classes
{
    internal interface ITechnology
    {
        bool CalcWarrantyValidation();
    }

    internal interface ITransport
    {
        int CalcTimeLeftForCheckUp();
    }

    internal interface IFurniture
    {
        double CalcArea();
    }

    public struct Dimensions
    {
        public double length;
        public double height;
        public double width;

        public Dimensions(double length, double height, double width)
        {
            this.height = height;
            this.length = length;
            this.width = width;
        }

        public string ToString()
        {
            return length.ToString() + ',' + height.ToString() + ',' + width.ToString();
        }
    }

    public class Item : IComparable<Item>, IEquatable<Item>
    {
        public Item()
        {
           
        }

        public Item(int ID, DateTime dateOfPurchase, ConditionEnum Condition, CathegoryEnum Cathegory, int userID, Dimensions size, int karmaPrice)
        {
            this.ID = ID;
            this.dateOfPurchase = dateOfPurchase;
            this.Condition = Condition;
            this.Cathegory = Cathegory;
            this.userID = userID;
            this.size = size;
            this.karmaPrice = karmaPrice;
        }

        public int CompareTo(Item other)
        {
            return other.ID.CompareTo(this.ID); // default: high to low
        }

        public override string ToString()
        {
            string str = "";

            str += ID.ToString() + ';';
            str += dateOfPurchase.ToString("yyyy-MM-dd") + ';';
            str += Condition.ToString() + ';';
            str += Cathegory.ToString() + ';';
            str += userID.ToString() + ';';
            str += size.height.ToString() + ',' + size.length.ToString() + ',' + size.width.ToString() + ';';
            str += karmaPrice.ToString();

            return str;
        }

        public bool Equals(Item other)
        {
            if (other == null) return false;
            return (this.ID.Equals(other.ID));
        }

        public int ID { get; set; }

        public DateTime dateOfPurchase;
        public DateTime DateOfPurchase
        {
            get
            {
                return dateOfPurchase;
            }
            set
            {
                if (value > DateTime.Today)
                    throw new ArgumentOutOfRangeException("Did this item broke time laws?");
                else
                    dateOfPurchase = value;
            }
        }
        public string dateStr { get; set; }

        public ConditionEnum Condition { get; set; }
        public CathegoryEnum Cathegory { get; set; }

        public int userID;

        public Dimensions size;
        public Dimensions Size
        {
            get
            {
                return size;
            }
            set
            {
                if (value.height < 0 || value.width < 0 || value.length < 0)
                    throw new ArgumentOutOfRangeException("The values of the item cannot be negative");
                else
                    size = value;
            }
        }
        public string SizeStr { get; set; }

        public int karmaPrice;
        public int KarmaPrice
        {
            get
            {
                return karmaPrice;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("The value of the item cannot be negative");
                else
                    karmaPrice = value;
            }
        }
    }
}

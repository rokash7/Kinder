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
        public int Length;
        public int Height;
        public int Width;

        public Dimensions(int Length, int Height, int Width)
        {
            this.Length = Length;
            this.Height = Height;
            this.Width = Width;
        }

        public override string ToString()
        {
            return Length.ToString() + ',' + Height.ToString() + ',' + Width.ToString();
        }
    }

    public class Item : IComparable<Item>, IEquatable<Item>
    {
        public Item()
        {
           
        }

        public Item(int ID, DateTime DateOfPurchase, ConditionEnum Condition, CathegoryEnum Cathegory, int UserID, Dimensions size, int KarmaPrice)
        {
            this.ID = ID;
            this.dateOfPurchase = DateOfPurchase;
            this.Condition = Condition;
            this.Cathegory = Cathegory;
            this.UserID = UserID;
            this.size = size;
            this.karmaPrice = KarmaPrice;
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
            str += UserID.ToString() + ';';
            str += size.ToString() + ';';
            str += karmaPrice.ToString() + ';';
            str += Name.ToString() + ';';
            str += Description.ToString();

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
        public string DateStr { get; set; }

        public ConditionEnum Condition { get; set; }
        public CathegoryEnum Cathegory { get; set; }

        public int UserID { get; set; }

        public Dimensions size;
        public Dimensions Size
        {
            get
            {
                return size;
            }
            set
            {
                if (value.Height < 0 || value.Width < 0 || value.Length < 0)
                    throw new ArgumentOutOfRangeException("The values of the item cannot be negative");
                else
                    size = value;
            }
        }
        public string SizeStr { get; set; }

        //optional arguments:
        public string Name { get; set; }
        public void SetName(string NameArg = "Unnamed item") => Name = NameArg;

        public string Description { get; set; }
        public void SetDescription(string DescArg = "Undescribed item") => Description = DescArg;

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

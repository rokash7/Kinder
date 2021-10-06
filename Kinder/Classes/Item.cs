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
    
    public interface ICondition
    {
        public enum options
        {
            Mint,
            Near_mint,
            Very_good,
            Good,
            Fair
        }
    }

    public interface ICathegory
    {
        public enum options
        {
            Furniture,
            Transport,
            Technology,
            Education
        }
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
    }

    public class Item : ICondition, ICathegory
    {
        public Item()
        {
           
        }

        public Item(int ID, DateTime dateOfPurchase, ICondition.options Condition, ICathegory.options Cathegory, int userID, Dimensions size, int karmaPrice)
        {
            this.ID = ID;
            this.dateOfPurchase = dateOfPurchase;
            this.Condition = Condition;
            this.Cathegory = Cathegory;
            this.userID = userID;
            this.size = size;
            this.karmaPrice = karmaPrice;
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


        public ICondition.options Condition { get; set; }
        public ICathegory.options Cathegory{ get; set; }

        public int userID;
        public int UserID
        {
            get
            {
                return userID;
            }
        }

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

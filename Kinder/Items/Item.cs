using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinder.Items
{
    public enum Conditions_options
    {
        Mint,
        Near_mint,
        Very_good,
        Good,
        Fair
    }

    internal interface ITechnology
    {
        double CalcPowerConsuption();
    }

    internal interface ITransport
    {
        int CalcTimeLeftForCheckUp();
    }

    internal interface IFurniture
    {
        double CalcArea();
       
    }

    class Item
    {

        public Item(double userID, double length, double width, double height, double karmaPrice, Conditions_options Condition, DateTime DateOfMaking)
        {
            this.userID = userID;
            this.length = length;
            this.width = width;
            this.height = height;
            this.karmaPrice = karmaPrice;
            condition = Condition;
            dateOfMaking = DateOfMaking;
        }


        private static DateTime dateOfMaking;
        //TODO: age things
        private int age_years = 0, age_months = 0;
        

        private Conditions_options condition { get; set; }
        private double userID;
        private double UserID
        {
            get
            {
                return userID;
            }
        }

        private double length;
        private double Length { 
            get
            {
                return length;
            } 
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("The length of the item cannot be negative or zero");
                else
                    length = value;
            } 
        }

        private double width;
        private double Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("The width of the item cannot be negative or zero");
                else
                    width = value;
            }
        }

        private double height;
        private double Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("The height of the item cannot be negative or zero");
                else
                    height = value;
            }
        }
        private double karmaPrice;        

        private double KarmaPrice
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

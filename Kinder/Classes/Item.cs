using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinder.Classes
{
    public struct Dimensions
    {
        public int length;
        public int height;
        public int width;

        public Dimensions(int length, int height, int width)
        {
            this.length = length;
            this.height = height;
            this.width = width;
        }

        public override string ToString()
        {
            return length.ToString() + ',' + height.ToString() + ',' + width.ToString();
        }
    }

    //.NET interface usage:
    public class Item : IComparable<Item>, IEquatable<Item>
    {
        FileManager file = new();

        public Item()
        {

        }

        public Item(int id)
        {
            this.ID = id;
        }

        public Item(int id, DateTime dateOfPurchase, ConditionEnum condition, CathegoryEnum cathegory, int userID, Dimensions size, int karmaPrice)
        {
            this.ID = id;
            this.dateOfPurchase = dateOfPurchase;
            this.Condition = condition;
            this.Cathegory = cathegory;
            this.UserID = userID;
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
            str += UserID.ToString() + ';';
            str += size.ToString() + ';';
            str += karmaPrice.ToString() + ';';
            str += Name.ToString() + ';';
            str += Description.ToString();

            return str;
        }

        public string ToString(string arg)
        {
            string str = "";

            str += ID.ToString() + ';';
            str += dateOfPurchase.ToString("yyyy-MM-dd") + ';';
            str += Condition.ToString() + ';';
            str += Cathegory.ToString() + ';';
            str += UserID.ToString() + ';';
            str += arg + ';';
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

        public int LikedBy { get; set; }

        private DateTime dateOfPurchase;
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

        //auto-implemented property:
        public ConditionEnum Condition { get; set; }
        public CathegoryEnum Cathegory { get; set; }

        public int UserID { get; set; }

        //standard property:
        private Dimensions size;
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

        //optional arguments:
        public string Name { get; set; }
        public void SetName(string nameArg = "Unnamed item") => Name = nameArg;

        public string Description { get; set; }
        public void SetDescription(string descArg = "Undescribed item") => Description = descArg;

        private int karmaPrice;
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

        private bool IsUseful(Dimensions dimensions1, Dimensions dimensions2)
        {
            int[] arr1 =
            {
                dimensions1.length, dimensions1.height, dimensions1.width
            };

            int[] arr2 =
            {
                dimensions2.length, dimensions2.height, dimensions2.width
            };

            arr1 = arr1.OrderBy(x => x).ToArray();
            arr2 = arr2.OrderBy(x => x).ToArray();

            if (arr1[0] == arr2[0] && arr1[1] == arr2[1] && arr1[2] == arr2[2])
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //custom event init 2
        public delegate void UselessChangeEventHandler(object sender, InvalidEventArgs<Item, Dimensions> e);
        public event UselessChangeEventHandler UselessChange;

        public Item ChangeItemDimentions(Dimensions dimensions, List<Item> items)
        {
            Item result = new();

            //iterating thru every item, to find the one being changed
            foreach (Item i in items)
            {
                if(i.ID == ID)
                {
                    //check if change is usefull
                    if(IsUseful(i.Size, dimensions))
                    {
                        i.size = dimensions;
                    }
                    else
                    {   
                        //event
                        UselessChange(this, new InvalidEventArgs<Item, Dimensions>(i, dimensions));
                    }

                    result = i;
                }
            }

            return result;
        }
    }
}

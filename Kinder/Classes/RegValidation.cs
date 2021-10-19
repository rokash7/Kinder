using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kinder.Classes
{
    class RegValidation
    {
        public static Boolean CheckTextBoxInput(string Text)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
        
        public static Boolean CheckIfEmailValid(string Email)           ///Email validation
        {
            if (Regex.IsMatch(Email, @"^[\w._%+-]+@[\w.-]+\.[a-zA-Z]{2,4}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public static Boolean CheckIfFieldIsValid(string Text)          ///Does not allows to write a space into a field
        {
            if (Regex.IsMatch(Text, @"\s"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Boolean CheckIfPhoneNumberValid(string Phonenumber) /// Phone Number validation.
        {
            if(Regex.IsMatch(Phonenumber, @"^(\+\d{11})$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

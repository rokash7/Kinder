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
            if (Regex.IsMatch(Email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                return false;
            }
            else
            {
                return true;
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

        public static Boolean CheckIfPhoneNumberValid(string Phonenumber) /// TO DO WITH ALL PHONE NUMBS. CURRENTLY ONLY LTU WORKS...
        {
            if(Regex.IsMatch(Phonenumber, @"^(\+370[0-9]{8})$"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

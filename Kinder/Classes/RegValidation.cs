using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Kinder.Classes
{
    class RegValidation
    {
        public static void CheckTextBoxInput(string Text)
        {
            if (string.IsNullOrEmpty(Text))
            {
                throw (new EmptyFieldException("Empty field!"));
            }
        }

        public static void CheckIfEmailValid(string Email)           ///Email validation
        {
            if (!Regex.IsMatch(Email, @"^[\w._%+-]+@[\w.-]+\.[a-zA-Z]{2,4}$"))
            {
                throw (new InvalidEmailException("Invalid email!"));
            }
        }

        public static void CheckIfFieldIsValid(string Text)          ///Does not allows to write a space into a field
        {
            if (Regex.IsMatch(Text, @"\s"))
            {
                throw (new WhiteSpaceDetectedException("Input is not valid"));
            }
        }

        public static void CheckIfPhoneNumberValid(string Phonenumber) /// Phone Number validation.
        {
            if (!Regex.IsMatch(Phonenumber, @"^(\+\d{11})$"))
            {
                throw (new InvalidPhonenumberException("Invalid phonenumber"));
            }
        }

        public static void CheckIfPasswordValid(string Password)
        {
            if (Password.Length < 8)
            {
                throw (new InvalidPasswordException("Password is too short"));
            }
            if (!Regex.IsMatch(Password, @"[0-9]+"))
            {
                throw (new InvalidPasswordException("Password should have at least one number"));
            }
            if (!Regex.IsMatch(Password, @"[A-Z]+"))
            {
                throw (new InvalidPasswordException("Password should have at least one uppercase letter"));
            }
            if (!Regex.IsMatch(Password, @"[a-z]+"))
            {
                throw (new InvalidPasswordException("Password should have at least one downcase letter"));
            }
            if (!Regex.IsMatch(Password, @"[^A-Za-z0-9]"))
            {
                throw (new InvalidPasswordException("Password should have at least one special character (@#$> and etc.)"));
            }
        }
    }
}

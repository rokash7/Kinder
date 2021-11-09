using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Kinder.Classes
{
    class RegValidation
    {
        public static void CheckTextBoxInput(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw (new EmptyFieldException("Empty field!"));
            }
        }

        public static void CheckIfEmailValid(string email)           ///Email validation
        {
            if (!Regex.IsMatch(email, @"^[\w._%+-]+@[\w.-]+\.[a-zA-Z]{2,4}$"))
            {
                throw (new InvalidEmailException("Invalid email!"));
            }
        }

        public static void CheckIfFieldIsValid(string text)          ///Does not allows to write a space into a field
        {
            if (Regex.IsMatch(text, @"\s"))
            {
                throw (new WhiteSpaceDetectedException("Input is not valid"));
            }
        }

        public static void CheckIfPhoneNumberValid(string phoneNumber) /// Phone Number validation.
        {
            if (!Regex.IsMatch(phoneNumber, @"^(\+\d{11})$"))
            {
                throw (new InvalidPhonenumberException("Invalid phonenumber"));
            }
        }

        public static void CheckIfPasswordValid(string password)
        {
            if (password.Length < 8)
            {
                throw (new InvalidPasswordException("Password is too short"));
            }
            if (!Regex.IsMatch(password, @"[0-9]+"))
            {
                throw (new InvalidPasswordException("Password should have at least one number"));
            }
            if (!Regex.IsMatch(password, @"[A-Z]+"))
            {
                throw (new InvalidPasswordException("Password should have at least one uppercase letter"));
            }
            if (!Regex.IsMatch(password, @"[a-z]+"))
            {
                throw (new InvalidPasswordException("Password should have at least one downcase letter"));
            }
            if (!Regex.IsMatch(password, @"[^A-Za-z0-9]"))
            {
                throw (new InvalidPasswordException("Password should have at least one special character (@#$> and etc.)"));
            }
        }
    }
}

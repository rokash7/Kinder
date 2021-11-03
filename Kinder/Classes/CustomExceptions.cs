using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinder.Classes
{
    public class WhiteSpaceDetectedException : Exception
    {
        public WhiteSpaceDetectedException(string message) : base(message) { }
    }
    public class UsernameIsTakenException : Exception
    {
        public UsernameIsTakenException(string message) : base(message) { }
    }
    public class EmptyFieldException : Exception
    {
        public EmptyFieldException(string message) : base(message) { }
    }
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message) { }
    }
    public class InvalidPhonenumberException : Exception
    {
        public InvalidPhonenumberException(string message) : base(message) { }
    }
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message) : base(message) { }
    }
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException(string message) : base(message) { }
    }
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException(string message) : base(message) { }
    }
}

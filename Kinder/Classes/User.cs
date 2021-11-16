using Kinder.Classes;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

public class User
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int ID { get; set; }
    public string Password { get; set; }
    public int KarmaPoints { get; set; }
    public int PlaceGive { get; set; }
    public int PlaceTake { get; set; }
    public string RegDate { get; set; }
    private static int _currentUserID;
    public static int CurrentUserID
    {
        get { return _currentUserID; }
        set { _currentUserID = value; }
    }
    public User() { }

    public static void CheckIfUserAlreadyExists(string text)
    {
        foreach (User user in FileManager.GetUsers())
        {
            if (text == user.Email || text == user.PhoneNumber)
            {
                throw (new UserAlreadyExistsException("User already exists! Try logging in!"));
            }
        }
    }

    public static void CheckIfUsernameIsTaken(string username)
    {
        foreach (User user in FileManager.GetUsers())
        {
            if (username == user.Username)
            {
                throw (new UsernameIsTakenException("Username is taken!"));
            }
        }
    }

    public static void CheckPassword(string password)
    {
        foreach (User user in FileManager.GetUsers())
        {
            if (user.ID == CurrentUserID)
            {
                if (!VerifyPassword(password, user.Password))
                {
                    throw (new IncorrectPasswordException("Incorrect password!"));
                }
            }
        }
    }

    public static Boolean CheckUsername(string username)
    {
        foreach (User user in FileManager.GetUsers())
        {
            if (username == user.Username)
            {
                CurrentUserID = user.ID;
                return true;
            }
        }
        throw (new UserDoesNotExistsException("User with this username does not exists!"));
    }

    public static int GetUserCount()
    {
        return FileManager.GetUsers().Count;
    }

    public static User GetCurrentUser()
    {
        User currentUser = new();
        foreach (User user in FileManager.GetUsers())
        {
            if (user.ID == CurrentUserID)
            {
                currentUser = user;
            }
        }
        return currentUser;
    }

    public static User GetUserByUsername(string username)
    {
        User currentUser = new();
        foreach (User user in FileManager.GetUsers())
        {
            if (user.Username == username)
            {
                currentUser = user;
            }
        }
        return currentUser;
    }

    public static User GetUserByID(int id)
    {
        User result = new();
        foreach (User user in FileManager.GetUsers())
        {
            if (user.ID == id)
            {
                result = user;
            }
        }
        return result;
    }

    //custom event init 1
    public delegate void SameEmailEventHandler(object sender, InvalidEventArgs<User, string> e);
    public event SameEmailEventHandler SameEmail;

    public void ChangeUserEmail(string text)
    {
        bool emailExists = false;
        User targetUser = new();

        //check if any user has same email, that we want to change
        foreach (User user in FileManager.GetUsers())
        {
            if (user.Email == text)
            {
                emailExists = true;
                targetUser = user;
            }
        }

        foreach (User user in FileManager.GetUsers())
        {
            if (user.ID == CurrentUserID)
            {
                if (emailExists)
                {
                    //custom event
                    SameEmail(this, new InvalidEventArgs<User, string>(targetUser, text));
                }
                else
                {
                    user.Email = text;
                    FileManager.ChangeUserField(user);
                    MessageBox.Show("Email changed to " + text + ".");
                }
            }
        }
    }

    public static void ChangeUserPhoneNumber(string text)
    {
        foreach (User user in FileManager.GetUsers())
        {
            if (user.ID == CurrentUserID)
            {
                user.PhoneNumber = text;
                FileManager.ChangeUserField(user);
            }
        }
    }

    public static void ChangeUserPassword(string text)
    {
        foreach (User user in FileManager.GetUsers())
        {
            if (user.ID == CurrentUserID)
            {
                user.Password = text;
                FileManager.ChangeUserField(user);
            }
        }
    }

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
  
    public override string ToString() => ID.ToString();

    public static Boolean CheckBoolIfUsernameIsTaken(string username)
    {
        foreach (User user in FileManager.GetUsers())
        {
            if (username == user.Username)
            {
                return true;
            }
        }
        return false;
    }
}

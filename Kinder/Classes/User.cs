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
    public static int CurrentUserID { get; set; }

    public User() { }
    public static Boolean CheckLogin(string Username, string Password)
    {
        foreach (User user in FileManager.getUsers())
        {
            if (Username == user.Username && Password == user.Password)
            {
                CurrentUserID = user.ID;
                return true;
            }
        }
        return false;
    }

    public static Boolean CheckIfUserAlreadyExists(string Text)
    {
        foreach (User user in FileManager.getUsers())
        {
            if (Text == user.Email || Text == user.PhoneNumber)
            {
                return true;
            }
        }
        return false;
    }

    public static Boolean CheckIfUsernameIsTaken(string Username)
    {
        foreach (User user in FileManager.getUsers())
        {
            if (Username == user.Username)
            {
                return true;
            }
        }
        return false;
    }

    public static Boolean CheckPassword(string Password)
    {
        foreach (User user in FileManager.getUsers())
        {
            if (user.ID == CurrentUserID)
            {
                if (user.Password == Password)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static int getUserCount()
    {
        return FileManager.getUsers().Count;
    }

    public static User getCurrentUser()
    {
        User CurrentUser = new User(); 
        foreach(User user in FileManager.getUsers())
        {
            if(user.ID == CurrentUserID)
            {
                CurrentUser = user;
            }
        }
        return CurrentUser;
    }

    public static void ChangeUserEmail(string Text)
    {
        foreach (User user in FileManager.getUsers())
        {
            if (user.ID == CurrentUserID)
            {
                user.Email = Text;
                FileManager.ChangeUserField(user);
            }
        }
    }

    public static void ChangeUserPhoneNumber(string Text)
    {
        foreach (User user in FileManager.getUsers())
        {
            if (user.ID == CurrentUserID)
            {
                user.PhoneNumber = Text;
                FileManager.ChangeUserField(user);
            }
        }
    }

    public static void ChangeUserPassword(string Text)
    {
        foreach (User user in FileManager.getUsers())
        {
            if (user.ID == CurrentUserID)
            {
                user.Password = Text;
                FileManager.ChangeUserField(user);
            }
        }
    }
}

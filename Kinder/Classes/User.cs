using Kinder.Classes;
using System;
using System.Collections.Generic;
using System.Reflection;

public class User
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int Id { get; set; }
    public string Password { get; set; }
    public int KarmaPoints { get; set; }
    public int PlaceGive { get; set; }
    public int PlaceTake { get; set; }
    public static int CurrentUserID { get; set; }

    public User() { }

    static List<User> users = FileManager.getUsers();
    public static Boolean CheckLogin(string Username, string Password)
    {
        users = FileManager.getUsers();
        foreach (User user in users)
        {
            if (Username == user.Username && Password == user.Password)
            {
                CurrentUserID = user.Id;
                return true;
            }
        }
        return false;
    }

    public static Boolean CheckIfUserAlreadyExists(string Email, string PhoneNumber)
    {
        users = FileManager.getUsers();
        foreach (User user in users)
        {
            if (Email == user.Email || PhoneNumber == user.PhoneNumber)
            {
                return false;
            }
        }
        return true;

    }

    public static Boolean CheckIfUsernameIsTaken(string Username)
    {
        users = FileManager.getUsers();
        foreach (User user in users)
        {
            if (Username == user.Username)
            {
                return false;
            }
        }

        return true;
    }

    public static int getUserNumber()
    {
        users = FileManager.getUsers();
        return users.Count;
    }

    public static void ChangeUserEmail(string Text)
    {
        foreach (User user in users)
        {
            if (user.Id == CurrentUserID)
            {
                user.Email = Text;
                FileManager.ChangeUserField(user.Username, user.Password, user.Email, user.PhoneNumber, user.Name, user.Surname, CurrentUserID);
            }
        }
    }




}

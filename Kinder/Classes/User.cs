using System;
public class User
{
    public string Name { get; set;}
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int Id { get; set; }
    public string Password { get; set; }
    public int KarmaPoints { get; set; }
    public int PlaceGive { get; set; }
    public int PlaceTake { get; set; }

    public User() {}
    
    public static Boolean checkLogin(string Username, string Password) ///TO DO WITH FILES
    {
        
        if (Username == "admin" && Password == "admin") 
        {
            return true;
        } else
        {
            return false;
        }
    }

    public static Boolean checkEmailAndPhoneNumber(string Email, string PhoneNumber) ///TO DO WITH FILES
    {
        if (Email == "dude@gmail.com" && PhoneNumber == "+12345")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static Boolean checkUsername(string Username) ///TO DO WITH FILES
    {
        if (Username == "admin")
        {
            return true;
        } else
        {
            return false;
        } 
    }
}

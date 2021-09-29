using System;
public class User
{
    public string Name { get; set;}
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int Id { get; }
    public int Password { get; set; }
    public int KarmaPoints { get; set; }
    
    public int PlaceGive { get; set; }
    public int PlaceTake { get; set; }


    public User() {}

}

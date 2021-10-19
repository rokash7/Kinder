using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kinder.Classes
{
    class FileManager
    {
        static string Path = System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString();
        static string FileLocation = System.IO.Path.Combine(Path, "Data_files\\Users.txt");
        static string FileLocation_liked = System.IO.Path.Combine(Path, "Data_files\\Items_liked.txt");

        public static List<User> getUsers()
        {
            List<User> users = new List<User>();
            users.Clear();
            using (StreamReader reader = new StreamReader(FileLocation))
            {
                string[] userData;
                while (!reader.EndOfStream)
                {
                    userData = reader.ReadLine().Split(' ');
                    User user = new User();
                    user.ID = Int32.Parse(userData[0]);
                    user.Username = userData[1];
                    user.Password = userData[2];
                    user.Email = userData[3];
                    user.PhoneNumber = userData[4];
                    user.Name = userData[5];
                    user.Surname = userData[6];
                    user.KarmaPoints = Int32.Parse(userData[7]);
                    user.RegDate = userData[8];
                    users.Add(user);
                }
            }
            return users;
        }

        //indexed property:
        private User[] Users = getUsers().ToArray();

        public User this[int index] 
        {
            get
            {
                return Users[index];
            }
            set
            {
                Users[index] = value;
            }
        }

        public static void AddNewUser(string Username, string Password, string Email, string PhoneNumber, string Name, string Surname, int ID, string RegDate)
        {
            using (StreamWriter sw = new StreamWriter(FileLocation, true))
            {
                sw.WriteLine(ID.ToString() + ' ' + Username + ' ' + Password + ' ' + Email + ' ' + PhoneNumber + ' ' + Name + ' ' + Surname + ' ' + 0 + ' ' + RegDate);
            }
        }
        
        public static void ChangeUserField(string Username, string Password, string Email, String PhoneNumber, string Name, string Surname, int ID, int KarmaPoints, string RegDate)
        {
            string[] UsersText = File.ReadAllLines(FileLocation);
            UsersText[ID] = ID.ToString() + ' ' + Username + ' ' + Password + ' ' + Email + ' ' + PhoneNumber + ' ' + Name + ' ' + Surname + ' ' + KarmaPoints.ToString() + ' ' + RegDate;
            File.WriteAllLines(FileLocation, UsersText);
        }
    
        public static void AddUserIDToLiked(int ID)
        {
            using (StreamWriter sw = new StreamWriter(FileLocation_liked, true))
            {
                sw.WriteLine(ID.ToString());
            }
        }
    }
}
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
        static string path = System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString()).ToString();
        static string fileLocation = System.IO.Path.Combine(path, "Data_files\\Users.txt");
        static string fileLocation_liked = System.IO.Path.Combine(path, "Data_files\\Items_liked.txt");
        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            users.Clear();
            using (StreamReader reader = new StreamReader(fileLocation))
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
        private User[] users = GetUsers().ToArray();

        public User this[int index] 
        {
            get
            {
                return users[index];
            }
            set
            {
                users[index] = value;
            }
        }

        public static void AddNewUser(string username, string password, string email, string phoneNumber, string name, string surname, int id, string regDate)
        {
            using (StreamWriter sw = new StreamWriter(fileLocation, true))
            {
                sw.WriteLine(id.ToString() + ' ' + username + ' ' + password + ' ' + email + ' ' + phoneNumber + ' ' + name + ' ' + surname + ' ' + 0 + ' ' + regDate);
            }
        }
        
        public static void ChangeUserField(User user)
        {
            string[] usersText = File.ReadAllLines(fileLocation);
            usersText[user.ID] = user.ID.ToString() + ' ' + user.Username + ' ' + user.Password + ' ' + user.Email + ' ' + user.PhoneNumber + ' ' + user.Name + ' ' + user.Surname + ' ' + user.KarmaPoints.ToString() + ' ' + user.RegDate;
            File.WriteAllLines(fileLocation, usersText);
        }
    
        public static void AddUserIDToLiked(int id)
        {
            using (StreamWriter sw = new StreamWriter(fileLocation_liked, true))
            {
                sw.WriteLine(id.ToString());
            }
        }
    }
}
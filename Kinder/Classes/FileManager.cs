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
        static string FileLocation = System.IO.Path.Combine(path, "Data_files\\Users.txt");

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
                    ///MessageBox.Show(userData[0]);
                    User user = new User();
                    user.Id = Int32.Parse(userData[0]);
                    user.Username = userData[1];
                    user.Password = userData[2];
                    user.Email = userData[3];
                    user.PhoneNumber = userData[4];
                    user.Name = userData[5];
                    user.Surname = userData[6];
                    user.KarmaPoints = Int32.Parse(userData[7]);
                    users.Add(user);
                }
            }

            return users;
        }
        public static void addNewUser(string Username, string Password, string Email, string PhoneNumber, string Name, string Surname, int id)
        {
            using (StreamWriter sw = new StreamWriter(FileLocation, true))
            {
                sw.Write(id);
                sw.Write(' ');
                sw.WriteLine(Username + ' ' + Password + ' ' + Email + ' ' + PhoneNumber + ' ' + Name + ' ' + Surname + ' ' + 0);


            }
        }
        public static void ChangeUserField(string Username, string Password, string Email, String PhoneNumber, string Name, string Surname, int id)
        {
            string[] UsersText = File.ReadAllLines(FileLocation);
            UsersText[id] = id.ToString() + ' ' + Username + ' ' + Password + ' ' + Email + ' ' + PhoneNumber + ' ' + Name + ' ' + Surname + ' ' + 0;
            File.WriteAllLines(FileLocation, UsersText);
        }
    }
}
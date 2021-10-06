using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinder.Classes
{
    class FileManager
    {
        static string UsersFile = "Users.txt";

        public static List<User> getUsers()                 ///NOT TESTED/IMPLEMENTED ANYWHERE YET !!!!!
        {
            List<User> users = new List<User>();
            using (StreamReader reader = new StreamReader(UsersFile))
            {
                string line;
                string[] userData;
                while ((line = reader.ReadLine()) != null)
                {
                    userData = reader.ReadLine().Split(' ');
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

        public void saveUser()
        {
            //TODO
        }
    }
}

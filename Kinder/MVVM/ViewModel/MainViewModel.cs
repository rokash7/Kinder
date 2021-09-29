using Kinder.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinder.MVVM.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }
        public MainViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            //adding test messages
            Messages.Add(new MessageModel
            {
                Username = "UsernameTest",
                UsernameColor = "#409aff",
                ImageSource = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png",
                Message = "Test message1 \n Test message1",
                Time = DateTime.Now,
                IsNativeOrigin = false,
                FirstMessage = true

            });

            for (int i = 0; i < 3; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "UsernameTest",
                    UsernameColor = "#409aff",
                    ImageSource = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png",
                    Message = "Test message1 \n Test message1",
                    Time = DateTime.Now,
                    IsNativeOrigin = false,
                    FirstMessage = false

                });
            }

            for (int i = 0; i < 4; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "THE DUDE",
                    UsernameColor = "#409aff",
                    ImageSource = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png",
                    Message = "Test message1 \n Test message1",
                    Time = DateTime.Now,
                    IsNativeOrigin = true
                });
            }

            Messages.Add(new MessageModel
            {
                Username = "THE DUDE",
                UsernameColor = "#409aff",
                ImageSource = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png",
                Message = "Last",
                Time = DateTime.Now,
                IsNativeOrigin = true
            });

            //adding test contacts
            for (int i = 0; i < 5; i++)
            {
                Contacts.Add(new ContactModel
                {
                    Username = $"Dude{i}",
                    ImageSource = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png",
                    Messages = Messages
                });
            }
        }
    }
}

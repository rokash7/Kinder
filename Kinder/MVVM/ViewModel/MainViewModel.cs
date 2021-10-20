﻿using Kinder.MessageCore;
using Kinder.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinder.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }

        /* Commands */
        public RelayCommand SendCommand { get; set; }

        private ContactModel _selectedContact;

        public ContactModel SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
            }
        }

        private String _message;

        public String Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            SendCommand = new RelayCommand(o =>
            {
                Messages.Add(new MessageModel
                {
                    Message = Message,
                });

                Message = "";

            });

            //adding test messages
            Messages.Add(new MessageModel
            {
                Username = "Dude1",
                UsernameColor = "#409aff",
                ImageSource = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png",
                Message = "Test message1",
                Time = DateTime.Now,
                IsNativeOrigin = false,

            });

            Messages.Add(new MessageModel
            {
                Username = "me :)",
                UsernameColor = "#409aff",
                ImageSource = "https://miro.medium.com/max/512/1*nZ9VwHTLxAfNCuCjYAkajg.png",
                Message = "mano testine zinute",
                Time = DateTime.Now,
                IsNativeOrigin =true,

            });

            Messages.Add(new MessageModel
            {
                Username = "Dude1",
                UsernameColor = "#409aff",
                ImageSource = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png",
                Message = "Test message2",
                Time = DateTime.Now,
                IsNativeOrigin = false,

            });

            //for (int i = 0; i < 3; i++)
            //{
            //    Messages.Add(new MessageModel
            //    {
            //        Username = "UsernameTest",
            //        UsernameColor = "#409aff",
            //        ImageSource = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png",
            //        Message = "Test message1 \n Test message1",
            //        Time = DateTime.Now,
            //        IsNativeOrigin = false,
            //        FirstMessage = false
            //    });
            //}


            //Messages.Add(new MessageModel
            //{
            //    Username = "THE DUDE",
            //    UsernameColor = "#409aff",
            //    ImageSource = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png",
            //    Message = "Last",
            //    Time = DateTime.Now,
            //    IsNativeOrigin = true
            //});

            //adding test contacts
            for (int i = 0; i < 3; i++)
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

using Kinder.MessageCore;
using Kinder.MessageCore.Services;
using Kinder.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Kinder.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private IChatService chatService;
        private IDialogService dialogService;

        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }

        /* Commands */
        public RelayCommand SendCommand { get; set; }

        private ContactModel _selectedContact;

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set 
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        private string _profilePic;
        public string ProfilePic
        {
            get { return _profilePic; }
            set
            {
                _profilePic = value;
                OnPropertyChanged();
            }
        }

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
        //public MainViewModel()
        //{
        //    Messages = new ObservableCollection<MessageModel>();
        //    Contacts = new ObservableCollection<ContactModel>();

        //    chatService = new ChatService();
        //    dialogService = new DialogService();

        //    SendCommand = new RelayCommand(o =>
        //    {
        //        Messages.Add(new MessageModel
        //        {
        //            Message = Message,
        //        });

        //        Message = "";

        //    });



        //    //adding test messages
        //    Messages.Add(new MessageModel
        //    {
        //        Username = "Dude1",
        //        UsernameColor = "#409aff",
        //        ImageSource = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png",
        //        Message = "Test message1",
        //        Time = DateTime.Now,
        //        IsNativeOrigin = false,

        //    });

        //    Messages.Add(new MessageModel
        //    {
        //        Username = "me :)",
        //        UsernameColor = "#409aff",
        //        ImageSource = "https://miro.medium.com/max/512/1*nZ9VwHTLxAfNCuCjYAkajg.png",
        //        Message = "mano testine zinute",
        //        Time = DateTime.Now,
        //        IsNativeOrigin = true,

        //    });

        //    Messages.Add(new MessageModel
        //    {
        //        Username = "Dude1",
        //        UsernameColor = "#409aff",
        //        ImageSource = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png",
        //        Message = "Test message2",
        //        Time = DateTime.Now,
        //        IsNativeOrigin = false,

        //    });

        //    //adding test contacts
        //    for (int i = 0; i < 3; i++)
        //    {
        //        Contacts.Add(new ContactModel
        //        {
        //            Username = $"Dude{i}",
        //            ImageSource = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png",
        //            Messages = Messages
        //        });
        //    }
        //}

        public MainViewModel(IChatService chatSvc, IDialogService diagSvc)
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            chatService = chatSvc;
            dialogService = diagSvc;

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
                IsNativeOrigin = true,

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

        private byte[] Avatar()
        {
            byte[] pic = null;
            if (!string.IsNullOrEmpty(_profilePic)) pic = File.ReadAllBytes(_profilePic);
            return pic;
        }

        //#region Login Command
        //private ICommand _loginCommand;
        //public ICommand LoginCommand
        //{
        //    get
        //    {
        //        return _loginCommand ?? (_loginCommand =
        //            new RelayCommandAsync(() => Login(), (o) => CanLogin()));
        //    }
        //}

        //private async Task<bool> Login()
        //{
        //    try
        //    {
        //        List<User> users = new List<User>();
        //        users = await chatService.LoginAsync(_userName, Avatar());
        //        if (users != null)
        //        {
        //            users.ForEach(u => Participants.Add(new Participant { Name = u.Name, Photo = u.Photo }));
        //            UserMode = UserModes.Chat;
        //            IsLoggedIn = true;
        //            return true;
        //        }
        //        else
        //        {
        //            dialogService.ShowNotification("Username is already in use");
        //            return false;
        //        }

        //    }
        //    catch (Exception) { return false; }
        //}

        //private bool CanLogin()
        //{
        //    return !string.IsNullOrEmpty(UserName) && UserName.Length >= 2 && IsConnected;
        //}
        //#endregion
    }
}

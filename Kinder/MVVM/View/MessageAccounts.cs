using Kinder.MessageCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kinder.MVVM.View
{
    class MessageAccounts
    {
        #region Login Command
        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand =
                    new RelayCommandAsync(() => Login(), (o) => CanLogin()));
            }
        }

        private async Task<bool> Login()
        {
            try
            {
                List<User> users = new List<User>();
                users = await chatService.LoginAsync(_userName, Avatar());
                if (users != null)
                {
                    users.ForEach(u => Participants.Add(new Participant { Name = u.Name, Photo = u.Photo }));
                    UserMode = UserModes.Chat;
                    IsLoggedIn = true;
                    return true;
                }
                else
                {
                    dialogService.ShowNotification("Username is already in use");
                    return false;
                }

            }
            catch (Exception) { return false; }
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(UserName) && UserName.Length >= 2 && IsConnected;
        }
        #endregion
    }
}

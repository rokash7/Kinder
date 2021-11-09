//using ChatServerCS;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Hubs
{
    public class ChatHub : Hub<IClient>
    {
        private static ConcurrentDictionary<string, User> ChatClients =
                                           new ConcurrentDictionary<string, User>();

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userName = ChatClients.SingleOrDefault((c) => c.Value.ID == Context.ConnectionId).Key;
            if (userName != null)
            {
                Clients.Others.ParticipantDisconnection(userName);
                Console.WriteLine($"<> {userName} disconnected");
            }
            return base.OnDisconnectedAsync(exception);
        }

        public override Task OnConnectedAsync()
        {
            var userName = ChatClients.SingleOrDefault((c) => c.Value.ID == Context.ConnectionId).Key;
            if (userName != null)
            {
                Clients.Others.ParticipantReconnection(userName);
                Console.WriteLine($"== {userName} reconnected");
            }
            return base.OnConnectedAsync();
        }

        public List<User> Login(string name, byte[] photo)
        {
            if (!ChatClients.ContainsKey(Context.ConnectionId))
            {
                Console.WriteLine($"++ {name} logged in");
                List<User> users = new List<User>(ChatClients.Values);
                User newUser = new User { Name = name, ID = Context.ConnectionId, Photo = photo };
                var added = ChatClients.TryAdd(Context.ConnectionId, newUser);
                if (!added) return null;
                //Clients.CallerState.UserName = name;                
                Clients.Others.ParticipantLogin(newUser);
                return users;
            }
            return null;
        }

        public void Logout()
        {
            //var name = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(Context.ConnectionId))
            {
                User client = new User();
                ChatClients.TryRemove(Context.ConnectionId, out client);
                Clients.Others.ParticipantLogout(Context.ConnectionId);
                Console.WriteLine($"-- {Context.ConnectionId} logged out");
            }
        }

        public void BroadcastTextMessage(string message)
        {
            //var name = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(Context.ConnectionId) && !string.IsNullOrEmpty(message))
            {
                Clients.Others.BroadcastTextMessage(Context.ConnectionId, message);
            }
        }

        public void BroadcastImageMessage(byte[] img)
        {
            //var name = Clients.CallerState.UserName;
            if (img != null)
            {
                Clients.Others.BroadcastPictureMessage(Context.ConnectionId, img);
            }
        }

        public void UnicastTextMessage(string recepient, string message)
        {
            //var sender = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(Context.ConnectionId) && recepient != Context.ConnectionId &&
                !string.IsNullOrEmpty(message) && ChatClients.ContainsKey(recepient))
            {
                User client = new User();
                ChatClients.TryGetValue(recepient, out client);
                Clients.Client(client.ID).UnicastTextMessage(Context.ConnectionId, message);
            }
        }

        public void UnicastImageMessage(string recepient, byte[] img)
        {
            //var sender = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(Context.ConnectionId) && recepient != Context.ConnectionId &&
                img != null && ChatClients.ContainsKey(recepient))
            {
                User client = new User();
                ChatClients.TryGetValue(recepient, out client);
                Clients.Client(client.ID).UnicastPictureMessage(Context.ConnectionId, img);
            }
        }
        //public void Typing(string recepient)
        //{
        //    if (string.IsNullOrEmpty(recepient)) return;
        //    //var sender = Clients.CallerState.UserName;
        //    User client = new User();
        //    ChatClients.TryGetValue(recepient, out client);
        //    Clients.Client(client.ID).ParticipantTyping(Context.ConnectionId);
        //}
    }
}

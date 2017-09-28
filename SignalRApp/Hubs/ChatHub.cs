using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRApp.Models;

namespace SignalRApp.Hubs
{
    public class ChatHub : Hub
    {
        private static List<User> users = new List<User>();
       
        public void Send (string name, string msg)
        {
            Clients.All.addMessage(name, msg);
        }

        public void Connect (string userName)
        {
            var id = Context.ConnectionId;

            if (!users.Any(u => u.ConnectionId == id))
            {
                users.Add(new User { ConnectionId = id, UserName = userName });

                Clients.Caller.onConnected(id, userName, users);

                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var connectionId = Context.ConnectionId;
            var item = users.FirstOrDefault(u => u.ConnectionId == connectionId);

            if (item != null)
            {
                users.Remove(item);
                Clients.All.onUserDisconnected(connectionId, item.UserName);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}
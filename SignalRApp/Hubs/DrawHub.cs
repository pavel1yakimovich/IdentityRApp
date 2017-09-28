using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRApp.Models;

namespace SignalRApp.Hubs
{
    public class DrawHub : Hub
    {
        public void Send(LineData data)
        {
            Clients.AllExcept(Context.ConnectionId).addLine(data);
        }
    }
}
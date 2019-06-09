using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using RentApp.Persistance;
using System.Data.Entity;
using Unity.Attributes;

namespace RentApp.Hubs
{
    [HubName("notifications")]
    public class NotificationHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        private static Timer t = new Timer();

        DbContext db { get; set; }

        public NotificationHub(DbContext db)
        {
            this.db = db;
        }

        public void Hello()
        {
            Clients.All.hello("Hello from server");
        }

        public static void Notify(string message)
        {
            hubContext.Clients.All.notification(message);


        } 

        public void GetTime()
        {
            Clients.All.setRealTime(DateTime.Now.ToString("h:mm:ss tt"));
        }

        public void TimeServerUpdates()
        {
            t.Interval = 1000;
            t.Start();
            t.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            GetTime();
        }

        public void StopTimeServerUpdates()
        {
            t.Stop();
        }

        public override Task OnConnected()
        {
            //Ako vam treba pojedinacni User
       //     var identityName = Context.User.Identity.Name;

          //  Groups.Add(Context.ConnectionId, $"PersonalGroup{Context.User.Identity.Name}");
            Groups.Add(Context.ConnectionId, "Admins");

            //if (Context.User.IsInRole("Admin"))
            //{
            //    Groups.Add(Context.ConnectionId, "Admins");
            //}

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Groups.Remove(Context.ConnectionId, "Admins");

            //if (Context.User.IsInRole("Admin"))
            //{
            //    Groups.Remove(Context.ConnectionId, "Admins");
            //}

            return base.OnDisconnected(stopCalled);
        }
    }
}
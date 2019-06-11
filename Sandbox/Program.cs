using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public class Program
    {
        static void Main(string[] args)
        {
            DateTime startup = DateTime.Now;
            TrackIt_2019.API.BaseUri = "";
            TrackIt_2019.API.Authentication.Username = "TrackIt-Control";
            TrackIt_2019.API.Authentication.Password = "";
            TrackIt_2019.API.Authentication.Group = "System Administration";

            var ticketProperties = new ExpandoObject() as IDictionary<string, Object>;
            ticketProperties.Add("Ticket Summary", "This is a test ticket");
            ticketProperties.Add("Assigned To Group", "HELP DESK");
            ticketProperties.Add("Requestor ID", "TD");
            dynamic newTicket = TrackIt_2019.API.Ticket.Create(ticketProperties);
            //dynamic newTicket = new ExpandoObject();
            //newTicket.TicketID = 78536;

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(newTicket));
            Console.WriteLine();

            TrackIt_2019.API.Ticket.AddNote(newTicket.TicketID, "test note");
            Console.WriteLine();

            TrackIt_2019.API.Ticket.Assign(newTicket.TicketID, "RR");
            Console.WriteLine();

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(TrackIt_2019.API.Ticket.Get(newTicket.TicketID)));
            Console.WriteLine();

            TrackIt_2019.API.Ticket.SetCategory(newTicket.TicketID, "Information Requests");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(TrackIt_2019.API.Ticket.Get(newTicket.TicketID)));
            Console.WriteLine();

            TrackIt_2019.API.Ticket.SetStatus(newTicket.TicketID, "Dismissed - No notes");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(TrackIt_2019.API.Ticket.Get(newTicket.TicketID)));
            Console.WriteLine();

            TrackIt_2019.API.Ticket.Delete(newTicket.TicketID);
            Console.WriteLine("and that should be a full life cycle");
            Console.WriteLine("press enter to finish");
            Console.ReadLine();
        }
    }
}

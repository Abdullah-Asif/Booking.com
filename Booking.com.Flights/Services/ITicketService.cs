using Booking.com.Flights.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.com.Flights.Services
{
    public interface ITicketService
    {
        void CreateTicket(Ticket ticket);
        IList<Ticket> GetAllTickets();
        (IList<Ticket> records, int total, int totalDisplay) GetTickets
            (int pageIndex, int pageSize, string searchText, string sortText);
        Ticket GetTicket(int id);
        void UpdateTicket(Ticket ticket);
        void Delete(int id);
    }
}

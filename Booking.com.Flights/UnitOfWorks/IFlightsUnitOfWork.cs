using Booking.com.Data;
using Booking.com.Flights.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.com.Flights.UnitOfWorks
{
    public interface IFlightsUnitOfWork : IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; set; }
        ITicketRepository TicketRepository { get; set; }
    }
}

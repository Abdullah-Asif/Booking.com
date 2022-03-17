using Booking.com.Data;
using Booking.com.Flights.Contexts;
using Booking.com.Flights.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.com.Flights.UnitOfWorks
{
    public class FlightsUnitOfWork : UnitOfWork, IFlightsUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; set; }
        public ITicketRepository TicketRepository { get; set; }
        public FlightsUnitOfWork(ICustomerRepository customerRepositoy,
            ITicketRepository ticketRepository,  IFlightsContext flightsContext)
            : base((FlightsContext)(flightsContext))
        {
            CustomerRepository = customerRepositoy;
            TicketRepository = ticketRepository;
        }
        
    }
}

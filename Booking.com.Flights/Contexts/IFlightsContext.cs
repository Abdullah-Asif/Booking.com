using Booking.com.Flights.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.com.Flights.Contexts
{
    public interface IFlightsContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Ticket> Tickets { get; set; }
    }
}

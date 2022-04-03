using Booking.com.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.com.Flights.Entities
{
    public class Ticket : IEntity<int>
    {
        public int Id { get; set; }
        public string Destination { get; set; }
        public int Fee { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

    }
}

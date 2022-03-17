﻿using Booking.com.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.com.Flights.Entities
{
    public class Customer : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public IList<Ticket> Tickets { get; set; }
    }

}

using Booking.com.Data;
using Booking.com.Flights.Contexts;
using Booking.com.Flights.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.com.Flights.Repositories
{
    public class CustomerRepository : Repository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(IFlightsContext flightsContext) :base((FlightsContext)(flightsContext))
        {
        }
    }
}
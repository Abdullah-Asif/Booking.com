using Booking.com.Data;
using Booking.com.Flights.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.com.Flights.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
    }
}

using Booking.com.Flights.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.com.Flights.Services
{
    public interface ICustomerService
    {
        void CreateCustomer(Customer customer);
        IList<Customer> GetAllCustomers();
        (IList<Customer> records, int total, int totalDisplay) GetCustomers
            (int pageIndex, int pageSize, string searchText, string sortText);
        Customer GetCustomer(int id);
        void UpdateCustomer(Customer customer);
        void Delete(int id);
    }
}

using Booking.com.Flights.BusinessObjects;
using Booking.com.Flights.Services;
using System;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking.com.Web.Models;

namespace Booking.com.Web.Areas.Admin.Models
{
    public class CustomerListModel
    {
        private readonly ICustomerService _customerService;
        public IList<Customer> Customers { get; set; }

        public CustomerListModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
        }

        public void GetCustomerList()
        {
            Customers = _customerService.GetAllCustomers();
        }

        internal object GetCustomers(DataTablesAjaxRequestModel dataTablesModel)
        {
            var data = _customerService.GetCustomers(
               dataTablesModel.PageIndex,
               dataTablesModel.PageSize,
               dataTablesModel.SearchText,
               dataTablesModel.GetSortText(new string[] { "Id", "Name", "Age", "Adress" }));

            return new
            {
                recordTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Id.ToString(),
                            record.Name.ToString(),
                            record.Age.ToString(),
                            record.Address.ToString(),
                            record.Id.ToString()
                        }
                 ).ToArray()
            };
        }
    }
}

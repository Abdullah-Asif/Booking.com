using Autofac;
using AutoMapper;
using Booking.com.Flights.BusinessObjects;
using Booking.com.Flights.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.com.Web.Areas.Admin.Models
{
    public class EditCustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public EditCustomerModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }

        public EditCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void LoadCustomerData(int id)
        {
            var customer = _customerService.GetCustomer(id);
            _mapper.Map(customer, this);
        }

        public void Update()
        {
            var customer = _mapper.Map<Customer>(this);
            _customerService.UpdateCustomer(customer);
        }
    }
}

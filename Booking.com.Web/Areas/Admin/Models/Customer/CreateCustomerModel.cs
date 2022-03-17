using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Booking.com.Flights.Services;
using Booking.com.Flights.BusinessObjects;

namespace Booking.com.Web.Areas.Admin.Models
{
    public class CreateCustomerModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CreateCustomerModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }

        public CreateCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void CreateCustomer()
        {
            var customer = _mapper.Map<Customer>(this);
            _customerService.CreateCustomer(customer);
        }

        public void Delete(int id)
        {
            _customerService.Delete(id);
        }
    }
}

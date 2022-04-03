using AutoMapper;
using Booking.com.Flights.BusinessObjects;
using Booking.com.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.com.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CreateCustomerModel, Customer>().ReverseMap();
            CreateMap<EditCustomerModel, Customer>().ReverseMap();
            CreateMap<CreateTicketModel, Ticket>().ReverseMap();
            CreateMap<EditTicketModel, Ticket>().ReverseMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO = Booking.com.Flights.Entities;
using BO = Booking.com.Flights.BusinessObjects;
using AutoMapper;

namespace Booking.com.Flights.Profiles
{
    public class FlightsProfile: Profile
    {
        public FlightsProfile()
        {
            CreateMap<EO.Customer, BO.Customer>().ReverseMap();
            CreateMap<EO.Ticket, BO.Ticket>().ReverseMap();
        }

    }
}

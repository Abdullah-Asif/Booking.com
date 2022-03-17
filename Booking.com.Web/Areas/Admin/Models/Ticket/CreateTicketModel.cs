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
    public class CreateTicketModel
    {
        public string Destination { get; set; }
        public int Fee { get; set; }

        public int CustomerId { get; set; }
        
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;

        public CreateTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }

        public CreateTicketModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public void CreateTicket()
        {
            var ticket = _mapper.Map<Ticket>(this);
            _ticketService.CreateTicket(ticket);
        }

        public void Delete(int id)
        {
            _ticketService.Delete(id);
        }
    }
}

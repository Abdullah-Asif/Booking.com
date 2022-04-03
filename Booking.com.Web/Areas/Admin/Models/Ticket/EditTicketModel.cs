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
    public class EditTicketModel
    {
        public int Id { get; set; }
        public string Destination { get; set; }
        public int Fee { get; set; }

        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;

        public EditTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }

        public EditTicketModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public void LoadTicketData(int id)
        {
            var ticket = _ticketService.GetTicket(id);
            _mapper.Map(ticket, this);
        }

        public void Update()
        {
            var ticket = _mapper.Map<Ticket>(this);
            _ticketService.UpdateTicket(ticket);
        }
    }
}

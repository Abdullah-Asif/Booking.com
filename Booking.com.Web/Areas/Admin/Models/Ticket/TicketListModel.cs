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
    public class TicketListModel
    {
        private readonly ITicketService _ticketService;
        public IList<Ticket> Tickets { get; set; }

        public TicketListModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
        }

        public void GetTicketList()
        {
            Tickets = _ticketService.GetAllTickets();
        }

        internal object GetTickets(DataTablesAjaxRequestModel dataTablesModel)
        {
            var data = _ticketService.GetTickets(
               dataTablesModel.PageIndex,
               dataTablesModel.PageSize,
               dataTablesModel.SearchText,
               dataTablesModel.GetSortText(new string[] { "Destination", "Fee", "CustomerId"}));

            return new
            {
                recordTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Destination.ToString(),
                            record.Fee.ToString(),
                            record.CustomerId.ToString(),
                            record.Id.ToString()
                        }
                 ).ToArray()
            };
        }
    }
}

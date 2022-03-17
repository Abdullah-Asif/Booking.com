using AutoMapper;
using Booking.com.Flights.BusinessObjects;
using Booking.com.Flights.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.com.Flights.Services
{
    public class TicketService : ITicketService
    {
        private readonly IFlightsUnitOfWork _flightsUnitOfWork;
        private readonly IMapper _mapper;
        public TicketService(IFlightsUnitOfWork flightsUnitOfWork, IMapper mapper)
        {
            _flightsUnitOfWork = flightsUnitOfWork;
            _mapper = mapper;

        }
        public void CreateTicket(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new InvalidOperationException("Ticket name can not be null");
            }
            if (isSameDestinationAlreadyExist(ticket.Destination))
            {
                throw new InvalidOperationException("Same Ticket from same address is already exist");
            }

            _flightsUnitOfWork.TicketRepository
                .Add(_mapper.Map<Entities.Ticket>(ticket));

            _flightsUnitOfWork.Save();
        }
        public Ticket GetTicket(int id)
        {
            var ticket = _flightsUnitOfWork.TicketRepository.GetById(id);
            if (ticket == null) return null;
            return _mapper.Map<Ticket>(ticket);
        }

        public IList<Ticket> GetAllTickets()
        {
            var ticketEntities = _flightsUnitOfWork.TicketRepository.GetAll();
            var tickets = new List<Ticket>();

            foreach (var entity in ticketEntities)
            {
                var ticket = _mapper.Map<Ticket>(entity);
                tickets.Add(ticket);
            }
            return tickets;
        }

        public (IList<Ticket> records, int total, int totalDisplay) GetTickets(
            int pageIndex, int pageSize, string searchText, string sortText)
        {
            var ticketsData = _flightsUnitOfWork.TicketRepository.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Destination.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from ticket in ticketsData.data
                               select _mapper.Map<Ticket>(ticket)).ToList();

            return (resultData, ticketsData.total, ticketsData.totalDisplay);
        }

        public void UpdateTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new InvalidOperationException("Ticket is missing");

            //if (isSameNameAndAddressAlreadyExist(Ticket.Name, Ticket.Address))
            //    throw new InvalidOperationException("Same Ticket from same address is already exist");
                
            var ticketEntity = _flightsUnitOfWork.TicketRepository.GetById(ticket.Id);

            if (ticketEntity != null)
            {
                _mapper.Map(ticket, ticketEntity);
                _flightsUnitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException("Couldn't this Ticket");
            }
        }

        private bool isSameDestinationAlreadyExist(string destination) =>
            _flightsUnitOfWork.TicketRepository.GetCount(x => x.Destination == destination) > 0;

        public void Delete(int id)
        {
            _flightsUnitOfWork.TicketRepository.Remove(id);
            _flightsUnitOfWork.Save();
        }
    }
}

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
    public class CustomerService : ICustomerService
    {
        private readonly IFlightsUnitOfWork _flightsUnitOfWork;
        private readonly IMapper _mapper;
        public CustomerService(IFlightsUnitOfWork flightsUnitOfWork, IMapper mapper)
        {
            _flightsUnitOfWork = flightsUnitOfWork;
            _mapper = mapper;

        }
        public void CreateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new InvalidOperationException("Customer name can not be null");
            }
            if (isSameNameAndAddressAlreadyExist(customer.Name, customer.Address))
            {
                throw new InvalidOperationException("Same Customer from same address is already exist");
            }

            _flightsUnitOfWork.CustomerRepository
                .Add(_mapper.Map<Entities.Customer>(customer));

            _flightsUnitOfWork.Save();
        }
        public Customer GetCustomer(int id)
        {
            var customer = _flightsUnitOfWork.CustomerRepository.GetById(id);
            if (customer == null) return null;
            return _mapper.Map<Customer>(customer);
        }

        public IList<Customer> GetAllCustomers()
        {
            var customerEntities = _flightsUnitOfWork.CustomerRepository.GetAll();
            var customers = new List<Customer>();

            foreach (var entity in customerEntities)
            {
                var customer = _mapper.Map<Customer>(entity);
                customers.Add(customer);
            }
            return customers;
        }

        public (IList<Customer> records, int total, int totalDisplay) GetCustomers(
            int pageIndex, int pageSize, string searchText, string sortText)
        {
            var customersData = _flightsUnitOfWork.CustomerRepository.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText)
                || x.Address.Contains(searchText)
                || x.Age.ToString().Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from customer in customersData.data
                               select _mapper.Map<Customer>(customer)).ToList();

            return (resultData, customersData.total, customersData.totalDisplay);
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new InvalidOperationException("Customer is missing");

            //if (isSameNameAndAddressAlreadyExist(customer.Name, customer.Address))
            //    throw new InvalidOperationException("Same Customer from same address is already exist");
                
            var customerEntity = _flightsUnitOfWork.CustomerRepository.GetById(customer.Id);

            if (customerEntity != null)
            {
                _mapper.Map(customer, customerEntity);
                _flightsUnitOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException("Couldn't this customer");
            }
        }

        private bool isSameNameAndAddressAlreadyExist(string name, string address) =>
            _flightsUnitOfWork.CustomerRepository.GetCount(x => x.Name == name && x.Address == address) > 0;

        public void Delete(int id)
        {
            _flightsUnitOfWork.CustomerRepository.Remove(id);
            _flightsUnitOfWork.Save();
        }
    }
}

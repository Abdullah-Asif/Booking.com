using Booking.com.Flights.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.com.Flights.Contexts
{
    public class FlightsContext : Context, IFlightsContext
    {
        public FlightsContext(string connectionString, string migrationAssemblyName) 
            : base(connectionString, migrationAssemblyName)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Customer)
                .WithMany(c => c.Tickets)
                .HasForeignKey(f => f.CustomerId);
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}

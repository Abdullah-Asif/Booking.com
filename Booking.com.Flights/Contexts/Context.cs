using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.com.Flights.Contexts
{
    public class Context : DbContext
    {
        private string _connectionString;
        private string _migrationAssemblyName;

        public Context(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(_connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }
            base.OnConfiguring(dbContextOptionsBuilder);
        }
    }
}

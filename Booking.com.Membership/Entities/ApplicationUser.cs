using Microsoft.AspNetCore.Identity;
using System;

namespace Booking.com.Membership.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Booking.com.Membership.Entities
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
       
    }
}

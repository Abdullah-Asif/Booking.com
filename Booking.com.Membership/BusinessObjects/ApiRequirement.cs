using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.com.Membership.BusinessObjects
{
    public class ApiRequirement : IAuthorizationRequirement
    {
        public ApiRequirement()
        {
        }
    }
}

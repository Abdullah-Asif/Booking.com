﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Booking.com.Membership.BusinessObjects
{ 
    public class ViewRequirementHandler :
          AuthorizationHandler<ViewRequirement>
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               ViewRequirement requirement)
        {
            var claim = context.User.HasClaim("ViewPermission", "true");
            if (claim)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}

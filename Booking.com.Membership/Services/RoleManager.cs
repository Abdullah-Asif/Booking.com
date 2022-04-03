using System.Collections.Generic;
using Booking.com.Membership.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Booking.com.Membership.Services
{
    public class RoleManager : RoleManager<ApplicationRole>
    {
        public RoleManager(IRoleStore<ApplicationRole> store,
            IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            ILogger<RoleManager<ApplicationRole>> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}

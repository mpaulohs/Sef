using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Sfe.Domain.AggregatesModel.UserAggregate
{
    public class Role : IdentityRole
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }
}

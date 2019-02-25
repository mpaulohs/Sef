using Microsoft.AspNetCore.Identity;

namespace Sfe.Domain.AggregatesModel.UserAggregate
{
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public virtual Role Role { get; set; }
    }
}

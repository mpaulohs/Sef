using Microsoft.AspNetCore.Identity;

namespace Sfe.Domain.AggregatesModel.UserAggregate
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}

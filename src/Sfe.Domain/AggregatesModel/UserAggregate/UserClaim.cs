using Microsoft.AspNetCore.Identity;

namespace Sfe.Domain.AggregatesModel.UserAggregate
{
    public class UserClaim : IdentityUserClaim<string>
    {
        public virtual User User { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace Sfe.Domain.AggregatesModel.UserAggregate
{
    public class UserToken : IdentityUserToken<string>
    {
        public virtual User User { get; set; }
    }
}

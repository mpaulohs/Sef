using Microsoft.AspNetCore.Identity;

namespace Sfe.Domain.AggregatesModel.UserAggregate
{
    public class UserLogin : IdentityUserLogin<string>
    {
        public virtual User User { get; set; }
    }
}

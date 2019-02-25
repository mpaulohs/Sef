using Microsoft.AspNetCore.Identity;
using Sfe.Domain.AggregatesModel.UserAggregate;
using System.Threading.Tasks;

namespace Sfe.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<(IdentityResult result, User user)>  CreateAsync(string name, string email)
        {
            var _user = new User { UserName = email, Email = email, Name=name };
            var result = await _userManager.CreateAsync(_user, "florida#09");
            return (result, _user);
         }
    }
}

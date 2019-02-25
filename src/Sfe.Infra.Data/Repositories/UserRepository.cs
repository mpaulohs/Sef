using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sfe.Domain.AggregatesModel.UserAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sfe.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SfeContext _context;

        public UserRepository(UserManager<User> userManager, SfeContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<(IdentityResult result, User user)>  CreateAsync(string name, string email)
        {
            var _user = new User { UserName = email, Email = email, Name=name };
            var result = await _userManager.CreateAsync(_user, "florida#09");
            return (result, _user);
         }

        public async Task<User> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<IEnumerable<User>> ListAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}

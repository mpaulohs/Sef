using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sfe.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository
    {
        Task<(IdentityResult result, User user)> CreateAsync(string name, string email);
    }
}

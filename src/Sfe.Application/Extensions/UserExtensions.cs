using Sfe.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sfe.Application.Extensions
{
    public static class UserExtensions
    {
        public static IEnumerable<Usuario> ToIEnumerableUsuarios(this IEnumerable<User> users)
        {
            var l = new List<Usuario>();

            foreach (var item in users)
            {
                var u = new Usuario();
                u.Id = item.Id;
                u.Nome = item.Name;

                l.Add(u);
            }
            return l;           
        }
    }

    public class Usuario
    {
        public string Id { get; set; }
        public string Nome { get; set; }

    }
}

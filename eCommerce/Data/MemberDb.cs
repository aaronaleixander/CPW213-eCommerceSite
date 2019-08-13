using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Data
{
    public static class MemberDb
    {
        /// <summary>
        /// Adds a member to the database. Returns the member with their member id populated
        /// </summary>
        /// <param name="context">Database Context used</param>
        /// <param name="m">New member to be added</param>
        /// <returns></returns>
        public static async Task<Member> Add(GameContext context, Member m)
        {
            context.Members.Add(m);
            await context.SaveChangesAsync();
            return m;
        }
    }
}

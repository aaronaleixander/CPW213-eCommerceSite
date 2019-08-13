using eCommerce.Models;
using Microsoft.EntityFrameworkCore;
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

        public static async Task<bool> IsLoginValid(LoginViewModel model, GameContext context)
        {
            return await (from m in context.Members
                             where m.Username == model.UsernameOrEmail || m.EmailAddress == model.UsernameOrEmail
                                                         && m.Password == model.Password
                             select m).AnyAsync();
        }
    }
}

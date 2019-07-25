using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Data
{
    /// <summary>
    /// Db helper class for videogames. Helper methods.
    /// </summary>
    public static class VideoGameDb
    {
        /// <summary>
        /// Adds a videogame to the datastore and sets the id value. (Then can get rid of return.)
        /// </summary>
        /// <param name="g">The game to add</param>
        /// <param name="context">The DB context to use.</param>
        /// <returns>VideoGame with Id populated</returns>
        public static VideoGame Add(VideoGame g, GameContext context)
        {
            context.Add(g);
            context.SaveChanges();
            return g;
        }
    }
}

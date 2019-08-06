using eCommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Data
{
    public static class VideoGameDb
    {
        /// <summary>
        /// Returns one page worth of products, products are sorted alphabetically by title.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="pageNum">Page number of the products you want</param>
        /// <param name="pageSize">Number of products per page</param>
        /// <returns></returns>
        public static async Task<List<VideoGame>> GetGamesByPage(GameContext context, int pageNum, int pageSize)
        {

            // Make sure to call skip before take.
            // Be sure to call order bby first.
            List<VideoGame> games = await context.VideoGames
                                                 .OrderBy(vg => vg.Title)
                                                 .Skip((pageNum - 1) * pageSize)
                                                 .Take(pageSize)
                                                 .ToListAsync();

            // You could also just do return await
            return games;
        }



        /// <summary>
        /// Adds a VideoGame to the data store and sets
        /// the ID value
        /// </summary>
        /// <param name="g">The game to be added</param>
        /// <param name="context">The DB context to use</param>
        public static async Task<VideoGame> Add(VideoGame g, GameContext context)
        {
            await context.AddAsync(g);
            await context.SaveChangesAsync();
            return g;
        }

        /// <summary>
        /// Retrieves all games sorted in alphabetical order
        /// by title
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<List<VideoGame>> GetAllGames(GameContext context)
        {
            // LINQ Query syntax
            //List<VideoGame> games =
            //    await (from vidGame in context.VideoGames
            //        orderby vidGame.Title ascending
            //        select vidGame).ToListAsync();

            // LINQ Method Syntax
            List<VideoGame> games = await context.VideoGames
                                        .OrderBy(g => g.Title)
                                        .ToListAsync();

            return games;
        }

        public static async Task<VideoGame> GetGameById(int id, GameContext context)
        {
            VideoGame g = await (from game in context.VideoGames
                                 where game.Id == id
                                 select game).SingleOrDefaultAsync();

            return g;
        }

        public static async Task<VideoGame> UpdateGame(VideoGame g, GameContext context)
        {
            context.Update(g);
            await context.SaveChangesAsync();
            return g;
        }

        public static async Task DeleteById(int id, GameContext context)
        {
            // Create Videogame object with the id of the game we want to remove from the database.
            VideoGame g = new VideoGame()
            {
                Id = id
            };

            context.Entry(g).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}
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
        /// Searches for games that match the criteria and returns all games that match
        /// </summary>
        /// <param name="context"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async static Task<List<VideoGame>> Search(GameContext context, SearchCriteria criteria)
        {
            // SELECT * FROM VideoGames
            // but this does not query the database.
            IQueryable<VideoGame> allGames = from g
                                             in context.VideoGames
                                             select g;
            // MinPrice search
            if (criteria.MinPrice.HasValue)
            {
                // Adds to the where clause.
                // LINQ figures alot of the unter the hood work for you.
                // Price >= criteria.MinPrice
                allGames = from g
                           in allGames
                           where g.Price >= criteria.MinPrice
                           select g;
            }

            // MaxPrice search
            if (criteria.MaxPrice.HasValue)
            {
                allGames = from g
                           in allGames
                           where g.Price <= criteria.MaxPrice
                           select g;
            }

            // Title Search
            if (!string.IsNullOrWhiteSpace(criteria.Title))
            {
                // WHERE LEFT Title = Criteria.Title
                allGames = from g
                           in allGames
                           where g.Title.StartsWith(criteria.Title)
                           select g;
            }

            // Rating search
            if (!string.IsNullOrWhiteSpace(criteria.Rating))
            {
                // WHERE Rating = criteria.rating
                allGames = from g
                           in allGames
                           where g.Rating == criteria.Rating
                           select g;
            }

            // Send final query to database to return results
            // EF does not send query to database until it has to.
            return await allGames.ToListAsync();
        }


        /// <summary>
        /// Returns the total number of pages needed to have <paramref name="pAGE_SIZE"/> amount of products per page.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="pAGE_SIZE"></param>
        /// <returns></returns>
        public static async Task<int> GetTotalPages(GameContext context, int pAGE_SIZE)
        {
            int totalNumGames = await context.VideoGames.CountAsync();
            
            // Partial number of pages 
            
            double pages = (double)totalNumGames / pAGE_SIZE;
            return (int)Math.Ceiling(pages);

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
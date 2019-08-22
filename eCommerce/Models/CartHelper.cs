using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    /// <summary>
    /// Contains helper methods to manage the users in the shopping cart.
    /// </summary>
    public static class CartHelper
    {
        private const string CartCookie = "Cart";

        /// <summary>
        /// Gets current users videogames from their shopping cart If there are no games, empty list return.
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static List<VideoGame> GetGames(IHttpContextAccessor accessor)
        {
            //Get data out of cookie
            string data = accessor.HttpContext.Request.Cookies[CartCookie];

            if (string.IsNullOrWhiteSpace(data))
            {
                return new List<VideoGame>();
            }
            
            // else
            List<VideoGame> games = JsonConvert.DeserializeObject<List<VideoGame>>(data);

            return games;
        }


        /// <summary>
        /// Get total number of videogames in the cart.
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public static int GetGameCount(IHttpContextAccessor accessor)
        {
            List<VideoGame> allGames = GetGames(accessor);
            return allGames.Count(); 
        }

        /// <summary>
        /// Adds Videogame to the cart.
        /// </summary>
        /// <param name="g"> Videogame to be added</param>
        /// <param name="accessor"></param>
        public static void Add(VideoGame g, IHttpContextAccessor accessor)
        {
            List<VideoGame> games = GetGames(accessor);

            games.Add(g);

            string data = JsonConvert.SerializeObject(games);

            accessor.HttpContext.Response.Cookies.Append(CartCookie, data);
        }                                                         
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly GameContext _context;
        private readonly IHttpContextAccessor _httpAccessor;
        
        public CartController(GameContext context, IHttpContextAccessor httpAccessor)
        {
            _context = context;
            _httpAccessor = httpAccessor;
        }

        public async Task<IActionResult> Add(int id)
        {
            VideoGame g = await VideoGameDb.GetGameById(id, _context);
            CartHelper.Add(_httpAccessor, g);
            return RedirectToAction("Index", "Library");
        }

        public IActionResult Checkout() // create view w/ list/ model videogame- no ref scripts
        {
            List<VideoGame> games = CartHelper.GetGames(_httpAccessor);
            return View(games);
        }
    }
}
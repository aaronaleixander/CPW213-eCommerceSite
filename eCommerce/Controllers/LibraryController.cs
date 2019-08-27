using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class LibraryController : Controller
    {
        // ~~ readonly ~~  becomes only usable in the constructor.
        private readonly GameContext _context;

        public LibraryController(GameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Search(SearchCriteria criteria)
        {
            if (ValidSearch(criteria))
            {
                criteria.GameResults = await VideoGameDb.Search(_context, criteria);
            }
            else
            {
                criteria.GameResults = new List<VideoGame>();
            }
            return View(criteria);
        }

        /// <summary>
        /// Returns true if user searches by atleast 1 piece of criteria.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        private bool ValidSearch(SearchCriteria criteria)
        {
            if(criteria.Title == null && criteria.Rating == null && criteria.MaxPrice == null && criteria.MinPrice == null)
            {
                return false;
            }
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id) 
        {
            const int PAGE_SIZE = 3; // Constant Variable

            // id is the page number coming in
            // ?? is the null-coalescing operator
            // If id is not null set page to it, or if null use 1
            // same as using a if/else
            int page = id ?? 1;
            
            List<VideoGame> games = await VideoGameDb.GetGamesByPage(_context, page, PAGE_SIZE);

            int totalPages = await VideoGameDb.GetTotalPages(_context, PAGE_SIZE);
            ViewData["Pages"] = totalPages;
            ViewData["CurrentPage"] = page;
            return View(games);

        }
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(VideoGame game)
        {
            if (ModelState.IsValid)
            {
                await VideoGameDb.Add(game, _context);
                return RedirectToAction("Index");
            }
            
            // Return view with model including error messages.
            return View(game);
        }

        public async Task<IActionResult> Update(int id)
        {
            VideoGame game =
                await VideoGameDb.GetGameById(id, _context);
            return View(game);
        }

        [HttpPost]
        public async Task<IActionResult> Update(VideoGame g)
        {
            if (ModelState.IsValid)
            {
                await VideoGameDb.UpdateGame(g, _context);
                return RedirectToAction("Index");
            }

            // If there are any errors show the user the form again.
            return View(g);
        }

        public async Task<IActionResult> Delete(int id)
        {
            VideoGame game = await VideoGameDb.GetGameById(id, _context);

            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await VideoGameDb.DeleteById(id, _context);
            return RedirectToAction("Index");
        }
    }
}
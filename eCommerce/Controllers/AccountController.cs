using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly GameContext _context;

        public AccountController(GameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(Member m)
        {
            if (ModelState.IsValid)
            {
                await MemberDb.Add(_context, m);

                // Display success message on home page after redirection
                TempData["Message"] = "You registered sucessfully";
                return RedirectToAction("Index", "Home");
            }
            return View(m);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isMember = await MemberDb.IsLoginValid(model, _context);
                if (isMember)
                {
                    TempData["Message"] = "Logged in sucessfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Credentials Invalid
                    ModelState.AddModelError(string.Empty, "Im sorry your login was invalid.");
                }
            }
            return View(model);
        }
    }
}
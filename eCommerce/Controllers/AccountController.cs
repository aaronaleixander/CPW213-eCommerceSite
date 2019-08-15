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
    public class AccountController : Controller
    {
        /// <summary>
        /// Provides access to session data for the current user.
        /// </summary>
        private readonly IHttpContextAccessor _httpAccessor;

        private readonly GameContext _context;

        public AccountController(GameContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _httpAccessor = accessor;
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
                Member member = await MemberDb.IsLoginValid(model, _context);
                if (member != null)
                {
                    TempData["Message"] = "Logged in sucessfully";

                    // Create current session for the user.
                    _httpAccessor.HttpContext.Session.SetInt32("MemberId", member.MemberId);
                    _httpAccessor.HttpContext.Session.SetString("Username", member.Username);


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

        public IActionResult Logout()
        {
            _httpAccessor.HttpContext.Session.Clear();
            TempData["Message"] = " You have been logged out";
            return RedirectToAction("Index", "Home");
        }
    }
}
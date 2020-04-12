using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MvcSn.Models;
using MvcSn.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MvcSn.Controllers
{
    public class AuthenticationController : Controller
    {
        private SNContext db = new SNContext();

        [Authorize]
        public ViewResult Index()
        {
            return View(db.Users.ToList());
        }

        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UserLogin([Bind] User user)
        {
            var allUsers = db.Users.FirstOrDefault();
            if (db.Users.Any(u => u.Email == user.Email))
            {
                var currUser = db.Users.First(u => u.Email == user.Email);
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, currUser.Name),
                    new Claim(ClaimTypes.Surname, currUser.Surname),
                    new Claim(ClaimTypes.Email, currUser.Email),
                };

                var identity = new ClaimsIdentity(userClaims, "User Identity");

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UserLogout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Regitster()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Regitster([Bind] User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
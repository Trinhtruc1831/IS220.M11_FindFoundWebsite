using IS220M11.Data;
using IS220M11.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace IS220M11.Controllers
{
    public class accountController : Controller
    {
        private readonly FindFoundContext _context;
        public accountController(FindFoundContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        public bool IsPass(string username, string pass)
        {
            var query = from st in _context.accounts
                        where st.UName == username
                        select new
                        {
                            pass = st.UPass
                        };
            string input = query.First().pass;
            if (String.Equals(input, pass))
            {
                return true;
            }
            return false;
        }
        public int RoleUser(string username, string pass)
        {
            var query = from st in _context.accounts
                        where st.UName == username
                        select new
                        {
                            r = st.UType
                        };
            return query.First().r;
        }
        [HttpPost]
        public IActionResult Login(string username, string pass)
        {
            if (!string.IsNullOrEmpty(username) && string.IsNullOrEmpty(pass))
            {
                return RedirectToAction("Login");
            }
            ClaimsIdentity identity = null;
            if (IsPass(username, pass) && RoleUser(username, pass) == 1)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,username),
                    new Claim(ClaimTypes.Role,"Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("AdminIndex", "Home");
            }
            if (IsPass(username, pass) && RoleUser(username, pass) == 0)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,username),
                    new Claim(ClaimTypes.Role,"User")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                TempData["username"] = username;
                return RedirectToAction("UserIndex", "Home");
            }
            return View();
        }

        /*[HttpPost]*/
        /*public IActionResult Login(string username, string pass)
        {
            if (!string.IsNullOrEmpty(username) && string.IsNullOrEmpty(pass))
            {
                return RedirectToAction("Login");
            }
            ClaimsIdentity identity = null;
            bool isAuthenticate = false;
            if (IsPass(username, pass))
            {
                if (RoleUser(username, pass) == 0)
                {
                    identity = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name,username),
                    new Claim(ClaimTypes.Role,"User")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;

                }
                if (RoleUser(username, pass) == 1)
                {
                    identity = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name,username),
                    new Claim(ClaimTypes.Role,"Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;

                }
                if (isAuthenticate)
                {
                    var principal = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("UserIndex", "Home", new { username = username });
                    return RedirectToAction("UserIndex", "Home");
                }
            }
            return View();
        }*/

        /**************Account*****************/


    }
}

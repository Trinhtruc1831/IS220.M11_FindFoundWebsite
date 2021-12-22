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
<<<<<<< HEAD
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
=======
>>>>>>> 62e40faea64d1c82fd5dc4e3c255f3ad807f7c9d

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
<<<<<<< HEAD
        public bool IsPass(string username, string pass)
=======
        public bool IsPass()
>>>>>>> 62e40faea64d1c82fd5dc4e3c255f3ad807f7c9d
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
<<<<<<< HEAD
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
        public IActionResult Login(/*HttpContext contextus,*/ string username, string pass)
        {
            if (!string.IsNullOrEmpty(username) && string.IsNullOrEmpty(pass))
=======
            /*return EmailList[1];*/
            return true;
        }

        /**************Account*****************/

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
>>>>>>> 62e40faea64d1c82fd5dc4e3c255f3ad807f7c9d
            {
                return RedirectToAction("Login");
            }
            ClaimsIdentity identity = null;
<<<<<<< HEAD
            if (IsPass(username, pass) && RoleUser(username, pass) == 1)
=======
            bool isAuthenticate = false;
            if (username == "admin" && password == "a")
>>>>>>> 62e40faea64d1c82fd5dc4e3c255f3ad807f7c9d
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,username),
                    new Claim(ClaimTypes.Role,"Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
<<<<<<< HEAD
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("AdminIndex", "Home");
            }
            if (IsPass(username, pass) && RoleUser(username, pass) == 0)
=======
                isAuthenticate = true;
            }
            if (username == "demo" && password == "c")
>>>>>>> 62e40faea64d1c82fd5dc4e3c255f3ad807f7c9d
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,username),
                    new Claim(ClaimTypes.Role,"User")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
<<<<<<< HEAD
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                /*set value username into  session*/
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("UserIndex", "Home");
=======
                isAuthenticate = true;
            }
            if (isAuthenticate)
            {
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
>>>>>>> 62e40faea64d1c82fd5dc4e3c255f3ad807f7c9d
            }
            return View();
        }

<<<<<<< HEAD
        /*public void SetUsnSession(HttpContext context,string username)
        {
            // Lấy ISession
            var session = context.Session;
            string key_access = "usn";
            string usnval = username;
            
            string testnull = session.GetString(key_access);
            if (testnull == null)
            {
                usnval = "Home";
            }
            session.SetString(key_access, usnval);
        }*/
=======
        /*HOme*/
        [Authorize(Roles = "Admin,User")]
        /*public IActionResult Index()
        {
            return View();
        }*/
        [Authorize(Roles = "User")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
>>>>>>> 62e40faea64d1c82fd5dc4e3c255f3ad807f7c9d


    }

}


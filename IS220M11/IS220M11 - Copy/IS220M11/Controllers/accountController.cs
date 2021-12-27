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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Dynamic;

namespace IS220M11.Controllers
{
    public class accountController : Controller
    {
        private readonly FindFoundContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public accountController(FindFoundContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Login()
        {
            
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
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
        public async Task<IActionResult> Login(string username, string pass)
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
                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                /*set value username into  session*/
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("UserIndex", "Home");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete(".AspNetCore.Cookies", new CookieOptions()
            {
                Secure = true,
            });
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        /***********************************************************/
        public int getIDfUser(string user)
        {
            var query = from account in _context.accounts
                        where account.UName == user
                        select new
                        {
                            id = account.UserID
                        };
            return query.First().id;
        }
        public async Task<IActionResult> Edit(accountModel account)
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            if (ModelState.IsValid)
            {
                string folder = "";
                if (account.CoverPhoto != null)
                {

                    folder = "public/assets/ava/";
                    folder += Guid.NewGuid().ToString() +"_"+  account.CoverPhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    /*account.UAva = await UploadImage(folder, bookModel.CoverPhoto);*/
                    await account.CoverPhoto.CopyToAsync(new FileStream(serverFolder,FileMode.Create));
                }
                account.UAva = "/"+folder;
                await _context.accounts.AddAsync(account);
                await _context.SaveChangesAsync();
                return Ok();

            }

            return Ok();
        }

        public async Task<IActionResult> EditInfo()
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View();
        }
        public IFormFile CoverPhoto { get; set; }
        public List<Object> GetUserInfo()
        {
            var username = HttpContext.Session.GetString("username");
            ViewData["username"] = username;
            int iduser = getIDfUser(username);
            var query = from account in _context.accounts
                        where account.UserID == iduser
                        select new
                        {
                            name = account.UName,
                            status = account.UStatus,
                            email = account.UEmail,
                            phone = account.UPhone,
                            ava = account.UAva

                        };
            List<object> a = query.ToList<object>();
            return a;
        }
        
        public List<Object> GetUserPost()
        {
            var username = HttpContext.Session.GetString("username");
            ViewData["username"] = username;
            int iduser = getIDfUser(username);
            var query = from post in _context.posts
                        where post.PUserID == iduser
                        select new
                        {
                            date = post.PDate,
                            tit = post.PTitle
                        };
            List<object> a = query.ToList<object>();
            return a;
        }
        public async Task<IActionResult> Info()
        {
            dynamic infoModel = new ExpandoObject();
            infoModel.Accounts = GetUserInfo();
            infoModel.Posts = GetUserPost();
            /*Get username session*/
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View(infoModel);
        }

    }
}

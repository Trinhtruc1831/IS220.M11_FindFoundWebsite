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
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
        public int getIDUser(string user)
        {
            var query = from account in _context.accounts
                        where account.UName == user
                        select new
                        {
                            id = account.UserID
                        };
            return query.First().id;
        }
        /*public async Task<IActionResult> InfoCreatenekkkk(accountModel account)
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
                    *//*account.UAva = await UploadImage(folder, bookModel.CoverPhoto);*//*
                    await account.CoverPhoto.CopyToAsync(new FileStream(serverFolder,FileMode.Create));
                }
                account.UAva = "/"+folder;
                await _context.accounts.AddAsync(account);
                await _context.SaveChangesAsync();
                return Ok();

            }

            return Ok();
        }*/
        
        public async Task<IActionResult> Edit(accountModel account)
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            List<Object> thisModel = GetUserInfo();
            string name = "";
            string status = "";
            string email = "";
            string phone = "";
            string ava = "";
            string pass = "";
            foreach (object item in thisModel)
            {
                name = item.GetType().GetProperty("name").GetValue(item, null).ToString();
                status = item.GetType().GetProperty("status").GetValue(item, null).ToString();
                email = item.GetType().GetProperty("email").GetValue(item, null).ToString();
                phone = item.GetType().GetProperty("phone").GetValue(item, null).ToString();
                ava = item.GetType().GetProperty("ava").GetValue(item, null).ToString();
                pass = item.GetType().GetProperty("pass").GetValue(item, null).ToString();

            }
            int userid = getIDUser(account.UName);
            account.UserID = userid;            
            if (ModelState.IsValid)
            {
                string folder = "";
                if (account.CoverPhoto != null)
                {

                    folder = "public/assets/ava/";
                    folder += Guid.NewGuid().ToString() + "_" + account.CoverPhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    /*account.UAva = await UploadImage(folder, bookModel.CoverPhoto);*/
                    await account.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    account.UAva = "/" + folder;
                }
                else
                {
                    account.UAva = ava;
                }
                if(account.UName == null)
                {
                    account.UName = name;
                }
                if (account.UEmail == null)
                {
                    account.UEmail = email;
                }
                if (account.UPass == null)
                {
                    account.UPass = pass;
                }
                if (account.UPhone == null)
                {
                    account.UPhone = phone;
                }
                _context.Update(account);
                await _context.SaveChangesAsync();
                return RedirectToAction("account", "Info"); ;

            }

            return Error();
        }

        public async Task<IActionResult> EditInfo()
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View();
        }
        public IActionResult GetPostInterstJson()
        {
            var username = HttpContext.Session.GetString("username");
            ViewData["username"] = username;
            int iduser = getIDUser(username);
            var query = from interest in _context.interests
                        join post in _context.posts on interest.InPostID equals post.PostID
                        where interest.InUserID == iduser
                        select new
                        {
                            date = interest.InDate,
                            tit = post.PTitle
                        };
            return new JsonResult(query);
        }
        public IActionResult GetPostUserJson()
        {
            var username = HttpContext.Session.GetString("username");
            ViewData["username"] = username;
            int iduser = getIDUser(username);
            var query = from post in _context.posts
                        where post.PUserID == iduser
                        select new
                        {
                            date = post.PDate,
                            tit = post.PTitle,
                            postid = post.PostID
                        };
            return new JsonResult(query);
        }
        public List<Object> GetUserInfo()
        {
            var username = HttpContext.Session.GetString("username");
            ViewData["username"] = username;
            int iduser = getIDUser(username);
            var query = from account in _context.accounts
                        where account.UserID == iduser
                        select new
                        {
                            name = account.UName,
                            status = account.UStatus,
                            email = account.UEmail,
                            phone = account.UPhone,
                            ava = account.UAva,
                            pass = account.UPass

                        };
            List<object> a = query.ToList<object>();
            return a;
        }
        public List<Object> GetUserPost()
        {
            var username = HttpContext.Session.GetString("username");
            ViewData["username"] = username;
            int iduser = getIDUser(username);
            var query = from post in _context.posts
                        where post.PUserID == iduser
                        select new
                        {
                            date = post.PDate,
                            tit = post.PTitle,
                            postid = post.PostID
                        };
            List<object> a = query.ToList<object>();
            return a;
        }
        public async Task<IActionResult> Info()
        {
            dynamic infoModel = new ExpandoObject();
            infoModel.Accounts = GetUserInfo();
            infoModel.Posts = GetUserPost();//Do dùng ajax r nên thực ra model này không sử dụng nữa
            /*Get username session*/
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View(infoModel);
        }
        public List<Object> GetGuestInfo(string user)
        {
            var username = HttpContext.Session.GetString("username");
            ViewData["username"] = username;
            int iduser = getIDUser(user);
            var query = from account in _context.accounts
                        where account.UserID == iduser
                        select new
                        {
                            name = account.UName,
                            status = account.UStatus,
                            email = account.UEmail,
                            phone = account.UPhone,
                            ava = account.UAva,
                            pass = account.UPass

                        };
            List<object> a = query.ToList<object>();
            return a;
        }
        public List<Object> GetGuestPost(string user)
        {
            var username = HttpContext.Session.GetString("username");
            ViewData["username"] = username;
            int iduser = getIDUser(user);
            var query = from post in _context.posts
                        where post.PUserID == iduser
                        select new
                        {
                            date = post.PDate,
                            tit = post.PTitle,
                            postid = post.PostID
                        };
            List<object> a = query.ToList<object>();
            return a;
        }
        public async Task<IActionResult> GuestInfo(string? id)
        {
            dynamic infoModel = new ExpandoObject();
            infoModel.Accounts = GetGuestInfo(id);
            infoModel.Posts = GetGuestPost(id);
            /*Get username session*/
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View(infoModel);
        }
        private bool accountModelExists(int id)
        {
            return _context.accounts.Any(e => e.UserID == id);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

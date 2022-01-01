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
using Microsoft.AspNetCore.Authorization;


namespace IS220M11.Controllers
{
    public class postController : Controller
    {
        private readonly FindFoundContext _context;

        public postController(FindFoundContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var query = from post in _context.posts
                        join pic in _context.pictures on post.PostID equals pic.IPostID
                        where pic.IOrder == 1
                        select new
                        {
                            price = post.PPrice,
                            tit = post.PTitle,
                            tnpic = pic.ILink
                        };
            List<object> a = query.ToList<object>();
            return View(a);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IQueryable<object> GetPostTit()
        {
            var query = from post in _context.posts
                        join pic in _context.pictures on post.PostID equals pic.IPostID
                        where pic.IOrder == 1
                        select new
                        {
                            postid = post.PostID,
                            price = post.PPrice,
                            tit = post.PTitle,
                            tnpic = pic.ILink
                        };
            return query;
        }
        public int GetUserID(string user)
        {
            var query = from account in _context.accounts
                        where account.UName == user
                        select new
                        {
                            UserID = account.UserID
                        };
            return query.FirstOrDefault().UserID;
        }
        public string GetUserName(int id)
        {
            var query = from account in _context.accounts
                        where account.UserID == id
                        select new
                        {
                            UserName = account.UName
                        };
            return query.FirstOrDefault().UserName;
        }
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string PTitle, string PDesc, int PPrice, string user)
        {
            
            int iduser = GetUserID(user);
            postModel post = new postModel();
            if (ModelState.IsValid)
            {
                post.PTitle = PTitle;
                post.PDesc = PDesc;
                post.PPrice = PPrice;
                post.PUserID = iduser;
                post.PDate = DateTime.Now;
                await _context.posts.AddAsync(post);
                await _context.SaveChangesAsync();
                
                HttpContext.Session.SetString("postid", post.PostID.ToString());
                HttpContext.Session.SetString("Iorder", "1");
                return RedirectToAction("AddImage", "picture");
            }

            ViewData["username"] = HttpContext.Session.GetString("username");
            return Error();
        }

        public string GetPicPost(int id)
        {
            var query = from pic in _context.pictures
                        where pic.IPostID == id

                        where pic.IOrder == 1
                        select new
                        {
                            tnpic = pic.ILink
                        };
            return query.FirstOrDefault().tnpic;
        }
        [Authorize(Roles = "User,Admin")]
        public IActionResult Post(int? id)
        {
            var query = from post in _context.posts
                        join pic in _context.pictures on post.PostID equals pic.IPostID
                        join account in _context.accounts on post.PUserID equals account.UserID
                        select new
                        {
                            postid = post.PostID,
                            price = post.PPrice,
                            tit = post.PTitle,
                            tnpic = pic.ILink,
                            tdesc = post.PDesc,
                            porder = pic.IOrder,
                            postuser = account.UName

                        };
            TempData["postId"] = id;
            ViewData["username"] = HttpContext.Session.GetString("username");
            List<object> a = query.ToList<object>();
            return View(a);
        }

        public int getIDPost(string post)
        {
            var query = from p in _context.posts
                        where p.PTitle == post
                        select new
                        {
                            id = p.PostID
                        };
            return query.FirstOrDefault().id;
        }
        public async Task<IActionResult> Edit(postModel post)
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            IQueryable thisModel = GetPostTit();
            string title = "";
            string desc = "";
            string price = "";
            
            foreach (object item in thisModel)
            {
                title = item.GetType().GetProperty("title").GetValue(item, null).ToString();
                desc = item.GetType().GetProperty("desc").GetValue(item, null).ToString();
                price = item.GetType().GetProperty("price").GetValue(item, null).ToString();
                // phone = item.GetType().GetProperty("phone").GetValue(item, null).ToString();
            }
            int postid = getIDPost(post.PTitle);
            post.PostID = postid;            
            if (ModelState.IsValid)
            {
                if(post.PTitle == null)
                {
                    post.PTitle = title;
                }
                if (post.PDesc == null)
                {
                    post.PDesc = desc;
                }
                if (post.PPrice.ToString() == null)
                {
                    post.PPrice = Int32.Parse(price);
                }
                _context.Update(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("post", "Post"); 

            }

            return Error();
        }
        public async Task<IActionResult> EditPost(int id)
        {
            ViewData["postid"]= id;
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View();
        }
    }

}

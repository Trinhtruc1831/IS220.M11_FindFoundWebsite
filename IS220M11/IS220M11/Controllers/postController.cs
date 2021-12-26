using IS220M11.Data;
using IS220M11.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

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
                            tnpic = pic.ILink,
                            tdesc = post.PDesc
                        };
            return query;
        }

        public IActionResult Create()
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PTitle, PDesc, PPrice")] postModel post)
        {
            if (ModelState.IsValid)
            {
                post.PUserID = 5;
                post.PDate = DateTime.Now;
                _context.Add(post);
                await _context.SaveChangesAsync();
                // picture.IPostID = post.PostID;
                // _context.Add(picture);
                // await _context.SaveChangesAsync();
            }
            ViewData["username"] = HttpContext.Session.GetString("username");
            return RedirectToAction("AddImage", "picture");
        }

        // public int UserID(string userID, string pass)
        // {
        //     var query = from st in _context.accounts
        //                 where st.UName == username
        //                 select new
        //                 {
        //                     r = st.UType
        //                 };
        //     return query.First().r;
        // }
        public IActionResult Post(int postId)
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
            TempData["postId"] = postId;
            List<object> a = query.ToList<object>();
            return View(a);


        }
        public IActionResult UserIndex()
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
            /*Get username session*/
            ViewData["username"] = HttpContext.Session.GetString("username");
            List<object> a = query.ToList<object>();
            return View(a);
        }

    }

}

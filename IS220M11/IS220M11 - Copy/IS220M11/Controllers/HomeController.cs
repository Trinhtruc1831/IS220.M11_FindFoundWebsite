using IS220M11.Data;
using IS220M11.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;


namespace IS220M11.Controllers
{
    public class HomeController : Controller
    {
        private readonly FindFoundContext _context;
        public HomeController(FindFoundContext context)
        {
            _context = context;
        }
        //Get model
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
        private List<Object> GetPostPic()
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
            List<object> a = query.ToList<object>();
            return a;
        }
        private List<object> GetChatHis()
        {
            var query = from chat in _context.chats
                        join account in _context.accounts on chat.ChUserID equals account.UserID
                        orderby chat.ChatID descending
                        select new
                        {
                            user = account.UName,
                            mess = chat.ChContent,
                            date = chat.ChDate
                        };
            List<object> a = query.ToList<object>();
            return a;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminIndex()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public IActionResult UserIndex()
        {
            dynamic indexModel = new ExpandoObject();
            indexModel.Posts = GetPostPic();
            indexModel.Chats = GetChatHis();
            /*Get username session*/
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View(indexModel);
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
        public async Task<IActionResult> CreateMess(string mess, string user, string day)
        {
            var query = from row in _context.chats
                        select row;
            var count = query.Count();
            if (count >= 10)
            {
                int id = query.FirstOrDefault().ChatID;
                var chatModel = await _context.chats.FindAsync(id);
                _context.chats.Remove(chatModel);
                await _context.SaveChangesAsync();
            }
            int iduser = getIDfUser(user);
            chatModel chat = new chatModel();
            if (ModelState.IsValid)
            {
                chat.ChContent = mess;
                chat.ChUserID = iduser;
                chat.ChDate = day;
                await _context.chats.AddAsync(chat);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return Error();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

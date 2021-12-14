using IS220M11.Data;
using IS220M11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

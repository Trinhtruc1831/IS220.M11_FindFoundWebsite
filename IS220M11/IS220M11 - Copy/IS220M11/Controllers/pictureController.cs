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
    public class pictureController:Controller
    {
        private readonly FindFoundContext _context;
    
        public pictureController(FindFoundContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.pictures.ToListAsync());
        }

        public IActionResult AddImage()
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage([Bind("ILink")] pictureModel picture, [Bind("PostID")] postModel post)
        {
            if (ModelState.IsValid)
            {
                picture.IPostID = post.PostID;
                _context.Add(picture);
                await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
            }
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View();
        }
    }
}
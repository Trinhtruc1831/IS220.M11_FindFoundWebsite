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
    public class postController:Controller
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
                            price = post.PPrice,
                            tit = post.PTitle,
                            tnpic = pic.ILink
                        };
            List<object> a = query.ToList<object>();
            return query;
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( [Bind("PTitle,PPrice")] postModel post, [Bind("ILink")] pictureModel picture)
        {
            // var size = files.Sum(f => f.Length);
            // var filePaths = new List<string>();
            // foreach (var formFile in files)
            // {
            //     if (formFile.Length > 0)
            //     {
            //         // var filePath = Path.Combine
            //     }
            // }
            if (ModelState.IsValid)
            {
                post.PUserID = 1;
                post.PDate = DateTime.Now;
                _context.Add(post);
                await _context.SaveChangesAsync();
                picture.IPostID = post.PostID;
                _context.Add(picture);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            
            var postModel = await _context.posts
            .FirstOrDefaultAsync(m => m.PostID == id);
            var pictureModel = await _context.pictures
            .FirstOrDefaultAsync(m => m.IPostID == id);
            if (postModel == null && pictureModel == null)
            {
                return NotFound();
            }
            return View(postModel);
        }

    }

}

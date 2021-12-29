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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Web;
using System.Diagnostics;

namespace IS220M11.Controllers
{
    public class pictureController : Controller
    {
        private readonly FindFoundContext _context;

        private readonly IWebHostEnvironment _webHostEnviroment;

        public pictureController(FindFoundContext context,
                IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.pictures.ToListAsync());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AddImage()
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            ViewData["postid"] = HttpContext.Session.GetString("postid");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(pictureModel pic)
        {
            int postid = Int32.Parse(HttpContext.Session.GetString("postid"));
            if (ModelState.IsValid)
            {
                string folder = "";
                if (pic.Image != null)
                {
                    folder = "public/assets/img/";
                    folder += Guid.NewGuid().ToString() + "_" + pic.Image.FileName;
                    string serverFolder = Path.Combine(_webHostEnviroment.WebRootPath, folder);

                    await pic.Image.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                int order = Int32.Parse(HttpContext.Session.GetString("Iorder"));
                pic.IOrder = order;
                order = order + 1;
                HttpContext.Session.SetString("Iorder", order.ToString());

                pic.ILink = "/" + folder;
                pic.IPostID = postid;
                await _context.pictures.AddAsync(pic);
                await _context.SaveChangesAsync();
                return RedirectToAction("AddImage", "picture");
            }
            ViewData["username"] = HttpContext.Session.GetString("username");
            return Error();

        }
    }
}
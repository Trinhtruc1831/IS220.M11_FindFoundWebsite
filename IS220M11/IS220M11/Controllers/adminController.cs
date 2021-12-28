using IS220M11.Data;
using IS220M11.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS220M11.Controllers
{
    public class adminController : Controller
    {
        private readonly FindFoundContext _context;
        public adminController(FindFoundContext context)
        {
            _context = context;
        }

        /*Account*/
        [Authorize(Roles = "Admin")]
        // GET: account
        public async Task<IActionResult> Index()
        {
            return View(await _context.accounts.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListMember()
        {
            return View(await _context.accounts.ToListAsync());
        }

        // GET: account/Details/5
        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountModel = await _context.accounts
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (accountModel == null)
            {
                return NotFound();
            }

            return View(accountModel);
        }

        // GET: account/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,UName,UPass,UType,UStatus,UEmail,UPhone,Hideinfo")] accountModel accountModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountModel);
        }

        // GET: account/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountModel = await _context.accounts.FindAsync(id);
            if (accountModel == null)
            {
                return NotFound();
            }
            return View(accountModel);
        }

        // POST: account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,UName,UPass,UType,UStatus,UEmail,UPhone,Hideinfo")] accountModel accountModel)
        {
            if (id != accountModel.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!accountModelExists(accountModel.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(accountModel);
        }

        // GET: account/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountModel = await _context.accounts
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (accountModel == null)
            {
                return NotFound();
            }

            return View(accountModel);
        }

        // POST: account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountModel = await _context.accounts.FindAsync(id);
            _context.accounts.Remove(accountModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool accountModelExists(int id)
        {
            return _context.accounts.Any(e => e.UserID == id);
        }*/

        /*Post*/

        // GET: post
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListPost()
        {
            var findFoundContext = _context.posts.Include(p => p.account);
            return View(await findFoundContext.ToListAsync());
        }

        // GET: post/Details/5
        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postModel = await _context.posts
                .Include(p => p.account)
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (postModel == null)
            {
                return NotFound();
            }

            return View(postModel);
        }

        // GET: post/Create
        public IActionResult Create()
        {
            ViewData["PUserID"] = new SelectList(_context.accounts, "UserID", "UserID");
            return View();
        }

        // POST: post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostID,PUserID,PTitle,PPrice,Heart,PStatus,PDate")] postModel postModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PUserID"] = new SelectList(_context.accounts, "UserID", "UserID", postModel.PUserID);
            return View(postModel);
        }

        // GET: post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postModel = await _context.posts.FindAsync(id);
            if (postModel == null)
            {
                return NotFound();
            }
            ViewData["PUserID"] = new SelectList(_context.accounts, "UserID", "UserID", postModel.PUserID);
            return View(postModel);
        }

        // POST: post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostID,PUserID,PTitle,PPrice,Heart,PStatus,PDate")] postModel postModel)
        {
            if (id != postModel.PostID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!postModelExists(postModel.PostID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PUserID"] = new SelectList(_context.accounts, "UserID", "UserID", postModel.PUserID);
            return View(postModel);
        }

        // GET: post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postModel = await _context.posts
                .Include(p => p.account)
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (postModel == null)
            {
                return NotFound();
            }

            return View(postModel);
        }

        // POST: post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postModel = await _context.posts.FindAsync(id);
            _context.posts.Remove(postModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool postModelExists(int id)
        {
            return _context.posts.Any(e => e.PostID == id);
        }*/
    }

}


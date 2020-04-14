using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcSn.Data;
using MvcSn.Models;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Authorization;

namespace MvcSn.Controllers
{
    public class PostsController : Controller
    {
        private readonly SNContext _context;
        private IAuthorizationService authorizationService;

        public PostsController(SNContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            this.authorizationService = authorizationService;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var posts = _context.Posts.ToList();
            return View(await _context.Posts.ToListAsync());
        }


        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Text,Date")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Date = DateTime.Now;
                post.Comments = new Collection<Comment>();
                var username = User.Identity.Name;
                var sender = _context.Users.First(x => x.UserName == username);
                post.Sender = sender;
                post.SenderName = sender.UserName;
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Text,Date")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }
            var timeCheck = await authorizationService.AuthorizeAsync(User, post, "EditTime");
            if (!timeCheck.Succeeded)
                return RedirectToAction("Index", "Posts");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            var timeCheck = await authorizationService.AuthorizeAsync(User, post, "EditTime");
            if (!timeCheck.Succeeded)
                return RedirectToAction("Index", "Posts");
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}

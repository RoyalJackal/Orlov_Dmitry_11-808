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
using Microsoft.AspNetCore.Authorization;
using MvcSn.Sender;

namespace MvcSn.Controllers
{
    public class CommentsController : Controller
    {
        private readonly SNContext _context;
        private IAuthorizationService _authorizationService;
        private IMessageSender _sender;

        public CommentsController(SNContext context, IAuthorizationService authorizationService,
            IMessageSender sender)
        {
            _context = context;
            _authorizationService = authorizationService;
            _sender = sender;
        }

        public IActionResult Index(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Posts");
            var comments = _context.Comments
                .Where(x => x.Post.Id == _context.Posts
                .FirstOrDefault(y => y.Id == id).Id)
                .ToList();
            ViewBag.Comments = comments;
            return View();
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Text,Date")] Comment comment, int? id)
        {
            comment.Id = 0;
            if (ModelState.IsValid)
            {
                comment.Date = DateTime.Now;               
                var post = _context.Posts.First(x => x.Id == id);
                var username = User.Identity.Name;
                var sender = _context.Users.First(x => x.UserName == username);
                comment.Post = post;
                comment.Sender = sender;
                comment.SenderName = sender.UserName;
                _context.Add(comment);
                _sender.Send();
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Text,Date")] Comment comment)
        {
            var timeCheck = await _authorizationService.AuthorizeAsync(User, comment, "EditTime");

            if (!timeCheck.Succeeded)
                return RedirectToAction("Index", "Posts");
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {           
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
            var timeCheck = await _authorizationService.AuthorizeAsync(User, comment, "EditTime");
            if (!timeCheck.Succeeded)
                return RedirectToAction("Index", "Posts");
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            var timeCheck = await _authorizationService.AuthorizeAsync(User, comment, "EditTime");
            if (!timeCheck.Succeeded)
                return RedirectToAction("Index", "Posts");
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackendCapstone.Data;
using BackendCapstone.Models;
using Microsoft.AspNetCore.Identity;

namespace BackendCapstone.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessagesController(ApplicationDbContext ctx,
                          UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var applicationDbContext = _context.Messages.Include(m => m.SendingUser).Include(m => m.ReceivingUser).Where(m => m.SendingUserId == user.Id || m.ReceivingUserId == user.Id).OrderByDescending(m => m.MessageId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.SendingUser)
                .Include(m => m.ReceivingUser)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();

            ViewData["SendingUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");

            if (user.IsVet != true)
            {
                ViewData["ReceivingUserId"] = new SelectList(_context.ApplicationUsers.Where(v => v.IsVet == true), "Id", "FullName");
                return View();
            }
            else{
                ViewData["ReceivingUserId"] = new SelectList(_context.ApplicationUsers.Where(u => u.IsVet != true), "Id", "FullName");
                return View();
            }
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageId,ReceivingUserId,SendingUserId,Messages")] Message message)
        {
            var user = await GetCurrentUserAsync();
            message.SendingUser = user;
            message.SendingUserId = user.Id;

            ModelState.Remove("SendingUserId");
            ModelState.Remove("SendingUser");

            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SendingUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", message.SendingUserId);
            ViewData["ReceivingUserId"] = new SelectList(_context.ApplicationUsers, "Id", "FullName", message.ReceivingUserId);
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["SendingUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", message.SendingUserId);
            ViewData["ReceivingUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", message.ReceivingUserId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageId,ReceivingUserId,SendingUserId,Messages")] Message message)
        {
            if (id != message.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.MessageId))
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
            ViewData["SendingUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", message.SendingUserId);
            ViewData["ReceivingUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", message.ReceivingUserId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.SendingUser)
                .Include(m => m.ReceivingUser)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }
    }
}

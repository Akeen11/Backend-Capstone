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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BackendCapstone.Controllers
{
    public class PetsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _appEnvironment;

        public PetsController(ApplicationDbContext ctx,
                          UserManager<ApplicationUser> userManager,
                          IHostingEnvironment appEnvironment)
        {
            _userManager = userManager;
            _context = ctx;
            _appEnvironment = appEnvironment;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> VetUserIndex()
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return RedirectToAction("Index", "Pets");
            }

            if (user.IsVet == true)
            {
                var applicationDbContext = _context.Pets.Include(p => p.User).Include(p => p.Vet).Where(p => p.VetId == user.Id);
                return View(await applicationDbContext.ToListAsync());
            }
            else if (user.IsVet == false)
            {
                var applicationDbContext = _context.Pets.Include(p => p.User).Include(p => p.Vet).Where(p => p.UserId == user.Id);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Pets.Include(p => p.User).Include(p => p.Vet);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: Pets
        public async Task<IActionResult> Index()
        {
                var applicationDbContext = _context.Pets.Include(p => p.User).Include(p => p.Vet);
                return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.User)
                .Include(p => p.Vet)
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["VetId"] = new SelectList(_context.ApplicationUsers.Where(v => v.IsVet == true), "Id", "FullName");
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetId,VetId,UserId,Name,Age,Status,ImagePath")] Pet pet, IFormFile file)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("User");

            // make sure file is selected
            if (file == null || file.Length == 0) return Content("file not selected");

            // get path location to store img
            string path_Root = _appEnvironment.WebRootPath;

            // get only file name without file path
            var trimmedFileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);

            // store file location
            string path_to_Images = path_Root + "\\User_Files\\Images\\" + trimmedFileName;

            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                pet.User = user;
                pet.UserId = user.Id;

                // copy file to target
                using (var stream = new FileStream(path_to_Images, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                pet.ImagePath = trimmedFileName;

                _context.Add(pet);
                await _context.SaveChangesAsync();
                ViewData["FilePath"] = path_to_Images;
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", pet.UserId);
            if (user.IsVet != true)
            {
                ViewData["VetId"] = new SelectList(_context.ApplicationUsers.Where(v => v.IsVet == true), "Id", "FullName", pet.VetId);
                return View(pet);
            }
            else
            {
                ViewData["VetId"] = new SelectList(_context.ApplicationUsers.Where(u => u.IsVet != true), "Id", "FullName", pet.VetId);
                return View(pet);
            }
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetId,VetId,UserId,Name,Age,Status,ImagePath")] Pet pet)
        {
            if (id != pet.PetId)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();
            pet.User = user;
            pet.UserId = user.Id;
            pet.ImagePath = pet.ImagePath;

            ModelState.Remove("User");
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.PetId))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", pet.UserId);
            ViewData["VetId"] = new SelectList(_context.ApplicationUsers, "Id", "FullName", pet.VetId);
            return View(pet);
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.User)
                .Include(p => p.Vet)
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.PetId == id);
        }
    }
}

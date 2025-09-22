using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Controllers
{
    public class ContactsController : Controller
    {
        private readonly AppDbContext _context;

        public ContactsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /contacts/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contacts = await _context.Contacts
                .Include(c => c.Category)
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .ToListAsync();
            return View(contacts);
        }

        // GET: /contacts/details/5/john-doe
        [HttpGet]
        public async Task<IActionResult> Details(int id, string? slug)
        {
            var contact = await _context.Contacts
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.ContactId == id);

            if (contact == null) return NotFound();

            // If slug missing or incorrect, redirect to canonical URL (helps SEO & URLs consistent)
            if (string.IsNullOrWhiteSpace(slug) || !string.Equals(slug, contact.Slug, StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction(nameof(Details), new { id = id, slug = contact.Slug });
            }

            return View(contact);
        }

        // GET: /contacts/create
        [HttpGet]
        public IActionResult Create()
        {
            PopulateCategoriesDropDown();
            return View("CreateEdit", new Contact());
        }

        // POST: /contacts/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.DateAdded = DateTime.UtcNow;
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateCategoriesDropDown(contact.CategoryId);
            return View("CreateEdit", contact);
        }

        // GET: /contacts/edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();

            PopulateCategoriesDropDown(contact.CategoryId);
            return View("CreateEdit", contact);
        }

        // POST: /contacts/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact contact)
        {
            if (id != contact.ContactId) return BadRequest();

            if (ModelState.IsValid)
            {
                // Retrieve existing contact to preserve DateAdded and avoid overwriting navigation property
                var existing = await _context.Contacts.AsNoTracking().FirstOrDefaultAsync(c => c.ContactId == id);
                if (existing == null) return NotFound();

                // Ensure DateAdded is preserved
                contact.DateAdded = existing.DateAdded;

                _context.Update(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = contact.ContactId, slug = contact.Slug });
            }

            PopulateCategoriesDropDown(contact.CategoryId);
            return View("CreateEdit", contact);
        }

        // GET: /contacts/delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.Contacts.Include(c => c.Category).FirstOrDefaultAsync(c => c.ContactId == id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        // POST: /contacts/delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private void PopulateCategoriesDropDown(object? selected = null)
        {
            ViewBag.Categories = new SelectList(_context.Categories.OrderBy(c => c.CategoryName), "CategoryId", "CategoryName", selected);
        }
    }
}
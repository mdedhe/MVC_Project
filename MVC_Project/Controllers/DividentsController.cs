using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project.DataAccess;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class DividentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DividentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dividents
        public async Task<IActionResult> Index()
        {
            return View(await _context.Divident.ToListAsync());
        }

        // GET: Dividents/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var divident = await _context.Divident
                .FirstOrDefaultAsync(m => m.Exdate == id);
            if (divident == null)
            {
                return NotFound();
            }

            return View(divident);
        }

        // GET: Dividents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dividents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Exdate,Payment_date,Record_date,Amount,type")] Divident divident)
        {
            if (ModelState.IsValid)
            {
                _context.Add(divident);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(divident);
        }

        // GET: Dividents/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var divident = await _context.Divident.FindAsync(id);
            if (divident == null)
            {
                return NotFound();
            }
            return View(divident);
        }

        // POST: Dividents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("Exdate,Payment_date,Record_date,Amount,type")] Divident divident)
        {
            if (id != divident.Exdate)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(divident);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DividentExists(divident.Exdate))
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
            return View(divident);
        }

        // GET: Dividents/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var divident = await _context.Divident
                .FirstOrDefaultAsync(m => m.Exdate == id);
            if (divident == null)
            {
                return NotFound();
            }

            return View(divident);
        }

        // POST: Dividents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var divident = await _context.Divident.FindAsync(id);
            _context.Divident.Remove(divident);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DividentExists(DateTime id)
        {
            return _context.Divident.Any(e => e.Exdate == id);
        }
    }
}

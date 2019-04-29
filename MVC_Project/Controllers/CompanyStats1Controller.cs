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
    public class CompanyStats1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyStats1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CompanyStats1
        public async Task<IActionResult> Index()
        {
            return View(await _context.CompanyStats.ToListAsync());
        }

        // GET: CompanyStats1/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyStats = await _context.CompanyStats
                .FirstOrDefaultAsync(m => m.Symbol == id);
            if (companyStats == null)
            {
                return NotFound();
            }

            return View(companyStats);
        }

        // GET: CompanyStats1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyStats1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Symbol,CompanyName,MarketCap,Revenue,GrossProfit,Debt")] CompanyStats companyStats)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyStats);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyStats);
        }

        // GET: CompanyStats1/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyStats = await _context.CompanyStats.FindAsync(id);
            if (companyStats == null)
            {
                return NotFound();
            }
            return View(companyStats);
        }

        // POST: CompanyStats1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Symbol,CompanyName,MarketCap,Revenue,GrossProfit,Debt")] CompanyStats companyStats)
        {
            if (id != companyStats.Symbol)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyStats);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyStatsExists(companyStats.Symbol))
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
            return View(companyStats);
        }

        // GET: CompanyStats1/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyStats = await _context.CompanyStats
                .FirstOrDefaultAsync(m => m.Symbol == id);
            if (companyStats == null)
            {
                return NotFound();
            }

            return View(companyStats);
        }

        // POST: CompanyStats1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var companyStats = await _context.CompanyStats.FindAsync(id);
            _context.CompanyStats.Remove(companyStats);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyStatsExists(string id)
        {
            return _context.CompanyStats.Any(e => e.Symbol == id);
        }
    }
}

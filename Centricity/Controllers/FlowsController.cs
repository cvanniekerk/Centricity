using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Centricity.Data;
using Centricity.Models;

namespace Centricity.Controllers
{
    public class FlowsController : Controller
    {
        private readonly CentricityContext _context;

        public FlowsController(CentricityContext context)
        {
            _context = context;
        }

        // GET: Flows
        public async Task<IActionResult> Index()
        {
              return _context.Flow != null ? 
                          View(await _context.Flow.ToListAsync()) :
                          Problem("Entity set 'CentricityContext.Flow'  is null.");
        }

        // GET: Flows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Flow == null)
            {
                return NotFound();
            }

            var flow = await _context.Flow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flow == null)
            {
                return NotFound();
            }

            return View(flow);
        }

        // GET: Flows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Flow flow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flow);
        }

        // GET: Flows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Flow == null)
            {
                return NotFound();
            }

            var flow = await _context.Flow.FindAsync(id);
            if (flow == null)
            {
                return NotFound();
            }
            return View(flow);
        }

        // POST: Flows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Flow flow)
        {
            if (id != flow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlowExists(flow.Id))
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
            return View(flow);
        }

        // GET: Flows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Flow == null)
            {
                return NotFound();
            }

            var flow = await _context.Flow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flow == null)
            {
                return NotFound();
            }

            return View(flow);
        }

        // POST: Flows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Flow == null)
            {
                return Problem("Entity set 'CentricityContext.Flow'  is null.");
            }
            var flow = await _context.Flow.FindAsync(id);
            if (flow != null)
            {
                _context.Flow.Remove(flow);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlowExists(int id)
        {
          return (_context.Flow?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

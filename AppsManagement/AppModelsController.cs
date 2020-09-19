using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppsManagement.Data;
using AppsManagement.Models;

namespace AppsManagement
{
    public class AppModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AppModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Apps.ToListAsync());
        }

        // GET: AppModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appModel = await _context.Apps
                .FirstOrDefaultAsync(m => m.AppId == id);
            if (appModel == null)
            {
                return NotFound();
            }

            return View(appModel);
        }

        // GET: AppModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppId,Name,OrganizationId,ChromeWebOrigin")] AppModel appModel)
        {
            if (ModelState.IsValid)
            {


                _context.Add(appModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appModel);
        }

        // GET: AppModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appModel = await _context.Apps.FindAsync(id);
            if (appModel == null)
            {
                return NotFound();
            }
            return View(appModel);
        }

        // POST: AppModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AppId,Name,OrganizationId,ChromeWebOrigin")] AppModel appModel)
        {
            if (id != appModel.AppId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppModelExists(appModel.AppId))
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
            return View(appModel);
        }

        // GET: AppModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appModel = await _context.Apps
                .FirstOrDefaultAsync(m => m.AppId == id);
            if (appModel == null)
            {
                return NotFound();
            }

            return View(appModel);
        }

        // POST: AppModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var appModel = await _context.Apps.FindAsync(id);
            _context.Apps.Remove(appModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppModelExists(string id)
        {
            return _context.Apps.Any(e => e.AppId == id);
        }
    }
}

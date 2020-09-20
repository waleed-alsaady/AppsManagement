using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppsManagement.Data;
using AppsManagement.Models;
using Microsoft.AspNetCore.Authorization;
using AppsManagement.Services;

namespace AppsManagement
{
    [Authorize]
    public class AppModelsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IOneSignalApiService oneSignalApiService;

        public AppModelsController(ApplicationDbContext context, IOneSignalApiService oneSignalApiService)
        {
            this.context = context;
            this.oneSignalApiService = oneSignalApiService;
        }

        // GET: AppModels
        [Authorize(Roles = "apps.admin, apps.data-entry-operator")]
        public async Task<IActionResult> Index()
        {
            var apiResults = await oneSignalApiService.GetAllAsync();
            return View(apiResults);
            // results can be stored from local db or api
            //return View(await context.Apps.ToListAsync());
        }

        // GET: AppModels/Details/5
        [Authorize(Roles = "apps.admin, apps.data-entry-operator")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // app can be retrieve locally or from api
            var appModel = await oneSignalApiService.GetAsync(id);

            //var appModel = await context.Apps
            //    .FirstOrDefaultAsync(m => m.AppId == id);
            if (appModel == null)
            {
                return NotFound();
            }

            return View(appModel);
        }

        // GET: AppModels/Create
        [Authorize(Roles = "apps.admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "apps.admin")]
        public async Task<IActionResult> Create([Bind("Name,OrganizationId,ChromeWebOrigin")] AppModel appModel)
        {
            if (ModelState.IsValid)
            {
                var app = await oneSignalApiService.CreateApp(appModel);

                context.Add(app);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appModel);
        }

        // GET: AppModels/Edit/5
        [Authorize(Roles = "apps.admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appModel = await context.Apps.FindAsync(id);
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
        [Authorize(Roles = "apps.admin")]
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
                    var app = await oneSignalApiService.UpdateApp(appModel);

                    context.Update(app);
                    await context.SaveChangesAsync();
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



        private bool AppModelExists(string id)
        {
            return context.Apps.Any(e => e.AppId == id);
        }
    }
}

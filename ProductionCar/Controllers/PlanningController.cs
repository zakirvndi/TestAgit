using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductionCar.Data;
using ProductionCar.Models;
using ProductionCar.Services;

namespace ProductionCar.Controllers
{
    public class PlanningController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PlanningService _planningService;

        public PlanningController(AppDbContext context, PlanningService planningService)
        {
            _context = context;
            _planningService = planningService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Calculate(int[] dailyInput)
        {
            if (dailyInput == null || dailyInput.Length != 7)
            {
                ModelState.AddModelError("", "Please fill all 7 days.");
                return View("Index");
            }

            var details = _planningService.Distribute(dailyInput.ToList());
            var header = _planningService.BuildHeader(details);

            // Save planning header
            _context.PlanningHeaders.Add(header);
            await _context.SaveChangesAsync();

            foreach (var detail in details)
                detail.PlanningID = header.PlanningID;

            // Save planning details
            _context.PlanningDetails.AddRange(details);
            await _context.SaveChangesAsync();

            ViewBag.Header = header;
            ViewBag.Details = details;

            return View("Result");
        }

        public async Task<IActionResult> History()
        {
            var history = await _context.PlanningHeaders
                .Include(h => h.Details.OrderBy(d => d.DayOrder))
                .OrderByDescending(h => h.PlanningID)
                .ToListAsync();

            return View(history);
        }
    }
}
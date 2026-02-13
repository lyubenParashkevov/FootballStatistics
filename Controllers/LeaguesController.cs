using FootballStatistics.Core.Contracts;
using FootballStatistics.ViewModels.League;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballStatistics.Controllers
{
    [Authorize]
    public class LeaguesController : Controller
    {
        private readonly ILeagueService leagueService;

        public LeaguesController(ILeagueService leagueService)
        {
            this.leagueService = leagueService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var leagues = await leagueService.GetAllAsync();
            return View(leagues);
        }
   
        [HttpGet]
        public IActionResult Create()
        {
            return View(new LeagueFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeagueFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await leagueService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}

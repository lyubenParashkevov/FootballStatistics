using FootballStatistics.Core.Contracts;
using FootballStatistics.Infrastructure.Data;
using FootballStatistics.Infrastructure.Models;
using FootballStatistics.ViewModels.Match;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FootballStatistics.Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly FootballStatisticsDbContext dbContext;

        public MatchService(FootballStatisticsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<MatchListItemModel>> GetAllAsync()
        {
            return await dbContext.Matches
                .AsNoTracking()
                .OrderByDescending(m => m.MatchDate)
                .Select(m => new MatchListItemModel
                {
                    Id = m.Id,
                    HomeTeamName = m.HomeTeam.Name,
                    AwayTeamName = m.AwayTeam.Name,
                    HomeGoals = m.HomeGoals,
                    AwayGoals = m.AwayGoals,
                    MatchDate = m.MatchDate
                })
                .ToListAsync();
        }

        public async Task<MatchFormModel> GetCreateModelAsync()
        {
            return new MatchFormModel
            {
                MatchDate = DateTime.Today,
                Teams = await GetTeamsAsync()
            };
        }

        public async Task CreateAsync(MatchFormModel model)
        {
            if (model.HomeTeamId == null || model.AwayTeamId == null)
            {
                throw new InvalidOperationException("Both teams are required.");
            }

            if (model.HomeTeamId.Value == model.AwayTeamId.Value)
            {
                throw new InvalidOperationException("Home team and away team cannot be the same.");
            }

            bool homeExists = await dbContext.Teams.AnyAsync(t => t.Id == model.HomeTeamId.Value);
            bool awayExists = await dbContext.Teams.AnyAsync(t => t.Id == model.AwayTeamId.Value);

            if (!homeExists || !awayExists)
            {
                throw new InvalidOperationException("Selected team does not exist.");
            }

            var match = new Match
            {
                HomeTeamId = model.HomeTeamId.Value,
                AwayTeamId = model.AwayTeamId.Value,
                HomeGoals = model.HomeGoals,
                AwayGoals = model.AwayGoals,
                MatchDate = model.MatchDate
            };

            await dbContext.Matches.AddAsync(match);
            await dbContext.SaveChangesAsync();
        }

        private async Task<IEnumerable<SelectListItem>> GetTeamsAsync()
        {
            return await dbContext.Teams
                .AsNoTracking()
                .OrderBy(t => t.Name)
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                })
                .ToListAsync();
        }
    }
}

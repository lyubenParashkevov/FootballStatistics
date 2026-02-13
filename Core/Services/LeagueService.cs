using FootballStatistics.Core.Contracts;
using FootballStatistics.Infrastructure.Data;
using FootballStatistics.Infrastructure.Models;
using FootballStatistics.ViewModels.League;
using Microsoft.EntityFrameworkCore;

namespace FootballStatistics.Core.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly FootballStatisticsDbContext dbContext;

        public LeagueService(FootballStatisticsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<LeagueListItemModel>> GetAllAsync()
        {
            return await dbContext.Leagues
                .AsNoTracking()
                .OrderBy(l => l.Name)
                .Select(l => new LeagueListItemModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Country = l.Country
                })
                .ToListAsync();
        }

        public async Task CreateAsync(LeagueFormModel model)
        {
            var league = new League
            {
                Name = model.Name,
                Country = model.Country
            };

            await dbContext.Leagues.AddAsync(league);
            await dbContext.SaveChangesAsync();
        }
    }
}

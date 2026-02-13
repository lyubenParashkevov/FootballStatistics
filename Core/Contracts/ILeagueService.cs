using FootballStatistics.ViewModels.League;

namespace FootballStatistics.Core.Contracts
{
    public interface ILeagueService
    {
        Task<IEnumerable<LeagueListItemModel>> GetAllAsync();
        Task CreateAsync(LeagueFormModel model);
    }
}

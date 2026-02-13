using FootballStatistics.ViewModels.Team;

namespace FootballStatistics.Core.Contracts
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamListItemModel>> GetAllAsync();

        Task<TeamFormModel> GetCreateModelAsync();

        Task<TeamFormModel?> GetEditModelAsync(int id);

        Task CreateAsync(TeamFormModel model);

        Task<bool> UpdateAsync(int id, TeamFormModel model);

        Task<bool> DeleteAsync(int id);

        
    }
}

using FootballStatistics.ViewModels.League;
using Humanizer;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using static FootballStatistics.Core.Common.ValidationConstants;

namespace FootballStatistics.ViewModels.Team
{
    public class TeamFormModel
    {
        [Required]
        [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength)]
        public string Name { get; set; } = null!;

        [Range(TeamGoalsMinValue, TeamGoalsMaxValue )]
        public int GoalsScored { get; set; }


        [Range(TeamGoalsMinValue, TeamGoalsMaxValue)] 
        public int GoalsConceded { get; set; }

        [Required]
        public int? LeagueId { get; set; }

        public IEnumerable<LeagueDropdownModel> Leagues { get; set; } = new List<LeagueDropdownModel>();
    }
}

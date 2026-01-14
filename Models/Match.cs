using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballStatistics.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(HomeTeam))]
        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; } = null!;

        [ForeignKey(nameof(AwayTeam))]
        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; } = null!;

        [Range(0, 20)]
        public int HomeGoals { get; set; }

        [Range(0, 20)]
        public int AwayGoals { get; set; }

        public DateTime MatchDate { get; set; }
    }
}

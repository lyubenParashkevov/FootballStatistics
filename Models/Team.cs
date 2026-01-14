using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballStatistics.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Range(0, 150)]
        public int Points { get; set; }

        [Range(0, 1000)]
        public int GoalsScored { get; set; }

        [Range(0, 1000)]
        public int GoalsConceded { get; set; }

        // FK
        [ForeignKey(nameof(League))]
        public int LeagueId { get; set; }

        public League League { get; set; } = null!;

        // Navigation
        public ICollection<Match> HomeMatches { get; set; } = new HashSet<Match>();
        public ICollection<Match> AwayMatches { get; set; } = new HashSet<Match>();
    }
}

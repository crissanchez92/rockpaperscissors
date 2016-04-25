using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rpsapi.Models
{
    public class Competitor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompetitorId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
    }
}
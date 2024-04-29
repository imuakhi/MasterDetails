using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDetails.Models
{
    public class Experiance
    {
        public int Id { get; set; }
        public string SkillName { get; set; }
        public string SkillLevel { get; set; }
        public int SkillYear { get; set; }
        [ForeignKey("Candidate")]
        public int CanId { get; set; }
        public Candidate Candidate { get; set; }

    }
}

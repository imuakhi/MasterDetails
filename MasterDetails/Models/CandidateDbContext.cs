using Microsoft.EntityFrameworkCore;

namespace MasterDetails.Models
{
    public class CandidateDbContext :DbContext
    {
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options):base(options) { }
        public DbSet<Candidate> candidates { get; set;}
        public DbSet<Experiance> experiances { get; set;}
    }
}

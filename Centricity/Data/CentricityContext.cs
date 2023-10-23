using Centricity.Models;
using Microsoft.EntityFrameworkCore;

namespace Centricity.Data
{
    public class CentricityContext : DbContext
    {
        public CentricityContext (DbContextOptions<CentricityContext> options)
            : base(options)
        {            
        }        

        public DbSet<Flow> Flow { get; set; } = default!;
        public DbSet<Step> Step { get; set; } = default!;
        public DbSet<Evidence> Evidence { get; set; } = default!;
        public DbSet<Transition> Transition { get; set; } = default!;
        public DbSet<Job> Job { get; set; } = default!;
        public DbSet<JobStep> JobStep { get; set; } = default!;
        public DbSet<JobStepEvidence> JobStepEvidence { get; set; } = default!;
        public DbSet<JobStepTransition> JobStepTransition { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<JobStep>().HasMany<JobStepTransition>().WithOne(t => t.JobStepTo);
            modelBuilder.Entity<Step>().HasMany<Transition>().WithOne(t => t.StepTo);

            base.OnModelCreating(modelBuilder);
        }

    }    
}

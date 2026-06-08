using Microsoft.EntityFrameworkCore;
using ProductionCar.Models;

namespace ProductionCar.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<PlanningHeader> PlanningHeaders { get; set; }
        public DbSet<PlanningDetail> PlanningDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlanningDetail>()
                .HasOne(d => d.Header)
                .WithMany(h => h.Details)
                .HasForeignKey(d => d.PlanningID)
                .HasConstraintName("FK_PlanningDetails_PlanningHeaders");
        }
    }
}
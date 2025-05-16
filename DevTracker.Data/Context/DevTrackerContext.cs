using DevTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.Data.Context;

public class DevTrackerContext : DbContext
{
    public DevTrackerContext(DbContextOptions<DevTrackerContext> options) : base(options)
    {
    }

    public DbSet<Goal> Goals { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Session> Sessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new GoalConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new SessionConfiguration());
    }
}
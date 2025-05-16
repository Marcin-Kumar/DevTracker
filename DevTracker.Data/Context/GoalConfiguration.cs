using DevTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTracker.Data.Context;
internal class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.CurrentStatus).HasConversion<string>().HasMaxLength(20);
        builder.Property(g => g.Title).HasMaxLength(100);
        builder.Property(g => g.Description).HasMaxLength(300);

        builder.HasMany(g => g.Projects)
            .WithOne(p => p.Goal)
            .HasForeignKey(p => p.GoalId)
            .IsRequired(false);
        builder.HasMany(g => g.CodingSessions)
            .WithOne(s => s.GoalCodingSession)
            .HasForeignKey(s => s.GoalCodingSessionId)
            .IsRequired(false);
        builder.HasMany(g => g.TheorySessions)
            .WithOne(s => s.GoalTheorySession)
            .HasForeignKey(s => s.GoalTheorySessionId)
            .IsRequired(false);
    }
}

using DevTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTracker.Data.Context;
internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.CurrentStatus).HasConversion<string>();
        builder.HasOne(p => p.Goal)
            .WithMany(g => g.Projects)
            .HasForeignKey(p => p.GoalId)
            .IsRequired(false);
        builder.HasMany(p => p.CodingSessions)
            .WithOne(s => s.ProjectCodingSession)
            .HasForeignKey(s => s.ProjectCodingSessionId)
            .IsRequired(false);
        builder.HasMany(p => p.TheorySessions)
            .WithOne(s => s.ProjectTheorySession)
            .HasForeignKey(s => s.ProjectTheorySessionId)
            .IsRequired(false);
    }
}

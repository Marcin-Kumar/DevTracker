using DevTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTracker.Data.Context;
internal class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(s => s.Id);
        builder.HasOne(s => s.GoalCodingSession)
            .WithMany(g => g.CodingSessions)
            .HasForeignKey(s => s.GoalCodingSessionId)
            .IsRequired(false);
        builder.HasOne(s => s.GoalTheorySession)
            .WithMany(g => g.TheorySessions)
            .HasForeignKey(s => s.GoalTheorySessionId)
            .IsRequired(false);
        builder.HasOne(s => s.ProjectCodingSession)
            .WithMany(p => p.CodingSessions)
            .HasForeignKey(s => s.ProjectCodingSessionId)
            .IsRequired(false);
        builder.HasOne(s => s.ProjectTheorySession)
            .WithMany(p => p.TheorySessions)
            .HasForeignKey(s => s.ProjectTheorySessionId)
            .IsRequired(false);
    }
}
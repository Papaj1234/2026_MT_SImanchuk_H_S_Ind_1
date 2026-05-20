using CodingTask.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects => this.Set<Project>();

    public DbSet<StageType> StageTypes => this.Set<StageType>();

    public DbSet<BuildStatus> BuildStatuses => this.Set<BuildStatus>();

    public DbSet<IssueSeverity> IssueSeverities => this.Set<IssueSeverity>();

    public DbSet<BuildStage> BuildStages => this.Set<BuildStage>();

    public DbSet<IssueLog> IssueLogs => this.Set<IssueLog>();

    public DbSet<HostConfiguration> HostConfigurations => this.Set<HostConfiguration>();

    public DbSet<ThreadSpeedMetric> ThreadSpeedMetrics => this.Set<ThreadSpeedMetric>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasIndex(p => p.FolderPath)
            .IsUnique();

        modelBuilder.Entity<StageType>()
            .HasIndex(s => s.Name)
            .IsUnique();

        modelBuilder.Entity<BuildStatus>()
            .HasIndex(s => s.Name)
            .IsUnique();

        modelBuilder.Entity<IssueSeverity>()
            .HasIndex(s => s.Name)
            .IsUnique();

        modelBuilder.Entity<HostConfiguration>()
            .HasIndex(h => new
            {
                h.CpuModel,
                h.PhysicalCoreCount,
                h.LogicalThreadCount,
                h.RamGb,
                h.OperatingSystem,
            })
            .IsUnique();

        modelBuilder.Entity<BuildStage>()
            .HasIndex(x => new { x.ProjectId, x.StageTypeId, x.StartedAt })
            .IsUnique();
    }
}
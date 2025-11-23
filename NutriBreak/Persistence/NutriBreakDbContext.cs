using Microsoft.EntityFrameworkCore;
using NutriBreak.Domain;

namespace NutriBreak.Persistence;

public class NutriBreakDbContext : DbContext
{
    public NutriBreakDbContext(DbContextOptions<NutriBreakDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet<BreakRecord> BreakRecords => Set<BreakRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("USERS");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("ID").ValueGeneratedNever().HasPrecision(18, 2);
            e.Property(x => x.Name).HasColumnName("NAME").IsRequired().HasMaxLength(100);
            e.Property(x => x.Email).HasColumnName("EMAIL").IsRequired().HasMaxLength(200);
            e.Property(x => x.WorkMode).HasColumnName("WORKMODE").HasMaxLength(50);
            e.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<Meal>(e =>
        {
            e.ToTable("MEALS");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("ID").ValueGeneratedNever().HasPrecision(18, 2);
            e.Property(x => x.UserId).HasColumnName("USERID").HasPrecision(18, 2);
            e.Property(x => x.Title).HasColumnName("TITLE").IsRequired().HasMaxLength(120);
            e.Property(x => x.Calories).HasColumnName("CALORIES").IsRequired();
            e.Property(x => x.TimeOfDay).HasColumnName("TIMEOFDAY").HasMaxLength(50);
            e.HasOne(x => x.User)
                .WithMany(u => u.Meals)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasIndex(x => x.UserId).HasDatabaseName("IX_MEALS_USERID");
        });

        modelBuilder.Entity<BreakRecord>(e =>
        {
            e.ToTable("BREAK_RECORDS");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("ID").ValueGeneratedNever().HasPrecision(18, 2);
            e.Property(x => x.UserId).HasColumnName("USERID").HasPrecision(18, 2);
            e.Property(x => x.StartedAt).HasColumnName("STARTEDAT");
            e.Property(x => x.DurationMinutes).HasColumnName("DURATIONMINUTES");
            e.Property(x => x.Type).HasColumnName("TYPE").IsRequired().HasMaxLength(50);
            e.Property(x => x.Mood).HasColumnName("MOOD").HasMaxLength(50);
            e.Property(x => x.EnergyLevel).HasColumnName("ENERGYLEVEL");
            e.Property(x => x.ScreenTimeMinutes).HasColumnName("SCREENTIMEMINUTES");
            e.HasOne(x => x.User)
                .WithMany(u => u.Breaks)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasIndex(x => x.UserId).HasDatabaseName("IX_BREAKS_USERID");
        });
    }
}

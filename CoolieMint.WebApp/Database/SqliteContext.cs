using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebControlCenter.Database.Entities;

namespace WebControlCenter.Database
{
    public class SqliteContext : DbContext , ISqLiteContext
    {
        public DbSet<Controller> Controller { get; set; }
        public DbSet<ControllerStatusSegment> ControllerStatusSegment { get; set; }
        public DbSet<ControllerStateInformation> ControllerStateInformation { get; set; }
        public DbSet<ControllerStateHistory> ControllerStateHistory { get; set; }
        public DbSet<VersionHistory> VersionHistory { get; set; }

        EntityEntry ISqLiteContext.Add(object entity)
        {
            return Add(entity);
        }

        int ISqLiteContext.SaveChanges()
        {
            return SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=TestDatabase.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<ControllerStatusSegment>().ToTable("ControllerStatusSegments");
            modelBuilder.Entity<ControllerStatusSegment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstOnline).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Controller>().ToTable("Controllers");
            modelBuilder.Entity<Controller>(entity => { entity.HasKey(e => e.Id); });

            modelBuilder.Entity<ControllerStatusSegment>().ToTable("ControllerStatusSegments");
            modelBuilder.Entity<ControllerStatusSegment>(entity => { entity.HasKey(e => e.Id); });

            modelBuilder.Entity<ControllerStateInformation>().ToTable("ControllerStateInformations");
            modelBuilder.Entity<ControllerStateInformation>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ControllerStateHistory>().ToTable("ControllerStateHistories");
            modelBuilder.Entity<ControllerStateHistory>(entity => { entity.HasKey(e => e.Id); });

            
            base.OnModelCreating(modelBuilder);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Collections.Specialized;

namespace WebApi3._0
{
    public class DBContext : DbContext
    {
        public DbSet<Complaint> Complaints { get; set; } = null!;
        public DbSet<Resource> Resources { get; set; } = null!;
        public DbSet<Street> Streets { get; set; } = null;
        public DbSet<District> Districts { get; set; } = null!;
        
        public DBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConfigurationManager.AppSettings.Get("SqlServer"));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Complaint>().HasOne(t => t.Resource).WithMany().HasForeignKey(t => t.Resource_ID);
            modelBuilder.Entity<Complaint>().HasOne(t => t.Street).WithMany().HasForeignKey(t => t.Street_ID);
            modelBuilder.Entity<Complaint>().HasKey(t => t.Complaint_ID);
            modelBuilder.Entity<Street>().HasOne(t => t.District).WithMany().HasForeignKey(t => t.District_ID);
            modelBuilder.Entity<Street>().HasKey(t => t.Street_ID);
            modelBuilder.Entity<Resource>().HasKey(t => t.Resource_ID);
            modelBuilder.Entity<District>().HasKey(t => t.District_ID);
            base.OnModelCreating(modelBuilder);
        }
    }

}
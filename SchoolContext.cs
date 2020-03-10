using Microsoft.EntityFrameworkCore;
using SchoolWebServiceWebAPO.Model;

namespace SchoolWebServiceWebAPO
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<ClassScore> ClassScore { get; set; }
        public DbSet<Classes> Classes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Grade>()
            //    .HasMany(e => e.Student)
            //    .WithRequired()
            //    .HasForeignKey(e => e.Gender);
        }
        public DbSet<SchoolWebServiceWebAPO.Model.Student> Student { get; set; }
    }
}

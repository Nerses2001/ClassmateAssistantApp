using Entity;
using Entity.EntityConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {
            this.ApplicationUser =  Set<ApplicationUser>();
            this.Courses = Set<Cource>();
            this.UserCourses = Set<UserCourse>();
        
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<Cource> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new  UserCourseConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());

        }




    }
}
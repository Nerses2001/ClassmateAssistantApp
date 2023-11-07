using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Entity.EntityConfiguration
{
    public class UserCourseConfiguration : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.HasKey(uc => new { uc.UserId, uc.CourseId });

            builder.HasOne(uc => uc.User)
                     .WithMany(u => u.UserCourses)
                     .HasForeignKey(uc => uc.UserId)
                     .IsRequired();

        }
    }
}
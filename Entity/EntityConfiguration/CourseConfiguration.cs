﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.EntityConfiguration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Cource>
    {
        public void Configure(EntityTypeBuilder<Cource> builder)
        {

            // Configure primary key
            builder.HasKey(c => c.Id);

            // Configure properties
            builder.Property(c => c.CourseName).IsRequired().HasMaxLength(50);

            // Configure relationships
            builder.HasMany(c => c.UserCourses)
                .WithOne(uc => uc.Course)
                .HasForeignKey(uc => uc.CourseId)
                .IsRequired();
        }
    }
}
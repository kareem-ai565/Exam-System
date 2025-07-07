using Exam_System.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Database.Configs
{
    public class ExamConfig : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(100);



            builder.HasMany(e => e.Questions)
            .WithOne(q => q.Exam);



            builder.HasMany(e => e.UserExamResults)
                .WithOne(uer => uer.Exam)
                ;

            builder.HasOne(u => u.User)
                .WithMany(e => e.CreatedExams)
                .HasForeignKey(u => u.UserId)
                .IsRequired(false);
                

            builder.HasData(
                    new Exam
                    {
                        Id = 1,
                        Title = "C# Fundamentals",
                        Description = "Basic C# exam for beginners.",

                        
                    },
                    new Exam
                    {
                        Id = 2,
                        Title = "ASP.NET Core",
                        Description = "Intermediate ASP.NET Core exam.",

                    }
                );

        }
    }
}

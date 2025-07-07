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

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.HasMany(e => e.Questions)
            .WithOne(q => q.Exam)
            .HasForeignKey(q => q.ExamId)
            .IsRequired();

            builder.HasMany(e => e.UserExamResults)
                .WithOne(uer => uer.Exam)
                .HasForeignKey(uer => uer.ExamId);

            builder.HasOne(u => u.User)
                .WithMany(e => e.CreatedExams)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

        }
    }
}

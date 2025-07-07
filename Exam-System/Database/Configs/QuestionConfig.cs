using Exam_System.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Database.Configs
{
    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(q => q.Exam)
              .WithMany(e => e.Questions)
              .HasForeignKey(q => q.ExamId);

            builder.Property(q => q.QuestionText)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasMany(q => q.Choises)
                .WithOne(c => c.Question)
                .HasForeignKey(c => c.QuestionId);
                
        }
    }
 
}

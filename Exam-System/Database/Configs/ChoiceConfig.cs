using Exam_System.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Database.Configs
{
    public class ChoiceConfig : IEntityTypeConfiguration<Choice>
    {
        public void Configure(EntityTypeBuilder<Choice> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ChoiseText)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.IsCorrect)
                .IsRequired();


            builder.HasOne(c => c.Question)
                .WithMany(q => q.Choises)
                .HasForeignKey(c => c.QuestionId)
                ;
        }
    }

}

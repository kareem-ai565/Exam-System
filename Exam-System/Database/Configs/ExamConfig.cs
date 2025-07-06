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

        }
    }
}

using Exam_System.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam_System.Database.Configs
{
    public class UserExamResultConfig : IEntityTypeConfiguration<UserExamResult>
    {
        public void Configure(EntityTypeBuilder<UserExamResult> builder)
        {
            builder.HasKey(uer => uer.Id);

            builder.Property(uer => uer.Score)
              .IsRequired();
            

            builder.HasOne(uer => uer.Exam)
                .WithMany(e => e.UserExamResults)
                .HasForeignKey(uer => uer.ExamId)
                .IsRequired();


            builder.HasOne(uer => uer.User)
                .WithMany(u => u.UserExamResults)
                .HasForeignKey(uer => uer.UserId)
                .IsRequired();

        }
    }

}

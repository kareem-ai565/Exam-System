
using Exam_System.Database.Context;
using Exam_System.Repositories;
using Exam_System.Repositories.Interfaces;
using Exam_System.Services;
using Exam_System.Services.Interfaces;
using Exam_System.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Exam_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ExamSysContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddScoped<IExamService,ExamService>();
            builder.Services.AddScoped<IUnitOfWork, Exam_System.UnitOfWork.UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

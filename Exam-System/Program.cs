
using Exam_System.Database.Context;
using Exam_System.Repositories;
using Exam_System.Repositories.Interfaces;
using Exam_System.Services;
using Exam_System.Services.Implementations;
using Exam_System.Services.Interfaces;
using Exam_System.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Exam_System.Database.Models;
using Microsoft.AspNetCore.Identity;

namespace Exam_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ExamSysContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddIdentity<User, IdentityRole<Guid>>().AddEntityFrameworkStores<ExamSysContext>()
                .AddDefaultTokenProviders();
            
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };

            });

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();




            builder.Services.AddScoped<IExamService,ExamService>();

            builder.Services.AddScoped<IQuestionService,QuestionService>();

            builder.Services.AddScoped<IUnitOfWork, Exam_System.UnitOfWork.UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

            builder.Services.AddScoped<IUserExamResultService, UserExamResultService>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDev", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
                var app = builder.Build();
            app.UseCors("AllowAngularDev");




          


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

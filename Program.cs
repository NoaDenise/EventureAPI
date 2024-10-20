
using EventureAPI.Data;
using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Data.Repositories;
using EventureAPI.Models;
using EventureAPI.Services.IServices;
using EventureAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EventureAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<EventureContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationContext"));
            });


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthorization();
            builder.Services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<EventureContext>()
                .AddApiEndpoints();

            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapIdentityApi<User>();

            app.MapControllers();

            app.Run();
        }
    }
}

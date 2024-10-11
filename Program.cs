
using EventureAPI.Data;
using EventureAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
            builder.Services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<EventureContext>()
                .AddApiEndpoints();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapIdentityApi<User>();

            app.MapControllers();

            app.Run();
        }
    }
}

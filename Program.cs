using EventureAPI.Data;
using EventureAPI.Data.Repositories;
using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Models;
using EventureAPI.Services;
using EventureAPI.Services.IServices;
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

            builder.Services.AddIdentityCore<User>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    //options.Password.RequiredLength = 8;
                })
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<EventureContext>()
              .AddDefaultTokenProviders();

            // Register RoleManager and UserManager
            builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddScoped<UserManager<User>>();




            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Adding scope for repo and services
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<ICommentService, CommentService>();

            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            builder.Services.AddScoped<IAttendanceService, AttendanceService>();

            builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
            builder.Services.AddScoped<IActivityService, ActivityService>();

            builder.Services.AddAuthorization();
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<EventureContext>()
                .AddDefaultTokenProviders();

            // L�gger till repository och service f�r rating
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();
            builder.Services.AddScoped<IRatingService, RatingService>();

            builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
            builder.Services.AddScoped<IActivityService, ActivityService>();


            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            var app = builder.Build();

            // Create roles 
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    CreateRolesAsync(services).Wait();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while creating roles.");
                }
            }

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
        // Create roles 

        private static async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();


            string[] roleNames = { "admin", "user" };
            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Optionally, create a default admin user
            var adminEmail = "admin@example.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new User { UserName = "admin", Email = adminEmail };
                var result = await userManager.CreateAsync(newAdmin, "Admin@1234"); 
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "admin");
                }
            }
        }
    }
}

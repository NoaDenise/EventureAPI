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
using Microsoft.OpenApi.Models;
using System.Text;

namespace EventureAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Configure DbContext
            builder.Services.AddDbContext<EventureContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationContext"));
            });


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // Configure Authentication (JWT)
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

            // Configure Identity (User and Roles)
            //builder.Services.AddIdentityCore<User>(options =>
            //    {
            //        options.User.RequireUniqueEmail = true;
            //        //options.Password.RequiredLength = 8;
            //    })
            //  .AddRoles<IdentityRole>()
            //  .AddEntityFrameworkStores<EventureContext>()
            //  .AddDefaultTokenProviders();




            //Configure Identity(User and Roles)
            builder.Services.AddIdentity<User, IdentityRole>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;
                    //options.Password.RequiredLength = 8; // Password requirements can be added here
                })
                .AddEntityFrameworkStores<EventureContext>()
                .AddDefaultTokenProviders();  // Includes password reset tokens, etc.

            // Register RoleManager and UserManager (though this is typically unnecessary with AddIdentity)
            builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddScoped<UserManager<User>>();


            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();

            // Swagger configuration to define JWT authentication for the API
            builder.Services.AddSwaggerGen(c =>
            {
                // Define the security scheme for JWT in Swagger UI
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,  // The JWT token will be passed in the header
                    Name = "Authorization", // The name of the header
                    Type = SecuritySchemeType.ApiKey, // Type of security (API key used for JWT)
                    Scheme = "Bearer", // The authentication scheme used (Bearer Token)
                    BearerFormat = "JWT", // Specify that the format is JWT
                    Description = "Please enter JWT with Bearer into field" // Description for the user in Swagger UI 
                });

                // Define that the API requires a Bearer token for authentication
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,  // Referencing the defined security scheme "Bearer"
                                Id = "Bearer"
                            }
                        },
                        new string[] { }  // No specific scopes required
                    }
                });
            });

            //Adding scope for repo and services
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<ICommentService, CommentService>();

            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            builder.Services.AddScoped<IAttendanceService, AttendanceService>();

            builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
            builder.Services.AddScoped<IActivityService, ActivityService>();
           
     
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();
            builder.Services.AddScoped<IRatingService, RatingService>();

            builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
            builder.Services.AddScoped<IActivityService, ActivityService>();

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


            app.MapControllers();

            app.Run();
        }

    }
}

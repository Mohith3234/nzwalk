
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWALKS.Automapper;
using NZWALKS.Data;
using NZWALKS.Repository;
using System.Text;
namespace NZWALKS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<DbcontextClass>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZDBcontext")));
            //add authorization services to the application
            builder.Services.AddDbContext<DbContextAutoClass>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZDBAutocontext")));

            builder.Services.AddScoped<RepositoryPattern, SqlRegionRepository>();
            builder.Services.AddScoped<IWalkREpository,SQLWalkRepository>();

            builder.Services.AddScoped(typeof(AutomapperProficeClass));

            //setting up identity
            builder.Services.AddIdentityCore<IdentityUser>()
                 .AddRoles<IdentityRole>()
                 .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWALKS")
                 .AddEntityFrameworkStores<DbContextAutoClass>()
                 .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric= false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

            });
            
            //add services of authentication
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
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using TicketBookingApp.Data;
using TicketBookingApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Load connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container
builder.Services.AddScoped(_ => new UserRepository(connectionString));
builder.Services.AddScoped(_ => new AdventureRepository(connectionString));
builder.Services.AddScoped(_ => new BookingRepository(connectionString));
builder.Services.AddScoped(_ => new PhotoRepository(connectionString));
builder.Services.AddScoped(_=> new GuideRepository(connectionString));

// Register JwtService
builder.Services.AddSingleton<JwtService>(new JwtService(builder.Configuration["Jwt:SecretKey"]));

// Add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });

builder.Services.AddControllers();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();

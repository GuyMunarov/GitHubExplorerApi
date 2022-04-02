using Core.Entities;
using Core.Interfaces;
using GitHubExplorerApi.Data;
using GitHubExplorerApi.Helpers;
using GitHubExplorerApi.Repositories;
using GitHubExplorerApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


var config = builder.Configuration;
// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(config.GetConnectionString("DefaultConnection"))
    .EnableSensitiveDataLogging().UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
        ValidIssuer = config["Token:Issuer"],
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IHashingService, HashingService>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton(typeof(IExcelService<>), typeof(ExcelService<>));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseMiddleware<ErrorHandlingMiddlewear>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run("http://localhost:5178");

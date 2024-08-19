using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BusinessLogic;
using DataAccounts;
using DataAccounts.Repositories;
using BusinessLogic.Model;
using AuthenticationAndAuthorization;
using AuthenticationAndAuthorization.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiAuthencation(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<RoleProvider>(); 
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DataBase")));

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleProvider = scope.ServiceProvider.GetRequiredService<RoleProvider>();
    await roleProvider.AddRoles();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapPost("sign-up", ([FromServices] IUserService userService, [FromBody] RegisterUserRequest usermodel)
    => userService.RegisterUserAsync(usermodel));

app.MapPost("sign-in", ([FromServices] IUserService userService, [FromBody] LoginUserRequest usermodel)
    => userService.LoginUserAsync(usermodel));

app.Run();
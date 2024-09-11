using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using DataAccounts;
using DataAccounts.Repositories;
using BusinessLogic.Model;
using AuthenticationAndAuthorization.Extensions;
using AuthenticationAndAuthorization;
using DataAccounts.Repositories.GenreRopository;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Model.MovieModel;
using DataAccounts.Repositories.MovieRepositories;
using DataAccounts.Repositories.GenreRepositories;
using BusinessLogic.Model.GenreModel;
using AuthenticationAndAuthorization.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiAuthencation(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<RoleProvider>(); 
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DataBase")));

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

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

app.MapUserEndpoints();
app.MapMovieEndpoints();
app.MapGenreEndpoints();

app.Run();
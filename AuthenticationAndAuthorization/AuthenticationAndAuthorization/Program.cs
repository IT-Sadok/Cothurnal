using AuthenticationAndAuthorization.Endpoints;
using AuthenticationAndAuthorization.Extensions;
using AuthenticationAndAuthorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

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

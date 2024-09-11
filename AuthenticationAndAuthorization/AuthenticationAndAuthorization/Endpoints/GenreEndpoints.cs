using AuthenticationAndAuthorization.Policy;
using BusinessLogic.Interfaces;
using BusinessLogic.Model.GenreModel;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Endpoints
{
    public static class GenreEndpoints
    {
        public static void MapGenreEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("create/genre", ([FromServices] IGenreService genreService, [FromBody] CreateGenreModel genre)
                => genreService.CreateGenre(genre)).RequireAuthorization(Policies.AdminPolicy);

            endpoints.MapPost("delete/genre", ([FromServices] IGenreService genreService, [FromBody] DeleteGenreModel genre)
                => genreService.DeleteGenre(genre)).RequireAuthorization(Policies.AdminPolicy);

            endpoints.MapGet("get/genre", ([FromServices] IGenreService genreService)
                => genreService.GetGenre()).RequireAuthorization(Policies.UserPolicy);
        }
    }

}

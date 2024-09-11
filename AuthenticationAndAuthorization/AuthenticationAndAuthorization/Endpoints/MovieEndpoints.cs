using AuthenticationAndAuthorization.Policy;
using BusinessLogic.Interfaces;
using BusinessLogic.Model.MovieModel;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Endpoints
{
    public static class MovieEndpoints
    {
        public static void MapMovieEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("create/movie", ([FromServices] IMovieService movieService, [FromBody] CreateMovieModel createModel)
                => movieService.CreateMovieAsync(createModel)).RequireAuthorization(Policies.AdminPolicy);

            endpoints.MapPost("update/movie", ([FromServices] IMovieService movieService, [FromBody] UpdateMovieModel updateModel)
                => movieService.UpdateMovieAsync(updateModel)).RequireAuthorization(Policies.AdminPolicy);

            endpoints.MapPost("delete/movie", ([FromServices] IMovieService movieService, [FromBody] DeleteMovieModel DeleteModel)
                => movieService.DeleteMovieAsync(DeleteModel)).RequireAuthorization(Policies.AdminPolicy);
            
            endpoints.MapPost("add/genre/to/movie", ([FromServices] IMovieService movieService, [FromBody] AddGenreModel addGenreModel)
                => movieService.AddGenresAsync(addGenreModel)).RequireAuthorization(Policies.AdminPolicy);

            endpoints.MapPost("get/movies/list", ([FromServices] IMovieService movieService, [FromBody] GetListMovieModel filtrModel)
                => movieService.GetMoviesListAsync(filtrModel)).RequireAuthorization(Policies.UserPolicy);

            endpoints.MapPost("get/movie", ([FromServices] IMovieService movieService, [FromBody] GetMovieModel getModel)
                => movieService.GetMovieAsync(getModel)).RequireAuthorization(Policies.UserPolicy);
        }
    }

}

using Microsoft.Extensions.Logging;
using Movies.Core.Entities;

namespace Movies.Infrastructure.Data;

public  class MovieContextSeed
{
    public static async Task SeedAsync(MovieContext movieContext, ILoggerFactory loggerFactory, int? retry = 0)
    {
        int retryForAvailability = retry.Value;
        try
        {
            await movieContext.Database.EnsureCreatedAsync();

            if (!movieContext.Movies.Any())
            {
                movieContext.Movies.AddRange(GetMovies());
                await movieContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability < 3)
            {
                retryForAvailability++;
                var log = loggerFactory.CreateLogger<MovieContextSeed>();
                log.LogError($"Exception occured while connecting: {ex.Message}");
                await SeedAsync(movieContext, loggerFactory, retryForAvailability);
            }
        }
    }

    private static IEnumerable<Movie> GetMovies()
    {
        return new List<Movie>()
        {
            new Movie {MovieName = "Avatar", DirectorName = "James Cameron", ReleaseYear = "2009"},
            new Movie {MovieName = "Titanic", DirectorName = "James Cameron", ReleaseYear = "1997"},
            new Movie {MovieName = "Die Another Day", DirectorName = "Lee Tamahori", ReleaseYear = "2002"},
            new Movie
            {
                MovieName = "Godzilla",
                DirectorName = "Gareth Edwards",
                ReleaseYear = "2014"
            }
        };
    }

}


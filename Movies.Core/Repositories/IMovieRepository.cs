using Movies.Core.Entities;
using Movies.Core.Repositories.Base;

namespace Movies.Core.Repositories;

public interface IMovieRepository : IRepository<Movie>
{
    //here we can have all custom operations 
    Task<IEnumerable<Movie>> GetMoviesByDirectorName(string directorName);
}
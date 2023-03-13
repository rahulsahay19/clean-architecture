using MediatR;
using Movies.Application.Commands;
using Movies.Application.Mappers;
using Movies.Application.Responses;
using Movies.Core.Entities;
using Movies.Core.Repositories;

namespace Movies.Application.Handlers;

public class CreateMovieCommandHandler: IRequestHandler<CreateMovieCommand, MovieResponse>
{
    private readonly IMovieRepository _movieRepository;

    public CreateMovieCommandHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    public async Task<MovieResponse> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movieEntity = MovieMapper.Mapper.Map<Movie>(request);
        if (movieEntity is null)
        {
            throw new ApplicationException("There is an issue with mapping...");
        }

        var newMovie = await _movieRepository.AddAsync(movieEntity);
        var movieResponse = MovieMapper.Mapper.Map<MovieResponse>(newMovie);
        return movieResponse;
    }
}
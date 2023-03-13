using MediatR;
using Movies.Application.Mappers;
using Movies.Application.Queries;
using Movies.Application.Responses;
using Movies.Core.Repositories;

namespace Movies.Application.Handlers;

public class GetMoviesByDirectorNameHandler:  IRequestHandler<GetMoviesByDirectorNameQuery, IEnumerable<MovieResponse>>
{
    private readonly IMovieRepository _movieRepository;

    public GetMoviesByDirectorNameHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    public async Task<IEnumerable<MovieResponse>> Handle(GetMoviesByDirectorNameQuery request, CancellationToken cancellationToken)
    {
        var movieList = await _movieRepository.GetMoviesByDirectorName(request.DirectorName);
        var movieResponseList = MovieMapper.Mapper.Map<IEnumerable<MovieResponse>>(movieList);
        return movieResponseList;
    }
}
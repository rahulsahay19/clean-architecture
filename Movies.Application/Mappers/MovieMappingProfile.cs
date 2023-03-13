using AutoMapper;
using Movies.Application.Commands;
using Movies.Application.Responses;
using Movies.Core.Entities;

namespace Movies.Application.Mappers;

public class MovieMappingProfile : Profile
{
    public MovieMappingProfile()
    {
        CreateMap<Movie, MovieResponse>().ReverseMap();
        CreateMap<Movie, CreateMovieCommand>().ReverseMap();
    }
}
using AutoMapper;
using MovieAPI.DTOS;
using MovieAPI.Models;

namespace MovieAPI.Profiles
{
    public class MovieProfile: Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDTO>().ReverseMap();
        }
        
    }
}
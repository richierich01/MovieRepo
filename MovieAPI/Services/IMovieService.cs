using MovieAPI.DTOS;
using MovieAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Services
{
    public interface IMovieService
    {
        List<Movie> GetMovies();
        Movie AddMovie(Movie movie);
        List<Stat> GetStats();
        List<MovieStat> GetMovieStats();
    }
}
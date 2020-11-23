using MovieAPI.DTOS;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieAPI.Services
{
    public class MovieService : IMovieService
    {

        List<Movie> movies;
        List<Stat> stats;

        public MovieService()
        {
            movies = File.ReadAllLines("SampleData\\metadata.csv").Skip(1).Select(line => new Movie(line)).ToList();

            stats =  File.ReadAllLines("SampleData\\stats.csv").Skip(1).Select(line => new Stat(line)).ToList();
        }

        public List<Movie> GetMovies()
        {
            return movies;
        }

        public Movie AddMovie(Movie movie)
        {
            var id = movies.Max(item => item.Id) + 1;

            movie.Id = id;

            movies.Add(movie);

            return movie;
        }

        public List<Stat> GetStats()
        {
            return stats;
        }

        public List<MovieStat> GetMovieStats()
        {
           var averageStats = stats.GroupBy(g => g.MovieId).Select(s => new { MovieId = s.Key, AverageDuration = s.Sum(o => o.WatchDuration) / s.Count(), Watches = s.Count() });

            var movieStats = movies.Join(averageStats,
                                           movie => movie.MovieId,
                                           stat => stat.MovieId,
                                           (movie, stat) => new MovieStat
                                           {
                                               MovieId = movie.MovieId,
                                               ReleaseYear = movie.ReleaseYear,
                                               Title = movie.Title,
                                               AverageWatchDurationS = Math.Round(TimeSpan.FromMilliseconds(stat.AverageDuration).TotalSeconds),
                                               Watches = stat.Watches
                                           }).ToList();

            var distinctMovieStats = movieStats.GroupBy(g => g.MovieId)
                                              .Select(s => s.First())
                                              .OrderByDescending(o=>(o.Watches, o.ReleaseYear))
                                              .ToList();
            return distinctMovieStats;
        }
    }
}
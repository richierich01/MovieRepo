
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.DTOS;
using MovieAPI.Models;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
       

        private readonly IMovieService movieService;
        private readonly IMapper mapper;

        public MoviesController(IMovieService movieService, IMapper mapper)
        {
            this.movieService = movieService;
            this.mapper = mapper;

        }

        [HttpGet]
        public  ActionResult<List<MovieDTO>> GetMovies()
        {
            var movies = movieService.GetMovies();

            var moviesDTO = movies.Select(item => mapper.Map<MovieDTO>(item)).ToList();

            return Ok(moviesDTO);
        }

        // GET api/Movies/5
        [HttpGet("{movieId}")]
        public ActionResult<List<MovieDTO>> GetMovieById(int movieId)
        {
            var moviesDTO = movieService.GetMovies().FindAll(o => o.MovieId == movieId)
                .GroupBy(g=> (g.MovieId, g.Language, g.Duration, g.Title))
                .Select(s=> new MovieDTO { Title = s.Key.Title, Duration = s.Key.Duration, Language = s.Key.Language, MovieId = s.Key.MovieId, ReleaseYear = s.Max(o=>o.ReleaseYear)})
                .ToList();

            if (moviesDTO == null)
            {
                return NotFound();
            }

            return Ok(moviesDTO);
        }

        // POST api/Movies
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MovieDTO> Post(MovieDTO movieDTO)
        {
            var movie = new Movie
            {
                Title = movieDTO.Title,
                Language = movieDTO.Language,
                Duration = movieDTO.Duration,
                MovieId  = movieDTO.MovieId,
                ReleaseYear = movieDTO.ReleaseYear
            };

            var newMovie = movieService.AddMovie(movie);

            return CreatedAtAction(nameof(GetMovieById), new { movieId = movie.MovieId }, movieDTO);
        }


        [HttpGet("stats")]
        public ActionResult<List<MovieDTO>> GetMovieStats()
        {
           return Ok(movieService.GetMovieStats().Distinct());
        }
    }
}
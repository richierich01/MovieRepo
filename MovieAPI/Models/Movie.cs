using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MovieAPI.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public int ReleaseYear { get; set; }

        public Movie()
        {

        }
        public Movie(string line)
        {
            Regex parser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            string[] fields = parser.Split(line);

            Id = Convert.ToInt32(fields[0]);
            MovieId = Convert.ToInt32(fields[1]);
            Title = fields[2];
            Language = fields[3];
            Duration = TimeSpan.Parse(fields[4]);
            ReleaseYear = Convert.ToInt32(fields[5]);
        }
    }

}
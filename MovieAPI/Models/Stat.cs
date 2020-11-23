using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MovieAPI.Models
{
    public class Stat
    {
        [Required]
        public int MovieId { get; set; }

        public long WatchDuration { get; set; }

        public Stat(string line)
        {
            Regex parser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            string[] fields = parser.Split(line);

            MovieId = Convert.ToInt32(fields[0]);
            WatchDuration = Convert.ToInt32(fields[1]);
        }
    }

}
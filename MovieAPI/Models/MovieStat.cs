using System;

namespace MovieAPI.DTOS
{
    public class MovieStat: IEquatable<MovieStat>
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public double AverageWatchDurationS { get; set; }
        public int Watches { get; set; }
        public int ReleaseYear { get; set; }


        public bool Equals(MovieStat other)
        {
            //Check whether the compared object is null.
            if (MovieStat.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (MovieStat.ReferenceEquals(this, other)) return true;

            //Check whether the MovieStat' properties are equal.
            return MovieId.Equals(other.MovieId);
        }
    }
}
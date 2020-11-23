using System;

namespace MovieAPI.DTOS
{
    public class MovieDTO
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public TimeSpan Duration { get; set; }
        public int ReleaseYear { get; set; }
    }
}
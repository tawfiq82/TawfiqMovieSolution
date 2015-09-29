using MoviesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TK.Service.Objects
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Classification { get; set; }
        public int Rating { get; set; }
        public int ReleaseYear { get; set; }
        public string[] Cast { get; set; }

        public Movie() { }

        public Movie(MovieData movieData)
        {
            if (movieData != null)
            {
                this.Id = movieData.MovieId;
                this.Title = movieData.Title;
                this.Genre = movieData.Genre;
                this.Classification = movieData.Classification;
                this.Rating = movieData.Rating;
                this.ReleaseYear = movieData.ReleaseDate;
                this.Cast = movieData.Cast;
            }
        }
    }
}

using MoviesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TK.Service.Objects
{
    public static class MovieExtension
    {
        public static MovieData GetMappingMovieData(this Movie movie)
        {
           return new MovieData()
           {
               MovieId = movie.Id,
               Title = movie.Title,
               Genre = movie.Genre,
               Classification = movie.Classification,
               Rating = movie.Rating,
               ReleaseDate = movie.ReleaseYear,
               Cast = movie.Cast
           };
        }

        public static IEnumerable<Movie> OrderBy(this IEnumerable<Movie> movies, string orderByAttributeName)
        {
            if (movies == null) return movies;
            PropertyInfo property = typeof(Movie).GetProperty(orderByAttributeName);
            if (property != null)
            {
                return movies.OrderBy(x => property.GetValue(x, null));
            }
            else
            {
                return movies.OrderBy(x => x.Title);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesLibrary;
using TK.Service.Search;
using System.Reflection;
using TK.Service.Objects;

namespace TK.Service.Services
{
    public class MovieService : IMovieService
    {
        private MovieDataSource _dataSource;

        public MovieService()
        {
            _dataSource = new MovieDataSource();
        }

        public IList<Movie> GetAllMovies(string sortAttribute = "Title")
        {
            return CachedMovieList.Instance.Movies.OrderBy(sortAttribute).ToList();
        }

        public IList<Movie> SearchMovies(IList<SearchCriteria> criterias, string sortAttribute = "Title")
        {
            var movies = CachedMovieList.Instance.Movies;
            return SearchHelper.GetSerchResult(movies, criterias).OrderBy(sortAttribute).ToList();
        }

        public void InsertMovie(Movie movie)
        {
            try
            {
                _dataSource.Create(movie.GetMappingMovieData());
                CachedMovieList.Instance.Updated();
            }
            catch (Exception ex)
            {

                throw new Exception("Could not Create Movie", ex);
            }
        }

        public void UpdateMovie(Movie movie)
        {
            try
            {
                _dataSource.Update(movie.GetMappingMovieData());
                CachedMovieList.Instance.Updated();
            }
            catch (Exception ex)
            {

                throw new Exception("Could not update Movie", ex);
            }
        }

        public Movie GetMovie(int id)
        {
            var movieData= _dataSource.GetDataById(id);
            if (movieData == null) return null;
            return new Movie(movieData);
        }
    }
}

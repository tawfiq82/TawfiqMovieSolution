using MoviesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.Service.Objects;
using TK.Service.Search;

namespace TK.Service.Services
{
    public interface IMovieService
    {
        IList<Movie> GetAllMovies(string sortAttribute = "Title");
        IList<Movie> SearchMovies(IList<SearchCriteria> criteria, string sortAttribute = "Title");
        void InsertMovie(Movie movie);
        void UpdateMovie(Movie movie);
        Movie GetMovie(int id);
    }
}

using MoviesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.Service.Objects;

namespace TK.Service.Services
{
    public sealed class CachedMovieList
    {
        private static volatile CachedMovieList _instance;
        private static object _syncRoot = new Object();
        private static DateTime _lastUpdated;
        private static IEnumerable<Movie> _movies;
        private static bool _isUpdated;

        private CachedMovieList()
        {
            _lastUpdated = DateTime.Now;
            _isUpdated = true;
        }

        public IEnumerable<Movie> Movies
        {
            get
            {
                MovieDataSource dataSource = new MovieDataSource();
                double hoursToLastUpdate = DateTime.Now.Subtract(_lastUpdated).TotalHours;
                if (_movies == null || _isUpdated || hoursToLastUpdate > 24)
                {
                    _movies = dataSource.GetAllData().Select(x => new Movie(x));
                    _isUpdated = false;
                    _lastUpdated = DateTime.Now;
                }
                return _movies;
            }
        }

        public void Updated()
        {
            _isUpdated = true;
        }

        public static CachedMovieList Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new CachedMovieList();
                    }
                }
                return _instance;
            }
        }
    }
}

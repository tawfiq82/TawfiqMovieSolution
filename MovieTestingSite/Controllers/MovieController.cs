using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TK.Service.Objects;
using TK.Service.Search;
using TK.Service.Services;

namespace MovieTestingSite.Controllers
{
    public class MovieController : Controller
    {
        private IMovieService _service;

        public MovieController()
        {
            _service = new MovieService();
        }

        // GET: Movies
        public ActionResult Index(string sortAttribute = "Title")
        {
            var model = _service.GetAllMovies(sortAttribute);
            return View(model);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            var model = _service.GetMovie(id);
            return View(model);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            var model = new Movie();
            return View(model);
        }

        // POST: Movies/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Movie model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.InsertMovie(model);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _service.GetMovie(id);
            return View(model);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Movie model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.UpdateMovie(model);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Search(string sortAttribute = "Title")
        {
            List<SearchCriteria> criterias = new List<SearchCriteria>()
            {
                new SearchCriteria { Name = "Rating" , Operator = OperatorType.GreaterThan, Value = "3"  },
                new SearchCriteria { Name = "ReleaseYear" , Operator = OperatorType.GreaterThanOrEqual, Value = "2013"  },
                new SearchCriteria { Name = "Genre" , Operator = OperatorType.NotEquals, Value = "Comedy"  },
                new SearchCriteria { Name = "Classification" , Operator = OperatorType.Contains, Value = "M15+"  },
               new SearchCriteria { Name = "Cast" , Operator = OperatorType.Contains, Value = "Leonardo DiCaprio"  }

            };
            var model = _service.SearchMovies(criterias, sortAttribute);
            return View(model);
        }

        public ActionResult InvalidSearch(string sortAttribute = "Title")
        {
            List<SearchCriteria> criterias = new List<SearchCriteria>()
            {
                new SearchCriteria { Name = "Rating" , Operator = OperatorType.Contains, Value = "3"  },
                new SearchCriteria { Name = "ReleaseYear" , Operator = OperatorType.NotEquals ,Value = "2013"  },
                new SearchCriteria { Name = "Genre" , Operator = OperatorType.LessThanOrEqual, Value = "Comedy"  },
                new SearchCriteria { Name = "Classification" , Operator = OperatorType.GreaterThanOrEqual, Value = "M15+"  },
                new SearchCriteria { Name = "Cast" , Operator = OperatorType.GreaterThanOrEqual, Value = "Tet"  },
            };
            var model = _service.SearchMovies(criterias, sortAttribute);
            return View("Search", model);
        }
    }
}

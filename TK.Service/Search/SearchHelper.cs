using MoviesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TK.Service.Objects;

namespace TK.Service.Search
{
    public class SearchHelper
    {
        public static IEnumerable<Movie> GetSerchResult(IEnumerable<Movie> movies, IList<SearchCriteria> criterias)
        {
            foreach (var critiera in criterias)
            {
                switch (critiera.Name)
                {
                    case "Cast":
                        if (critiera.Operator == OperatorType.Contains)
                            movies = movies.Where(x => x.Cast.Contains(critiera.Value));
                        break;
                    default:
                        var predicate = SearchExpressionBuilder.GetExpression<Movie>(critiera);
                        if (predicate != null)
                        {
                            movies = movies.Where(predicate.Compile());
                        }
                        break;
                }
            }
            return movies;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TK.Service.Search
{
    public class SearchExpressionBuilder
    {
        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");

        public static Expression<Func<T, bool>> GetExpression<T>(SearchCriteria criteria)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "x");
            Expression exp = GetExpression<T>(param, criteria);
            if (exp == null) return null;
            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression<T>(ParameterExpression param, SearchCriteria criteria)
        {
            Type type = typeof(T);
            PropertyInfo property = type.GetProperty(criteria.Name);
            if (property != null)
            {
                MemberExpression member = Expression.Property(param, criteria.Name);
                if (property.PropertyType == typeof(int))
                {
                    int value = 0;
                    Int32.TryParse(criteria.Value, out value);
                    return GetExpressionForIntType(member, Expression.Constant(value), criteria.Operator);
                }
                else if (property.PropertyType == typeof(string))
                {
                    return GetExpressionForStringType(member, Expression.Constant(criteria.Value), criteria.Operator);
                }
            }
            return null;
        }

        private static Expression GetExpressionForStringType(MemberExpression member, ConstantExpression constant, OperatorType opertaor)
        {
            switch (opertaor)
            {
                case OperatorType.Equals:
                    return Expression.Equal(member, constant);
                case OperatorType.NotEquals:
                    return Expression.NotEqual(member, constant);
                case OperatorType.Contains:
                    return Expression.Call(member, containsMethod, constant);
            }
            return null;
        }

        private static Expression GetExpressionForIntType(MemberExpression member, ConstantExpression constant, OperatorType opertaor)
        {
            switch (opertaor)
            {
                case OperatorType.Equals:
                    return Expression.Equal(member, constant);
                case OperatorType.NotEquals:
                    return Expression.NotEqual(member, constant);
                case OperatorType.LessThan:
                    return Expression.LessThan(member, constant);
                case OperatorType.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, constant);
                case OperatorType.GreaterThan:
                    return Expression.GreaterThan(member, constant);
                case OperatorType.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);
            }
            return null;
        }
    }
}

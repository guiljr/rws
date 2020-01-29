using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Helper.Queryable
{
    public static class QueryableExtension
    {
        private static IOrderedQueryable<T> Ordering<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), string.Empty);

            var propertyNames = propertyName.Split('.');
            Expression property = param;
            foreach (var prop in propertyNames)
            {
                property = Expression.PropertyOrField(property, prop);
            }
            LambdaExpression sort = Expression.Lambda(property, param);
            MethodCallExpression call = Expression.Call(
              typeof(System.Linq.Queryable),
              (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
              new[] { typeof(T), property.Type },
              source.Expression,
              Expression.Quote(sort));
            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }

        //private static IOrderedQueryable<T> Ordering<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
        //{
        //    ParameterExpression param = Expression.Parameter(typeof(T), string.Empty);
        //    MemberExpression property = Expression.PropertyOrField(param, propertyName);
        //    LambdaExpression sort = Expression.Lambda(property, param);
        //    MethodCallExpression call = Expression.Call(
        //        typeof(System.Linq.Queryable),
        //        (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
        //        new[] { typeof(T), property.Type },
        //        source.Expression,
        //        Expression.Quote(sort));
        //    return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        //}

        public static IOrderedQueryable<T> OrderByAscending<T>(this IQueryable<T> source, string propertyName)
        {
            return Ordering(source, propertyName, false, false);
        }
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return Ordering(source, propertyName, true, false);
        }
        public static IOrderedQueryable<T> ThenByAscending<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return Ordering(source, propertyName, false, true);
        }
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return Ordering(source, propertyName, true, true);
        }
    }
}
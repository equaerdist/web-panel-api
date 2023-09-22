using System.Linq.Expressions;

namespace web_panel_api.Services
{
    public class Filter<T>
    {
        public static Expression<Func<T, object>> CreateKeySelector(string propertyPath)
        {
            if (string.IsNullOrEmpty(propertyPath))
                throw new ArgumentNullException(nameof(propertyPath));
            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = propertyPath.Split('.')
                .Aggregate((Expression)parameter, Expression.PropertyOrField);
            var conversion = Expression.Convert(propertyAccess, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(conversion, parameter);
            return lambda;
        }

    }
}

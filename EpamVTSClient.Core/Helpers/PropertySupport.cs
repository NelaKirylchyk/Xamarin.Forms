using System;
using System.Linq.Expressions;

namespace EpamVTSClient.Core.Helpers
{
    public static class PropertySupport
    {
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Invalid Expression", nameof(propertyExpression));
            }
            return memberExpression.Member.Name;
        }
    }
}
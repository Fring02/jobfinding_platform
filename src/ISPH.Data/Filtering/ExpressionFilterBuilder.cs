using System;
using System.Linq.Expressions;
using ISPH.Domain.Models.Advertisements;

namespace ISPH.Data.Filtering;

public class ExpressionFilterBuilder
{
    public ExpressionFilterBuilder With(Expression<Func<Advertisement, bool>> predicate)
    {
        Result = Expression.Lambda<Func<Advertisement, bool>>(Expression
            .AndAlso(Result.Body, Expression.Invoke(predicate, Result.Parameters)), Result.Parameters);
        return this;
    }

    public Expression<Func<Advertisement, bool>> Result { get; private set; } = a => true;
}